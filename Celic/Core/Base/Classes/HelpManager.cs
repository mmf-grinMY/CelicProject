using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace Celic
{
	/// <summary> Вспомогательный класс, содержащий часто используемые общие методы </summary>
    public static class HelpManager
    {
        #region Public Static Constants

        /// <summary> Камерная система разработки </summary>
        public static readonly string CAMERA_DEV = "камерная";
        /// <summary> Cтолбовая система разработки </summary>
        public static readonly string LAVA_DEV = "столбовая";
        /// <summary> Система разработки не задана </summary>
        public static readonly string UNDEFINE_DEV = "не задана";

        #endregion

        #region Public Static Methods

        /*/// <summary> Преобразование строки в строку, схожую на вещественной число </summary>
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
        public static string ValidateString(string str) 
        {
        	float _;
            return str != string.Empty ? float.TryParse((str = StringIsNumber(str)) + (str[str.Length - 1].Equals(',') ? "0" : ""), out _) ? str : "" : "";
        }
        /// <summary> Проверка строки на схожесть с вещественным числом в указанном диапазоне </summary>
        /// <param name="str"> Исходная строка </param>
        /// <returns> Исходная строка, если она схожа с числом, "" в противном случае </returns>
        /// <param name="start"> Начальное значение диапазона </param>
        /// <param name="end"> Конечное значение диапазона </param>
        /// <returns> Исходная строка, если она схожа с числом из указанного диапазона, "" в противном случае </returns>
        public static string ValidateStringRange(string str, float start = 0F, float end = 1F) 
        {
        	float res;
        	return str != string.Empty ? float.TryParse((str = StringIsNumber(str)) + (str[str.Length - 1] == ',' ? "0" : ""), out res) ? (res >= start && res <= end ? str : "") : "" : "";
        }*/
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
            // CalculatorB calc = new CalculatorB(help);
            if(txt != null)
            	txt.AddRange(CalculatorB.TextCount);
            float ht = CalculatorB.Count(help);
            for (int i = 0; i < end - begin + 1; i++)
            {
                help.RemoveAt(help.Count - 1);
            }
            return ht;
        }
        /// <summary> Вычисление значения котангенса угла </summary>
        /// <param name="angle"> Угол в радианах </param>
        /// <returns> Значение котангенса, если он сущестует и 0 в противном случае </returns>
        public static float Ctg(float angle) 
        {
        	return Math.Tan(angle) != 0 ? (float)Math.Tan(angle) : 0;
        }

        #endregion
    }
}
