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
    	#region Private Fields
    	
		private static List<string> _txt = new List<string>();
		/// <summary> Минимально необходимая величина ПВП </summary>
		private static readonly float M = 35;
		
    	#endregion
		
        #region Private Static Methods
        
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
        private static float InnerRound(float value) 
        {
        	return (float)Math.Round(value * 100) / 100;
        }
        /// <summary> Расчет высоты ЗВТ с логированием произведенных операций </summary>
        /// <param name="h"> Старое значение высоты ЗВТ </param>
        /// <returns> Новое значение высоты ЗВТ </returns>
        private static float CountHtWithLog(float h, ObservableCollection<Plast> plasts, ref int iter)
        {
			iter++;
			float ht = PlastManager.Ht(plasts[0]);
			string txt = "H_" + iter + " = " + ht;
			Plast p_i;
			for (int i = 1; i < plasts.Count; i++) {
				p_i = plasts[i];
				ht += h * PlastManager.Ht(p_i) * p_i.S * p_i.Sz * p_i.Kt / (h + p_i.Main.H - plasts[0].Main.H);
				h = InnerRound(h);
				txt += " + (" + h + "∙" + InnerRound(PlastManager.CalcD(p_i)) + "∙" + PlastManager.MPr(p_i) + "∙" +
					p_i.S + "∙" + p_i.Sz + "∙" + p_i.Kt + ")/(" + h + " + " + (p_i.Main.H - plasts[0].Main.H) + ")";
			}
			txt += " = " + InnerRound(ht);
			_txt.Add(txt);
			p_i = null;
			return ht;
		}
		/// <summary> Расчет высоты ЗВТ с логированием произведенных операций </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        private static float CountHtLog(ObservableCollection<Plast> plasts)
        {
			int iter = 0;
            float oldHt, newHt;
            _txt.Add("Н_т = h_1 + (h d_2 m_пр2 S_2 S_z2 k_t2)/(h + ∆H_(1-2) ) + ... + (h d_n m_прn S_n S_zn k_tn)/(h + ∆H_(1-n) )");
            oldHt = PlastManager.Ht(plasts[0]);
            newHt = CountHtWithLog(oldHt, plasts, ref iter);
            while (newHt - oldHt >= 2) {
                oldHt = newHt;
                newHt = CountHtWithLog(oldHt, plasts, ref iter);
            }
            newHt = InnerRound(newHt);
            oldHt = InnerRound(oldHt);
            _txt.Add(iter + " - H_" + (iter - 1) + " < 2 => " + newHt+ " - " + oldHt + " = " + InnerRound(newHt - oldHt) + " < 2 => – условие соблюдается.Принимаем значение Н_т = " + newHt + " м.");
            return newHt;
        }
		
        #endregion

        #region Public Methods

        /// <summary> Метод для расчета итогового значения ЗВТ </summary>
        /// <returns> Итоговое значение ЗВТ </returns>
        public static float Count(ObservableCollection<Plast> plasts) {
            float ht = 0;
            if (plasts != null && (ht = CountHt(plasts)) <= M) {
				PlastCollectionManager.RecalcAllParams(plasts);
				ht = CountHt(plasts);
			}
            return ht;
        }
		/// <summary> Метод для расчета итогового значения ЗВТ </summary>
        /// <returns> Итоговое значение ЗВТ </returns>
        public static float CountLog(ObservableCollection<Plast> plasts) {
            float ht = 0;
            if (plasts != null && (ht = CountHtLog(plasts)) <= M) {
				_txt.Add("Был произведен дополнительный расчет коэффициентов пласта.");
				PlastCollectionManager.RecalcAllParams(plasts);
				ht = CountHt(plasts);
			}
            return ht;
        }

        #endregion
        
        /// <summary> Запись проведенных операций вычислений высоты ЗВТ </summary>
        /// <param name="reporter"> Модель, отслеживающая запись процесса расчета в файл </param>
        public static void WriteCalculation(ObservableCollection<Plast> plasts, Word.Application app)
        {
            new TextManager(app).Write(plasts, CountLog(plasts));
            TableManager tableManager = new TableManager(app, plasts);
            int rangeLength = tableManager.WriteTable();
            Word.Cell cell = tableManager.Cell;
            BaseTextManager.AddParagraphMath(_txt, app);
            app.ActiveDocument.Range(rangeLength - 6).Select();
            app.Selection.Cut();
            cell.TopPadding = 12F;
            cell.Select();
            app.Selection.Paste();
        }

        #region Public Properties

        /// <summary> Список строк, содержащий произведенные при расчете операции </summary>
        public static List<string> TextCount 
        {
        	get 
        	{
        		return _txt;
        	}
        }
        
        #endregion
    }
}
