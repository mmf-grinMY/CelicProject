using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace Celic
{
	/// <summary> Вспомогательный класс, содержащий часто используемые общие методы </summary>
    public static class HelpManager
    {
    	public static readonly int CALC_ERROR = -1;
        /*#region Public Static Constants

        /// <summary> Камерная система разработки </summary>
        public static readonly string CAMERA_DEV = "камерная";
        /// <summary> Cтолбовая система разработки </summary>
        public static readonly string LAVA_DEV = "столбовая";
        /// <summary> Система разработки не задана </summary>
        public static readonly string UNDEFINE_DEV = "не задана";

        #endregion*/

        #region Public Static Methods
        
        /// <summary> Вычисление высот ЗВТ над несколькими пластами </summary>
        /// <param name="begin"> Номер первого элемента, входящего в расчет </param>
        /// <param name="end"> Номер последнего элемента, входящего в расчет </param>
        /// <param name="plasts"> Коллекция рассматриваемых плаcтов </param>
        /// <param name="txt"> Информация о проведении подсчета </param>
        /// <returns> Значение высоты ЗВТ </returns>
        public static float CalculationHtLog(int begin = 0, int end = 1, ObservableCollection<Plast> plasts = null, List<string> reportText = null)
        {
        	if(plasts == null)
        		return CALC_ERROR;
            ObservableCollection<Plast> help = new ObservableCollection<Plast>();
            for (int i = begin; i <= end; i++)
                help.Add(plasts[i]);
            List<string> txt = new List<string>();
            float ht = CalculatorB.CountLog(help, txt);
            if(reportText != null)
            	reportText.AddRange(txt);
            help.Clear();
            return ht;
        }
        public static float CalculationHt(int begin = 0, int end = 1, ObservableCollection<Plast> plasts = null)
        {
        	if(plasts == null)
        		return CALC_ERROR;
            ObservableCollection<Plast> help = new ObservableCollection<Plast>();
            for (int i = begin; i <= end; i++)
                help.Add(plasts[i]);
            float ht = CalculatorB.Count(help);
            help.Clear();
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
