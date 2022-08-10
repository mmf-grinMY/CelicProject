using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace Celic
{
    /// <summary> Вспомогательный класс, содержащий часто используемые общие методы </summary>
    public class HelpManager
    {
        #region Public Static Constants

        /// <summary> Камерная система разработки </summary>
        public static readonly string CAMERA_DEV = "камерная";
        /// <summary> Cтолбовая система разработки </summary>
        public static readonly string LAVA_DEV = "столбовая";
        /// <summary> Система разработки не задана </summary>
        public static readonly string UNDEFINE_DEV = "не задана";
        /// <summary> Минимально необходимая величина ПВП </summary>
        public static readonly int M = 35;

        #endregion

        #region Public Static Methods

        /// <summary> Преобразование строки в строку, схожую на вещественной число </summary>
        /// <param name="str"> Исходная строка </param>
        /// <returns> Преобразованная строка </returns>
        public static string StringIsNumber(string str)
        {
            bool isFloat = false;
            for (int i = 0; i < str.Length; i++)
                if (char.IsDigit(str[i]) || str[i] == ',')
                {
                    if (str[i] == ',')
                        if (!isFloat)
                            isFloat = true;
                        else
                            str = str.Remove(i--, 1);
                    else if (str[0] == '0' && str[i] == '0' && (i + 1) < str.Length && str[i + 1] == '0')
                        str = str.Remove(i--, 1);
                }
                else
                {
                    str = str.Remove(i--, 1);
                }
            return str;
        }
        /// <summary> Проверка строки на схожесть с вещественным числом </summary>
        /// <param name="str"> Исходная строка </param>
        /// <returns> Исходная строка, если она схожа с числом, "" в противном случае </returns>
        public static string ValidateString(string str) =>
            str != string.Empty ? float.TryParse((str = StringIsNumber(str)) + (str[str.Length - 1].Equals(',') ?
                "0" : ""), out float _) ? str : "" : "";
        /// <summary> Проверка строки на схожесть с вещественным числом в указанном диапазоне </summary>
        /// <param name="str"> Исходная строка </param>
        /// <returns> Исходная строка, если она схожа с числом, "" в противном случае </returns>
        /// <param name="start"> Начальное значение диапазона </param>
        /// <param name="end"> Конечное значение диапазона </param>
        /// <returns> Исходная строка, если она схожа с числом из указанного диапазона, "" в противном случае </returns>
        public static string ValidateStringRange(string str, float start = 0F, float end = 1F) =>
            str != string.Empty ? float.TryParse((str = StringIsNumber(str)) + (str[str.Length - 1] == ',' ? "0" : ""),
                out float res) ? (res >= start && res <= end ? str : "") : "" : "";
        /// <summary> Вычисление высот ЗВТ над несколькими пластами </summary>
        /// <param name="begin"> Номер первого элемента, входящего в расчет </param>
        /// <param name="end"> Номер последнего элемента, входящего в расчет </param>
        /// <param name="plasts"> Коллекция рассматриваемых плаcтов </param>
        /// <param name="txt"> Информация о проведении подсчета </param>
        /// <returns> Значение высоты ЗВТ </returns>
        public static float SimpleCalcHt(int begin = 0, int end = 1, ObservableCollection<Plast> plasts = null, List<string> txt = null)
        {
            ObservableCollection<Plast> help = new ObservableCollection<Plast>();
            for (int i = begin; i <= end; i++)
                help.Add(plasts[i]);
            SCalculatorB calc = new SCalculatorB(help);
            txt?.AddRange(calc.TextCount);
            float ht = calc.Count();
            for (int i = 0; i < end - begin + 1; i++)
            {
                help.RemoveAt(help.Count - 1);
            }
            return ht;
        }
        /// <summary> Проверка двух пластов на сближенность по условиям водопроводимости </summary>
        /// <param name="plast1"> Первый пласт </param>
        /// <param name="plast2"> Второй пласт </param>
        /// <returns> true, если пласты являются сближенными; false в протичном случае </returns>
        public static bool IsContiguous(Plast plast1, Plast plast2)
        {
            if(plast1.MainMineField.H > plast2.MainMineField.H)
                return plast1.Buttom.Equals(plast2.Name) && plast2.Top.Equals(plast1.Name);
            else
                return plast2.Buttom.Equals(plast1.Name) && plast1.Top.Equals(plast2.Name);
        }
        /// <summary> Проверка коллекции пластов на наличие сближенных по условиям водопроводимости </summary>
        /// <param name="plasts"> Коллекция рассматриваемых пластов </param>
        /// <returns> true, если есть сближенные; false, если нет ни одной пары сближенных пластов </returns>
        public static bool IsContiguous(ObservableCollection<Plast> plasts)
        {
            foreach (Plast plast1 in plasts)
                foreach (Plast plast2 in plasts)
                    if (plast1 != plast2 && IsContiguous(plast1, plast2))
                        return true;
            return false;
        }
        /// <summary> Сортировка коллекции пластов по возрастанию величины параметра H </summary>
        /// <param name="plasts"> Коллекция разрабатываемых пластов</param>
        /// <returns> Отсортированная коллекция пластов </returns>
        public static ObservableCollection<Plast> Sort(ObservableCollection<Plast> plasts)
        {
            for (int i = 1; i < plasts.Count; i++)
                for (int j = i; j > 0 && plasts[j - 1].MainMineField.H > plasts[j].MainMineField.H; j--)
                    plasts.Move(j - 1, j);
            return plasts;
        }
        /// <summary> Выбор коэффициента параметра d при генерации отчета </summary>
        /// <param name="plast"> Рассматриваемый пласт </param>
        /// <returns> Значение коэффициента в виде строки </returns>
        public static string GetD(Plast plast) => plast.TypeDev == LAVA_DEV ? "46" : "26";
        /// <summary> Вычисление значения котангенса угла </summary>
        /// <param name="angle"> Угол в радианах </param>
        /// <returns> Значение котангенса, если он сущестует и 0 в противном случае </returns>
        public static float Ctg(float angle) => Math.Tan(angle) != 0 ? (float)Math.Tan(angle) : 0;

        #endregion
    }
}
