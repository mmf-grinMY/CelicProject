using System;
using System.Text;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;

namespace Celic
{
	/// <summary> Методика расчета высоты распространения зоны техногенных водопроводящих трещин над выработанным пространством </summary>
	static class CalculatorB
    {
    	#region Simple Calculation
    	
    	/// <summary> Расчет высоты ЗВТ без логирования информациии о произведенных расчетах </summary>
        /// <param name="h"> Старое значение высоты ЗВТ </param>
        /// <param name="plasts">Коллекция разрабатываемых пластов </param>
        /// <returns> Новое значение высоты ЗВТ </returns>
		private static float InnerCountHt(float h, ObservableCollection<Plast> plasts)
		{
	        float ht = PlastManager.Ht(plasts[0]) * plasts[0].S;
	        for (int i = 1; i < plasts.Count; i++)
	            ht += h * PlastManager.Ht(plasts[i]) * plasts[i].S * plasts[i].Sz * plasts[i].Kt / (h + plasts[i].Main.H - plasts[0].Main.H);
	        return ht;
	    }
        /// <summary> Расчет высоты ЗВТ без логирования произвденных операций </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        private static float CountHt(ObservableCollection<Plast> plasts) 
        {
            float oldHt = PlastManager.Ht(plasts[0]), newHt = InnerCountHt(oldHt, plasts);
            while (newHt - oldHt >= 2) {
                oldHt = newHt;
                newHt = InnerCountHt(oldHt, plasts);
            }
            newHt = (float)Math.Round(newHt * 100) / 100;
            return newHt;
        }
        /// <summary> Метод для расчета итогового значения ЗВТ </summary>
        /// <returns> Итоговое значение ЗВТ </returns>
        public static float Count(ObservableCollection<Plast> plasts)
        {
        	// Минимально необходимая величина ПВП
        	const int M = 35;
            float ht = 0;
            if (plasts != null && (ht = CountHt(plasts)) <= M) {
				PlastCollectionManager.RecalcAllParams(plasts);
				ht = CountHt(plasts);
			}
            return ht;
        }
    	
    	#endregion
		
        #region Calculation With Report
        
        private static float InnerRound(float value) 
        {
        	return (float)Math.Round(value * 100) / 100;
        }
        /// <summary> Расчет высоты ЗВТ с логированием произведенных операций </summary>
        /// <param name="h"> Старое значение высоты ЗВТ </param>
        /// <returns> Новое значение высоты ЗВТ </returns>
        private static float CountHtWithLog(float h, ObservableCollection<Plast> plasts, ref int iter, List<string> txt)
        {
			iter++;
			float ht = PlastManager.Ht(plasts[0]);
			string text = "H_" + iter + " = " + ht;
			Plast p_i;
			for (int i = 1; i < plasts.Count; i++) {
				p_i = plasts[i];
				ht += h * PlastManager.Ht(p_i) * p_i.S * p_i.Sz * p_i.Kt / (h + p_i.Main.H - plasts[0].Main.H);
				h = InnerRound(h);
				text += " + (" + h + "∙" + InnerRound(PlastManager.CalcD(p_i)) + "∙" + PlastManager.MPr(p_i) + "∙" +
					p_i.S + "∙" + p_i.Sz + "∙" + p_i.Kt + ")/(" + h + " + " + (p_i.Main.H - plasts[0].Main.H) + ")";
			}
			text += " = " + InnerRound(ht);
			txt.Add(text);
			p_i = null;
			return ht;
		}
		/// <summary> Расчет высоты ЗВТ с логированием произведенных операций </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        private static float CountHtLog(ObservableCollection<Plast> plasts, List<string> txt)
        {
			int iter = 0;
            float oldHt, newHt;
            txt.Add("Н_т = h_1 + (h d_2 m_пр2 S_2 S_z2 k_t2)/(h + ∆H_(1-2) ) + ... + (h d_n m_прn S_n S_zn k_tn)/(h + ∆H_(1-n) )");
            oldHt = PlastManager.Ht(plasts[0]);
            newHt = CountHtWithLog(oldHt, plasts, ref iter, txt);
            while (newHt - oldHt >= 2) {
                oldHt = newHt;
                newHt = CountHtWithLog(oldHt, plasts, ref iter, txt);
            }
            newHt = InnerRound(newHt);
            oldHt = InnerRound(oldHt);
            txt.Add(iter + " - H_" + (iter - 1) + " < 2 => " + newHt+ " - " + oldHt + " = " + InnerRound(newHt - oldHt) + " < 2 => – условие соблюдается.Принимаем значение Н_т = " + newHt + " м.");
            return newHt;
        }        
		/// <summary> Метод для расчета итогового значения ЗВТ </summary>
        /// <returns> Итоговое значение ЗВТ </returns>
        public static float CountLog(ObservableCollection<Plast> plasts, List<string> txt) 
        {
        	// Минимально необходимая величина ПВП
        	const int M = 35;
            float ht = 0;
            if (plasts != null && (ht = CountHtLog(plasts, txt)) <= M)
            {
				txt.Add("Был произведен дополнительный расчет коэффициентов пласта.");
				PlastCollectionManager.RecalcAllParams(plasts);
				ht = CountHtLog(plasts, txt);
			}
            return ht;
        }

        #endregion
    }
}
