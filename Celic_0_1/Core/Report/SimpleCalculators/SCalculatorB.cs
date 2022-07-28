using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Word = Microsoft.Office.Interop.Word;

namespace Celic
{
    /// <summary> Упрощенная методика расчета высоты распространения зоны техногенных
    /// водопроводящих трещин над выработанным пространством </summary>
    sealed class SCalculatorB : BaseCalculator
    {
        #region Private Fields

        /// <summary> Количество произведенных при расчете итераций </summary>
        private int _iter;
        /// <summary> Список разрабатываемых пластов </summary>
        private readonly ObservableCollection<Plast> _plasts;

        #endregion

        #region Constructors

        /// <summary> Главный конструктор, принимающий модель расчета </summary>
        /// <param name="model"> Модель расчета </param>
        public SCalculatorB(SCalcBViewModel model) : base() => _plasts = model.Plasts;
        /// <summary> Дополнительный конструктор для расчета без логирования </summary>
        /// <param name="plasts"> Коллекция разрабатываемых пластов </param>
        public SCalculatorB(ObservableCollection<Plast> plasts) => _plasts = plasts;

        #endregion

        #region Private Methods

        /// <summary> Расчет высоты ЗВТ без логирования информациии о произведенных расчетах </summary>
        /// <param name="h"> Старое значение высоты ЗВТ </param>
        /// <returns> Новое значение высоты ЗВТ </returns>
        private float CountHt(float h)
        {
            float ht = _plasts[0].Ht();
            for (int i = 1; i < _plasts.Count; i++)
                ht += h * _plasts[i].Ht() / (h + _plasts[i].Height - _plasts[0].Height);
            return ht;
        }
        /// <summary> Расчет высоты ЗВТ с логированием произведенных операций </summary>
        /// <param name="h"> Старое значение высоты ЗВТ </param>
        /// <returns> Новое значение высоты ЗВТ </returns>
        private float CountHtWithLog(float h)
        {
            _iter++;
            float ht = _plasts[0].Ht();
            string txt = "H_" + _iter + " = " + ht;
            for (int i = 1; i < _plasts.Count; i++)
            {
                Plast p = _plasts[i];
                ht += h * _plasts[i].Ht() / (h + _plasts[i].Height - _plasts[0].Height);
                h = (float)Math.Round(h * 100) / 100;
                txt += " + (" + h + "∙" + Math.Round(p.D() * 100) / 100 + "∙" + p.MPr() + "∙" +
                    p.S + "∙" + p.Sz + "∙" + p.Kt + ")/(" + h + " + " + (p.Height - _plasts[0].Height) + ")";
            }
            txt += " = " + Math.Round(ht * 100) / 100;
            _txt.Add(txt);
            return ht;
        }
        /// <summary> Расчет высоты ЗВТ с логированием произвдеенных операций </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        private float CountLog()
        {
            float oldHt, newHt;
            string txt = "Н_т = h_1 + (h d_2 m_пр2 S_2 S_z2 k_t2)/(h + ∆H_(1-2) ) + ... + (h d_n m_прn S_n S_zn k_tn)/(h + ∆H_(1-n) )";
            _txt.Add(txt);
            oldHt = _plasts[0].Ht();
            newHt = CountHtWithLog(oldHt);
            while (newHt - oldHt >= 2)
            {
                oldHt = newHt;
                newHt = CountHtWithLog(oldHt);
            }
            double delta = Math.Round((newHt - oldHt) * 100) / 100;
            newHt = (float)Math.Round(newHt * 100) / 100;
            oldHt = (float)Math.Round(oldHt * 100) / 100;
            txt = $"H_{ _iter} - H_{ _iter - 1} < 2 => { newHt} - { oldHt} = { delta} < 2 " +
                $"=> – условие соблюдается.Принимаем значение Н_т = { newHt} м.";
            _txt.Add(txt);
            return newHt;
        }
        /// <summary> Запись проведенных операций вычислений высоты ЗВТ </summary>
        /// <param name="reporter"> Модель, отслеживающая запись процесса расчета в файл </param>
        protected override void WriteCalculation(RepProViewModel reporter)
        {
            string tableWriteReport = "Создание и заполнение таблицы с данными";
            new TextManager(_app).Write(_plasts, CountLog(), reporter);
            TableManager tableManager = new TableManager(_app, _plasts);
            reporter.ResultReport = tableWriteReport;
            int rangeLength = tableManager.WriteTable(reporter);
            Word.Cell cell = tableManager.Cell;
            reporter.ResultReport = "Запись произведенных расчетов";
            AddParagraphMath(_txt, reporter);
            _app.ActiveDocument.Range(rangeLength - 6).Select();
            _app.Selection.Cut();
            cell.TopPadding = 12F;
            cell.Select();
            _app.Selection.Paste();
        }

        #endregion

        #region Public Methods

        /// <summary> Расчет высоты ЗВТ без логирования произвденных операций </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        public float Count()
        {
            float oldHt, newHt;
            oldHt = _plasts[0].Ht();
            newHt = CountHt(oldHt);
            while (newHt - oldHt >= 2)
            {
                oldHt = newHt;
                newHt = CountHt(oldHt);
            }
            newHt = (float)Math.Round(newHt * 100) / 100;
            return newHt;
        }

        #endregion

        #region Public Properties

        /// <summary> Список строк, содержащий произведенные при расчете операции </summary>
        public List<string> TextCount { get => _txt; }

        #endregion
    }
}