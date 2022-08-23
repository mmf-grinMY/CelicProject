using System.Collections.Generic;
using System.Collections.ObjectModel;
using Help = Celic.HelpManager;
using System.Windows;
using PM = Celic.PlastManager;

namespace Celic
{
    /// <summary> Логика вычисления высот ЗВТ с документированием в файл *.docx и без него </summary>
    abstract class BaseCalculator : BaseTextManager
    {
        #region Protected Fields

        /// <summary> Список строк, содержащий произведенные при расчете операции </summary>
        protected readonly List<string> _txt;

        #endregion

        #region Contructors

        /// <summary> Основной конструктор для класса BaseCalculator </summary>
        public BaseCalculator() 
        {
        	_txt = new List<string>();
        }

        #endregion

        #region Private Methods

        /// <summary> Вычисление высот ЗВТ для 2 пластов для расчетов C и D </summary>
        /// <param name="plasts"> Коллекция разрабатываемых пластов </param>
        /// <returns> Массив значений высот ЗВТ </returns>
        private float[] HtMorePlasts2(ObservableCollection<Plast> plasts)
        {
            float[] ht;
            if (plasts.Count == 2)
            {
                ht = new float[plasts.Count];
                if (!PlastManager.IsContiguous(plasts[0], plasts[1]))
                {
                    ht[0] = PlastManager.Ht(plasts[0]);
                    ht[1] = PlastManager.Ht(plasts[1]);
                }
                else
                {
                    ht[1] = PlastManager.Ht(plasts[1]);
                    ht[0] = Help.SimpleCalcHt(0, 1, plasts);
                }
            }
            else
            {
                ht = null;
            }
            return ht;
        }
        /// <summary> Вычисление высот ЗВТ для 3 пластов для расчетов C и D </summary>
        /// <param name="plasts"> Коллекция разрабатываемых пластов </param>
        /// <returns> Массив значений высот ЗВТ </returns>
        private float[] HtMorePlast3(ObservableCollection<Plast> plasts)
        {
            float[] ht;
            if (plasts.Count == 3)
            {
                ht = new float[plasts.Count];
                float dh = plasts[2].Main.H - plasts[1].Main.H;
                bool simpleCalcHt1 = ht[2] <= dh;
                ht[2] = PlastManager.Ht(plasts[2]);
                ht[1] = ht[2] <= dh ? PlastManager.Ht(plasts[1]) : Help.SimpleCalcHt(1, 2, plasts);
                ht[0] = ht[1] <= plasts[1].Main.H - plasts[0].Main.H ? PlastManager.Ht(plasts[0]) : Help.SimpleCalcHt(0, simpleCalcHt1 == true ? 1 : 2, plasts);
            }
            else
            {
                ht = null;
            }
            return ht;
        }
        /// <summary> Вычисление высоты ЗВТ для 2 пластов при расчетах типа C и D с записью в документ *.docx произведенных расчетов </summary>
        /// <param name="plasts"> Коллекция разрабатываемых пластов </param>
        /// <param name="reporter"> Процесс, следящий за ходом выполнения расчета </param>
        /// <param name="offset"> Процентное смещение ( число процентов, на которые стоит увеличить ProgressBar при выполнении отдельной операции) </param>
        /// <param name="status"> Состояние процесса вычисления на момент начала выполнения функции </param>
        /// <returns> Массив значений высот ЗВТ </returns>
        private float[] HtMorePlast2WithLog(ObservableCollection<Plast> plasts)
        {
            float[] ht;
            if (plasts.Count == 2)
            {
                AddParagraph("Список состоит из двух разрабатываемых пластов.");
                ht = new float[plasts.Count];
                if (!PlastManager.IsContiguous(plasts[0], plasts[1]))
                {
                    AddParagraph("Так как пласты не являются сближенными, " +
                        "высоты ЗВТ вычисляются для каждого пласта без учета влияния выработки другого:");
                    for (int i = 0; i < plasts.Count; i++)
                    {
                        ht[i] = PlastManager.Ht(plasts[i]);
                        AddParagraphCalcHt(i, ht, plasts);
                    }
                }
                else
                {
                    AddParagraph("Пласты являются сближенными по условию водозащиты. ");
                    AddParagraph("Так как пласты являются сближенными, высоты ЗВТ для верхнего" +
                        " пласта необходимо подсчитать с учетом выработки нижнего пласта: ");
                    ht[0] = Help.SimpleCalcHt(0, 1, plasts, _txt);
                    AddParagraphMath(_txt, _app);
                    AddParagraph("Величина ЗВТ для нижнего пласта рассчитывается без учета влияния выработки вышележащего: ");
                    AddParagraphCalcHt(1, ht, plasts);
                }
            }
            else
            {
                ht = null;
            }
            return ht;
        }
        /// <summary> Вычисление высоты ЗВТ для 3 пластов при расчетах типа C и D с записью в документ *.docx произведенных расчетов </summary>
        /// <param name="plasts"> Коллекция разрабатываемых пластов </param>
        /// <param name="reporter"> Процесс, следящий за ходом выполнения расчета </param>
        /// <param name="offset"> Процентное смещение ( число процентов, на которые стоит увеличить ProgressBar при выполнении отдельной операции) </param>
        /// <param name="status"> Состояние процесса вычисления на момент начала выполнения функции </param>
        /// <returns> Массив значений высот ЗВТ </returns>
        private float[] HtMorePlast3WithLog(ObservableCollection<Plast> plasts)
        {
            float[] ht;
            if (plasts.Count == 3)
            {
                ht = new float[plasts.Count];
                double dh12 = plasts[1].Main.H - plasts[0].Main.H;
                double dh23 = plasts[2].Main.H - plasts[1].Main.H;
                string begin = "Список состоит из трех разрабатываемых пластов." +
                                    " Для удобства назовем их пласт 1, пласт 2 и пласт 3 ( перечисление идет вниз по списку)";
                string plast21 = "Так как величина HT3 = " + ht[2] + " меньше величины междупластья ∆H2-3 = " + dh23 +", " +
                                    "то величина HT2 рассчитывается без учета влияния выработки остальных пластов.";
                string plast22 = "Так как величина HT3 = " + ht[2] + " больше величины междупластья ∆H2-3 = " + dh23 +", " +
                                    "то величина HT2 рассчитывается с учетом влияния выработки нижележащего пласта.";
                string plast31 = "Так как величина HT2 = " + ht[1] + " меньше величины междупластья ∆H1-2 = " + dh12 +", " +
                                    "то величина HT1 рассчитывается без учета влияния выработки остальных пластов.";
                string plast321 = "Так как величина НT2 рассчитывалась без учета влияния отработки остальных пластов," +
                                    " то величина HT1 рассчитывается с учетом влияния выработки пласта 1 и пласта 2.";
                string plast322 = "Так как величина НT2 рассчитывалась с учетом влияния отработки нижележащего пласта," +
                                    " то величина HT1 рассчитывается с учетом влияния выработки остальных пластов.";
                AddParagraph(begin);
                AddParagraphCalcHt(2, ht, plasts);
                bool simpleCalcHt1 = ht[2] <= dh23;
                if (ht[2] <= dh23)
                {
                    AddParagraphSpecial(plast21, "HT3", "HT2", "∆H2-3");
                    AddParagraphCalcHt(1, ht, plasts);
                }
                else
                {
                    AddParagraphSpecial(plast22, "HT3", "HT2", "∆H2-3");
                    ht[1] = Help.SimpleCalcHt(1, 2, plasts, _txt);
                    AddParagraphMath(_txt, _app);
                }
                if (ht[1] <= dh12)
                {
                    AddParagraphSpecial(plast31, "HT1", "HT2", "∆H1-2");
                    AddParagraphCalcHt(0, ht, plasts);
                }
                else
                {
                    AddParagraphWithSelectedThree(simpleCalcHt1 == true ? plast321 : plast322, "HT1", "HT2");
                    ht[0] = Help.SimpleCalcHt(0, simpleCalcHt1 == true ? 1 : 2, plasts, _txt);
                    AddParagraphMath(_txt, _app);
                }
            }
            else
            {
                ht = null;
            }
            return ht;
        }

        #endregion

        #region Protected Methods

        /// <summary> Обработка ситуации, не предусматривающейся правилами рассчета </summary>
        protected float CallOrg()
        {
            MessageBox.Show("Программа не может произвести расчет для данной ситуации! Пожалуйста обратитесь к специализированной организации.");
            return 0;
        }
        /// <summary> Вычисление высот ЗВТ для коллекции разрабатываемых пластов </summary>
        /// <param name="plasts"> Коллекция разрабатываемых пластов </param>
        /// <returns> Массив значений высот ЗВТ </returns>
        protected float[] HtMorePlasts(ObservableCollection<Plast> plasts)
        {
            float[] ht = new float[plasts.Count];
            switch (plasts.Count)
            {
                case 1:
                    ht[0] = PlastManager.Ht(plasts[0]);
                    break;
                case 2:
                    ht = HtMorePlasts2(plasts);
                    break;
                case 3:
                    ht = HtMorePlast3(plasts);
                    break;
                default:
                    return null;
            }
            return ht;
        }
        /// <summary> Вычисление высоты ЗВТ для 3 пластов при расчетах типа C и D с записью в документ *.docx произведенных расчетов </summary>
        /// <param name="plasts"> Коллекция разрабатываемых пластов </param>
        /// <param name="reporter"> Процесс, следящий за ходом выполнения расчета </param>
        /// <param name="offset"> Процентное смещение ( число процентов, на которые стоит увеличить ProgressBar при выполнении отдельной операции) </param>
        /// <param name="status"> Состояние процесса вычисления на момент начала выполнения функции ( по умолчанию равно 5 ) </param>
        /// <returns> Массив значений высот ЗВТ </returns>
        protected float[] HtMorePlastsWithLog(ObservableCollection<Plast> plasts)
        {
            float[] ht = new float[plasts.Count];
            switch (plasts.Count)
            {
                case 1:
                    ht[0] = PlastManager.Ht(plasts[0]);
                    break;
                case 2:
                    ht = HtMorePlast2WithLog(plasts);
                    break;
                case 3:
                    ht = HtMorePlast3WithLog(plasts);
                    break;
                default:
                    return null;
            }
            return ht;
        }

        #endregion
    }
}
