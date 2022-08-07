namespace Celic
{
    /// <summary> Вещественное число одинарной точности, которое может окначиваться на запятую и нуль </summary>
    public struct EFloat
    {
        #region Special Format Type

        /// <summary> Специальный тип формата представления вещественного числа </summary>
        public enum Format : byte
        {
            /// <summary> Формат числа по умолчанию ( отсутствуют нестандартные ситуации ) </summary>
            define = 0,
            /// <summary> Число оканчивается на запятую </summary>
            commaAtEnd = 1,
            /// <summary> Число оканчивается на нуль </summary>
            oneZeroAtEnd = 2,
            /// <summary> Число окначивается на два нуля </summary>
            twoZerosAtEnd = 4,
            /// <summary> Число является пустой строкой ( значение для format ) </summary>
            empty = 8
        }

        #endregion

        #region Private Fields

        /// <summary> Вещественное число одинарной точности ( поле ) </summary>
        private float value;
        /// <summary> Формат числа </summary>
        private Format format;

        #endregion

        #region Public Properties

        /// <summary> Вещественное число одинарной точности </summary>
        public float V
        {
            get => value;
            set => format = (this.value = value) == -1 ? Format.empty : Format.define;
        }

        #endregion

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        /// <param name="value"> Вещественное число одинарной точности </param>
        /// <param name="format"> Формат представления числа </param>
        public EFloat(float value = -1, Format format = Format.define)
        {
            if (format == Format.empty)
            {
                this.format = Format.empty;
                this.value = 0;
            }
            else
            {
                this.value = value;
                this.format = format;
            }
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
        public override bool Equals(object obj) => obj != null && obj is EFloat other && value == other.value && format == other.format;
        /// <summary> Уникальное хеш-значение объекта </summary>
        /// <returns> Хеш-значение </returns>
        public override int GetHashCode() => base.GetHashCode();
        /// <summary> EFloat является вещественным числом ( отсутствуют паарметры дополнительного форматирования ) </summary>
        /// <returns> true, если format == 0 и false в проивном случае </returns>
        public bool IsFloat() => format == Format.define;
        /// <summary> Перегруженный оператор проверки на равенство </summary>
        /// <param name="val1"> Переменная типа EFloat, находящаяся слева </param>
        /// <param name="val2"> Переменная типа EFloat, находящаяся справа </param>
        /// <returns> true, если переменный равны, false в противном случае </returns>
        public static bool operator ==(EFloat val1, EFloat val2) => val1.Equals(val2);
        /// <summary> Перегруженный оператор проверки на неравенство </summary>
        /// <param name="val1"> Переменная типа EFloat, находящаяся слева </param>
        /// <param name="val2"> Переменная типа EFloat, находящаяся справа </param>
        /// <returns> true, если переменный не равны, false в противном случае </returns>
        public static bool operator !=(EFloat val1, EFloat val2) => !val1.Equals(val2);
        /// <summary> Перегруженный оператор сравнения двух переменных </summary>
        /// <param name="val1"> Переменная типа EFloat, находящаяся слева </param>
        /// <param name="val2"> Переменная типа EFloat, находящаяся справа </param>
        /// <returns> true, если переменная слева больше переменой справа, false в противном случае </returns>
        public static bool operator >(EFloat val1, EFloat val2) => val1.value > val2.value;
        /// <summary> Перегруженный оператор сравнения двух переменных </summary>
        /// <param name="val1"> Переменная типа EFloat, находящаяся слева </param>
        /// <param name="val2"> Переменная типа EFloat, находящаяся справа </param>
        /// <returns> true, если переменная слева меньше переменой справа, false в противном случае </returns>
        public static bool operator <(EFloat val1, EFloat val2) => val1.value < val2.value;
        /// <summary> Перегруженный оператор сравнения двух переменных </summary>
        /// <param name="val1"> Переменная типа EFloat, находящаяся слева </param>
        /// <param name="val2"> Переменная типа EFloat, находящаяся справа </param>
        /// <returns> true, если переменная слева больше либо равна переменой справа, false в противном случае </returns>
        public static bool operator >=(EFloat val1, EFloat val2) => val1 > val2 || val1 == val2;
        /// <summary> Перегруженный оператор сравнения двух переменных </summary>
        /// <param name="val1"> Переменная типа EFloat, находящаяся слева </param>
        /// <param name="val2"> Переменная типа EFloat, находящаяся справа </param>
        /// <returns> true, если переменная слева меньше либо равна переменой справа, false в противном случае </returns>
        public static bool operator <=(EFloat val1, EFloat val2) => val1 < val2 || val1 == val2;
        /// <summary> Проверка переменной на отсутствие значения </summary>
        /// <returns> true, если переменная не хранит значения, false в противном случае </returns>
        public bool IsClear() => format == Format.empty;

        #endregion

        #region EFloatConverter

        /// <summary> Преобразование вещественного числа типа EFloat к строке </summary>
        /// <returns> Строковое представление числа </returns>
        public override string ToString()
        {
            string valueS = value.ToString();
            switch (format)
            {
                case Format.empty: valueS = "0"; break;
                case Format.define: break;
                case Format.commaAtEnd: valueS += ","; break;
                case Format.oneZeroAtEnd: valueS += ",0"; break;
                case Format.twoZerosAtEnd: valueS += ",00"; break;
            }
            return valueS;
        }
        /// <summary> Преобразование строкового представления вещественного числа типа EFloat в число типа EFloat </summary>
        /// <param name="text"> Строковое представление числа </param>
        /// <returns> Вещественное число типа EFloat </returns>
        public static EFloat Parse(string text)
        {
            EFloat result;
            result.value = -1;
            result.format = Format.define;
            if (text != null && text != string.Empty)
            {
                text = HelpManager.StringIsNumber(text);
                if (text != string.Empty)
                    result.value = float.Parse(text);
                if (text.Length >= 2 && text.Contains(","))
                    switch (text[text.Length - 1])
                    {
                        case ',': result.format = Format.commaAtEnd; break;
                        case '0': result.format = text[text.Length - 2].Equals('0') ? Format.twoZerosAtEnd : Format.oneZeroAtEnd; break;
                    }
            }
            return result;
        }

        #endregion
    }
}
