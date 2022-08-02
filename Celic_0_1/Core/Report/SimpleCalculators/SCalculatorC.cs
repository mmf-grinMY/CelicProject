﻿using System;
using System.Collections.ObjectModel;
using Word = Microsoft.Office.Interop.Word;
using static Celic.HelpManager;

namespace Celic
{
    /// <summary> Упрощенная методика расчета приразломных целиков </summary>
    sealed class SCalculatorC : BaseCalculator
    {
        #region Private Fields

        /// <summary> Угол 60 градусов в радианах </summary>
        private static readonly float _delta0 = (float)Math.PI / 3;
        /// <summary> Угол падения плоскости сместителя (разломной зоны), определяемый согласно графическим приложениям </summary>
        private readonly float _alfa;
        /// <summary> Список разрабатываемых пластов </summary>
        private readonly ObservableCollection<Plast> _plasts = null;

        #endregion

        #region Constructors

        /// <summary> Главный конструктор </summary>
        /// <param name="model"> Модель расчета </param>
        public SCalculatorC(SCalcCViewModel model) : base()
        {
            _alfa = (float)Math.PI / 180 * model.Alfa.V;
            _plasts = model.Plasts;
        }
        /// <summary> Дополнительный конструктор для расчета без логирования </summary>
        /// <param name="plasts"> Список разрабатываемых пластов </param>
        /// <param name="angle"> Угол падения разломной плоскости </param>
        public SCalculatorC(ObservableCollection<Plast> plasts, EFloat angle)
        {
            _alfa = (float)Math.PI * angle.V / 180;
            _plasts = plasts;
        }

        #endregion

        #region Public Methods

        /// <summary> Расчет ширины приразломного целика без логирования </summary>
        /// <returns> Строка с выходными данными </returns>
        public string Count()
        {
            string output = "";
            B();
            for(int i = 0; i < _plasts.Count; i++)
            {
                output += $"Пласт №{i + 1}: Ширина со стороны лежачего склона равна {_plasts[i].Bl} м," +
                    $" со стороны висячего склона - {_plasts[i].Bv} м\n";
            }
            return output;
        }

        #endregion

        #region Private Calculation Methods

        /// <summary> Преобразование радиан в градусы </summary>
        /// <param name="angle"> Угол в радианах </param>
        /// <returns> Значение угла в градусах </returns>
        private static double ToGrad(double angle) => angle / Math.PI * 180;
        /// <summary> Расчет ширины приразломного предохранительного целика 
        /// со стороны лежачего крыла разломной зоны для одного пласта </summary>
        /// <param name="ht"> Значение высоты ЗВТ для данного пласта </param>
        /// <returns> Значение ширины приразломного целика со стороны лежачего крыла </returns>
        private float CalcBl(float ht) => 50 + (ht * (Ctg(_delta0) + Ctg(_alfa)));
        /// <summary> Расчет ширины приразломного предохранительного целика 
        /// со стороны висячего крыла разломной зоны для одного пласта </summary>
        /// <param name="ht"> Значение ЗВТ для данного пласта </param>
        /// <returns> Значение ширины приразломного целика со стороны висячего крыла </returns>
        private float CalcBv(float ht) => 50 + (ht * (Ctg(_delta0) - Ctg(_alfa)));
        /// <summary> Расчет приразломных предохранительных целиков без логирования </summary>
        private void B()
        {
            if(_plasts.Count == 2 || _plasts.Count == 3)
            {
                float[] ht = HtMorePlasts(_plasts);
                for (int i = 0; i < _plasts.Count; i++)
                {
                    _plasts[i].Bl = CalcBl(ht[i]);
                    _plasts[i].Bv = _alfa > _delta0 ? CalcBv(ht[i]) : 50;
                }
            }
            else
            {
                CallOrg();
            }
        }
        /// <summary> Расчет ширины приразломных предохранительных целиков </summary>
        /// <param name="reporter"> Модель, отслеживающая запись процесса расчета в файл </param>
        protected override void WriteCalculation(RepProViewModel reporter)
        {
            if ((_plasts.Count == 2 || _plasts.Count == 3) && _app != null)
            {
                float status = 65f, offset = 15f / _plasts.Count;
                float[] ht = HtMorePlastsWithLog(_plasts, reporter, 60f);
                AddParagraph($"Список состоит из {_plasts.Count}-х пластов. ");
                for (int i = 0; i < _plasts.Count; i++)
                {
                    AddParagraph($"Для пласта {i + 1} имеем:");
                    reporter.ResultReport = $"Вычисление величины ВЛ для пласта №{i + 1}";
                    AddParagraphCalcBl(i, ht);
                    reporter.StatusReport = (status += offset).ToString();
                    reporter.ResultReport = $"Вычисление величины ВВ для пласта №{i + 1}";
                    if (_alfa > _delta0)
                    {
                        AddParagraphAngle($"Так как угол α = {ToGrad(_alfa)} больше угла δ0 = 60," +
                            $" то величина ВВ вычисляется по формуле:");
                        AddParagraphCalcBv(i, ht);
                    }
                    else
                    {
                        AddParagraphAngle($"Так как угол α = {ToGrad(_alfa)} меньше угла δ0 = 60," +
                            $" то величина ВВ принимается равной 50 м:");
                        AddParagraphWithSelectedTwo($"ВВ = {_plasts[i].Bv = 50};", "ВВ");
                    }
                    reporter.StatusReport = (status += offset).ToString();
                }
            }
            else
            {
                CallOrg();
            }
        }

        #endregion

        #region TextManager Methods

        /// <summary> Запись параграфа с информацией о подсчете параметра ВЛ </summary>
        /// <param name="i"> Индекс пласта в списке </param>
        /// <param name="ht"> Массив высот ЗВТ </param>
        private void AddParagraphCalcBl(int i, float[] ht)
        {
            Word.Range range = _app.ActiveDocument.Paragraphs.Add().Range;
            range.Text = $"ВЛ = 50 + HT ∙ (ctgδ0 + ctgα) = 50 + {ht[i]} ∙ (ctg{ToGrad(_delta0)} + ctg{ToGrad(_alfa)})" +
                $" = {_plasts[i].Bl = CalcBl(ht[i])};";
            FormatVariable2("ВЛ");
            FormatVariable2("HT");
            FormatVariable2("δ0");
            Select("α", 1, 1).ItalicRun();
            range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
            range.InsertParagraphAfter();
            range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
        }
        /// <summary> Запись параграфа с информацией о подсчете параметра ВВ </summary>
        /// <param name="i"> Индекс пласта в списке </param>
        /// <param name="ht"> Массив высот ЗВТ </param>
        private void AddParagraphCalcBv(int i, float[] ht)
        {
            Word.Range range = _app.ActiveDocument.Paragraphs.Add().Range;
            range.Text = $"ВВ = 50 + HT ∙ (ctgδ0 - ctgα) = 50 + {ht[i]} ∙ (ctg{ToGrad(_delta0)} - ctg{ToGrad(_alfa)})" +
                $" = {_plasts[i].Bv = CalcBv(ht[i])};";
            FormatVariable2("ВВ");
            FormatVariable2("HT");
            FormatVariable2("δ0");
            Select("α", 1, 1).ItalicRun();
            range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphJustify;
            range.InsertParagraphAfter();
            range.ParagraphFormat.Alignment = Word.WdParagraphAlignment.wdAlignParagraphCenter;
        }
        /// <summary> Запись параграфа с информацией о сравнении углов при расчете ВВ </summary>
        /// <param name="text"> Записываемый текст </param>
        private void AddParagraphAngle(string text)
        {
            Word.Range range = _app.ActiveDocument.Paragraphs.Add().Range;
            range.Text = text;
            Select("α", 1, 0).ItalicRun();
            FormatVariable2("δ0");
            FormatVariable2("ВВ");
            range.InsertParagraphAfter();
        }
        /// <summary> Запись параграфа с двухбуквенным специальным куском текста </summary>
        /// <param name="text"> Записываемый текст </param>
        /// <param name="selectedTwo"> Специальный кусок </param>
        private void AddParagraphWithSelectedTwo(string text, string selectedTwo)
        {
            Word.Range range = _app.ActiveDocument.Paragraphs.Add().Range;
            range.Text = text;
            FormatVariable2(selectedTwo);
            range.InsertParagraphAfter();
        }

        #endregion
    }
}