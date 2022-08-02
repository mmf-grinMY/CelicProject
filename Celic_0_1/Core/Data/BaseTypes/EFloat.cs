﻿namespace Celic
{
    /// <summary> Вещественное число одинарной точности, которое может окначиваться на запятую и нуль </summary>
    public struct EFloat
    {
        #region Private Fields

        /// <summary> Формат числа по умолчанию ( отсутствуют нестандартные ситуации ) </summary>
        private static readonly byte defMask = 0;
        /// <summary> Число оканчивается на запятую </summary>
        private static readonly byte commaAtEndMask = 1;
        /// <summary> Число оканчивается на нуль </summary>
        private static readonly byte zero1AtEndMask = 2;
        /// <summary> Число окначивается на два нуля </summary>
        private static readonly byte zero2AtEndMask = 4;
        /// <summary> Число является пустой строкой </summary>
        private static readonly byte emptyMask = 8;
        /// <summary> Вещественное число одинарной точности ( поле ) </summary>
        private float value;
        /// <summary> Формат числа </summary>
        private byte format;

        #endregion

        #region Public Properties

        /// <summary> Вещественное число одинарной точности </summary>
        public float V { get => value; }

        #endregion

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        /// <param name="value"> Вещественное число одинарной точности </param>
        /// <param name="format"> Формат представления числа </param>
        public EFloat(float value = 0, byte format = 0)
        {
            this.value = value;
            this.format = emptyMask;
        }

        #endregion

        #region Public Methods

        /// <summary> Сложение двух чисел типа EFloat </summary>
        /// <param name="val1"> Первое слагаемое </param>
        /// <param name="val2"> Второе слагаемое </param>
        /// <returns> Сумма двух чисел </returns>
        public static float operator +(EFloat val1, EFloat val2) => val1.value + val2.value;
        /// <summary> Разность двух чисел типа EFloat </summary>
        /// <param name="val1"> Уменьшаемое </param>
        /// <param name="val2"> Вычитаемое </param>
        /// <returns> Разность двух чисел типа float </returns>
        public static float operator -(EFloat val1, EFloat val2) => val1.value - val2.value;
        /// <summary> Произведение двух вещественных чисел типа EFloat </summary>
        /// <param name="val1"> Первый множитель </param>
        /// <param name="val2"> Второй множитель </param>
        /// <returns> Произведение двух вещественных чисел типа float </returns>
        public static float operator *(EFloat val1, EFloat val2) => val1.value * val2.value;
        /// <summary> Деление двух вещественных чисел типа EFloat </summary>
        /// <param name="val1"> Делимое </param>
        /// <param name="val2"> Делитель </param>
        /// <returns> Частное типа float </returns>
        public static float operator /(EFloat val1, EFloat val2) => val1.value / val2.value;
        /// <summary> Сравнение двух вещественных чисел типа EFloat </summary>
        /// <param name="obj"> Объект, с которым сравнивается данный </param>
        /// <returns> true, если вещественные чисал и их форматы одинаковы, false в противном случае </returns>
        public override bool Equals(object obj) => obj != null && (obj is EFloat other && value == other.value && format == other.format);
        /// <summary> Преобразование вещественного числа типа EFloat к строке </summary>
        /// <returns> Строковое представление числа </returns>
        public override string ToString()
        {
            if ((format & emptyMask) != 0)
                return "";
            string valueS = value.ToString();
            if ((format & defMask) != 0)
                return valueS;
            if((format & commaAtEndMask) != 0)
                valueS += ",";
            if((format & zero2AtEndMask) != 0)
                valueS += "00";
            if((format & zero1AtEndMask) != 0)
                valueS += "0";
            return valueS;
        }
        /// <summary> Уникальное хеш-значение объекта </summary>
        /// <returns> Хеш-значение </returns>
        public override int GetHashCode() => base.GetHashCode();
        /// <summary> Преобразование строкового представления вещественного числа типа EFloat в число типа EFloat </summary>
        /// <param name="text"> Строковое представление числа </param>
        /// <returns> Вещественное число типа EFloat </returns>
        public static EFloat Parse(string text)
        {
            EFloat result;
            result.value = -1;
            result.format = defMask;
            if (text != null)
            {
                // text = ValidateString(text);
                result.value = float.Parse(text);
                if (text.Length >= 2 && text.Contains(","))
                {
                    switch(text[text.Length - 1])
                    {
                        case ',': result.format = commaAtEndMask; break;
                        case '0': result.format = text[text.Length - 2].Equals('0') ? zero2AtEndMask : zero1AtEndMask; break;
                    }
                }
            }
            return result;
        }
        /// <summary> EFloat является вещественным числом ( отсутствуют паарметры дополнительного форматирования ) </summary>
        /// <returns> true, если format == 0 и false в проивном случае </returns>
        public bool IsFloat() => format == defMask;

        #endregion
    }
}
