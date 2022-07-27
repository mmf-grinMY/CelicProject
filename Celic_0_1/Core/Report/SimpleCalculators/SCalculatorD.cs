using System.Collections.ObjectModel;
using Word = Microsoft.Office.Interop.Word;

namespace Celic
{
    /// <summary> Упрощенная методика расчета ширины междубарьерного целика </summary>
    class SCalculatorD : BaseCalculator
    {
        #region Private Fields

        /// <summary> Список разрабатываемых слева пластов </summary>
        private readonly ObservableCollection<Plast> _leftPlasts = null;
        /// <summary> Список разрабатываемых справа пластов </summary>
        private readonly ObservableCollection<Plast> _rightPlasts = null;

        #endregion

        #region Constructors

        /// <summary> Главный конструктор для расчета ширины междубарьерного целика </summary>
        /// <param name="model"> Модель расчета </param>
        public SCalculatorD(SCalcDViewModel model)
        {
            _leftPlasts = model.LeftPlasts;
            _rightPlasts = model.RightPlasts;
        }
        /// <summary> Дополнительный конструктор для расчета без логирования </summary>
        /// <param name="leftPlasts"> Список разрабатываемых слева пластов </param>
        /// <param name="rightPlasts"> Список разрабатываемых справа пластов </param>
        public SCalculatorD(ObservableCollection<Plast> leftPlasts, ObservableCollection<Plast> rightPlasts)
        {
            _leftPlasts = leftPlasts;
            _rightPlasts = rightPlasts;
        }

        #endregion

        #region Private Calculation Methods

        /// <summary> Расчет ширины междубарьерного целика с логированием </summary>
        /// <param name="reporter"> Модель, отслеживающая запись процесса расчета в файл </param>
        protected override void WriteCalculation(RepProViewModel reporter)
        {
            float[] bMin = new float[_leftPlasts.Count];
            AddParagraph("Вычислим величины высот ЗВТ для пластов, лежащих справа от разлома");
            float[] htRight = HtMorePlastsWithLog(_rightPlasts, reporter, 30f);
            AddParagraph("Вычислим величины высот ЗВТ для пластов, лежащих слева от разлома");
            float[] htLeft = HtMorePlastsWithLog(_leftPlasts, reporter, 30f, 35f);
            float status = 65f, offset = 30f / htLeft.Length;
            for (int i = 0; i < htLeft.Length; i++)
            {
                reporter.ResultReport = $"Вычисление ширины междубарьерного целика на уровне пласта № {i + 1}";
                bMin[i] = 0.18F * (htRight[i] + htLeft[i]) + 66;
                AddParagraphBMin(htLeft[i], htRight[i], bMin[i]);
                reporter.StatusReport = (status += offset).ToString();
            }
        }
        /// <summary> Расчет ширины междубарьерного целика с логированием </summary>
        /// <returns> Массив значений ширины междубарьерного целика</returns>
        private float[] BMin()
        {
            float[] bMin = new float[_leftPlasts.Count];
            float[] htRight = HtMorePlasts(_rightPlasts);
            float[] htLeft = HtMorePlasts(_leftPlasts);
            for (int i = 0; i < htLeft.Length; i++)
            {
                bMin[i] = 0.18f * (htRight[i] + htLeft[i]) + 66;
            }
            return bMin;
        }

        #endregion

        #region Public Methods

        /// <summary> Расчет ширины междубарьерного целика с логированием </summary>
        /// <returns> Информация о выходных данных </returns>
        public string Count()
        {
            string output = "";
            float[] bMin = BMin();
            for(int i = 0; i < _leftPlasts.Count; i++)
            {
                output += $"На уровне пласта № {i + 1} ширина междубарьерного целика равна {bMin[i]} м.\n";
            }
            return output;
        }

        #endregion

        #region TextManager Methods

        /// <summary> Запись параграфа с информацией о расчете ширины приразломного целика для пары пластов </summary>
        /// <param name="ht1"> Значение высоты ЗВТ для первого пласта </param>
        /// <param name="ht2"> Значение высоты ЗВТ для второго пласта </param>
        /// <param name="bmin"> Значение ширины приразломного целика </param>
        private void AddParagraphBMin(double ht1, double ht2, double bmin)
        {
            Word.Paragraph par = _app.ActiveDocument.Paragraphs.Add();
            par.Range.Text = $"Bmin = 66 + 0,18 ⋅ (HT1 + HT2) = 66 + 0,18 ⋅ ({ht1} + {ht2}) = {bmin} м.";
            FormatVariable2("Bmin");
            FormatVariable2("HT1");
            FormatVariable2("HT2");
            par.Range.InsertParagraphAfter();
        }

        #endregion
    }
}
