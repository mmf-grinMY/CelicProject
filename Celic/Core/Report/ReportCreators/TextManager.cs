﻿using System.Collections.ObjectModel;
using Word = Microsoft.Office.Interop.Word;
using Align = Microsoft.Office.Interop.Word.WdParagraphAlignment;
using Help = Celic.HelpManager;
using System;
using PM = Celic.PlastManager;

namespace Celic
{
    /// <summary> Запись данных о проведении расчетов для SCaltularoB </summary>
    class TextManager : BaseTextManager
    {
        #region Private Fields

        /// <summary> Выбранный пласт ( пласт, с которым сейчас работают методы ) </summary>
        private Plast _p = null;

        #endregion

        #region Constructors

        /// <summary> Главный конструктор для данного класса </summary>
        /// <param name="application"> Запущенное приложение Microsoft Office Word </param>
        public TextManager(Word.Application application) 
        {
        	_app = application;
        }

        #endregion

        #region Public Methods

        /// <summary> Запись текстовой части отчета </summary>
        /// <param name="plasts"> Коллекция рассматриваемых пластов </param>
        /// <param name="result"> Значение высоты ЗВТ </param>
        /// <param name="reporter"> Модель, отслеживающая запись процесса расчета в файл </param>
        public void Write(ObservableCollection<Plast> plasts, double result)
        {
            _p = plasts[0];
            WriteCalculation();
            AddParagraph("С учетом сближенных слоев - " + result + " м", align: Align.wdAlignParagraphJustify);
        }

        #endregion

        #region Formating Text ( Private Methods )

        /// <summary> Форматирование текста заголовка файла </summary>
        private void FormatTitle() 
        {
        	_app.ActiveDocument.Paragraphs.Last.Range.Bold = 1;
        }
        /// <summary> Форматирование текста подзаголовка </summary>
        private void FormatHeader()
        {
            _range = _app.ActiveDocument.Paragraphs.Last.Range;
            _range.Bold = 0;
            _range.Italic = 1;
            _range.Underline = Word.WdUnderline.wdUnderlineSingle;
            _range.ParagraphFormat.CharacterUnitFirstLineIndent = 3;
        }
        /// <summary> Форматирование текста line1 </summary>
        private void FormatLine1()
        {
            _range = _app.ActiveDocument.Paragraphs.Last.Range;
            _range.Bold = 0;
            _range.Italic = 0;
            _range.Underline = Word.WdUnderline.wdUnderlineNone;
            Select(_p.Gorizont.ToString() + "-м", (_p.Gorizont.ToString() + "-м").Length).BoldRun();
            _app.ActiveDocument.Paragraphs.Last.Range.ParagraphFormat.CharacterUnitFirstLineIndent = 5;
        }
        /// <summary> Форматирование текста listLine1 </summary>
        private void FormatListLine1()
        {
            FormatVariable2("mв ");
            Select(_p.Main.Mv.ToString(), _p.Main.Mv.ToString().Length).BoldRun();
        }
        /// <summary> Форматирование текста listLine2 </summary>
        private void FormatListLine2() 
        {
        	Select(_p.Main.H.ToString(), _p.Main.H.ToString().Length).BoldRun();
        }
        /// <summary> Форматирование текста listLine32 </summary>
        private void FormatListLine32()
        {
            Select((_p.Main as Camera).Ki.ToString(), (_p.Main as Camera).Ki.ToString().Length).BoldRun();
            FormatVariable2("kи ");
        }
        /// <summary> Форматирование текста line2 </summary>
        private void FormatLine2() 
        {
        	Select(_p.Gorizont + "-м", (_p.Gorizont + "-м").Length).BoldRun();
        }
        /// <summary> Форматирование текста explanation1 </summary>
        private void FormatExplanation1() 
        {
        	FormatVariable2("mпр");
        }
        /// <summary> Форматирование текста explanation2 </summary>
        private void FormatExplanation2() 
        {
        	FormatVariable2("mв");
        }
        /// <summary> Форматирование текста explanation3 </summary>
        private void FormatExplanation3() 
        {
        	FormatVariable2("kи");
        }
        /// <summary> Форматирование текста explanation4 </summary>
        private void FormatExplanation4()
        {
            Select("k - коэффициент").ItalicRun();
            Select(" k = 1").ItalicRun();
        }
        /// <summary> Форматирование текста line3 </summary>
        private void FormatLine3()
        {
            Select(_p.Gorizont + "-му", 4).BoldRun();
            Select(_p.Main.Mv.ToString(), _p.Main.Mv.ToString().Length).BoldRun();
            FormatVariable2("kи ");
            Select((_p.Main as Camera).Ki.ToString(), (_p.Main as Camera).Ki.ToString().Length).BoldRun();
        }
        /// <summary> Форматирование текста formula1 </summary>
        private void FormatFormula1()
        {
            // "HT = d ∙ mпр,"
            FormatVariable2("HT");
            FormatVariable2("mпр");
            Select("d").ItalicRun();
        }
        /// <summary> Форматирование текста formula2 </summary>
        private void FormatFormula2()
        {
            // "mпр = mв ∙ kи ∙ k,"
            FormatVariable2("mпр");
            FormatVariable2("mв");
            FormatVariable2("kи");
            Select("k,", 1, 0).ItalicRun();
        }
        /// <summary> Форматирование текста formula3 </summary>
        private void FormatFormula3()
        {
            // $"mпр = mв ∙ kи ∙ k = {_p.Mv} ∙ {_p.Ki} ∙ {_p.K} = {_p.MPr()}"
            FormatVariable2("mпр");
            FormatVariable2("mв");
            FormatVariable2("kи");
            Select("k,", 1, 0).ItalicRun();
            Select(_p.Main.Mv + " ∙ " + (_p.Main as Camera).Ki + " ∙ " + _p.Main.K + " = " + PlastManager.MPr(_p),
                   (_p.Main.Mv + " ∙ " + (_p.Main as Camera).Ki + " ∙ " + _p.Main.K + " = " + PlastManager.MPr(_p)).Length).BoldRun();
        }
        /// <summary> Форматирование текста formula4 </summary>
        private void FormatFormula4()
        {
            // $"d = {GetD(_p)} – 0.01 ∙ Н = {GetD(_p)} – 0.01 ∙ {_p.H} = {_p.D()}"
            string D = PlastManager.GetD(_p);
            Select("d").ItalicRun();
            Select("Н").ItalicRun();
            Select(D + " – 0.01 ∙ Н", 9).BoldRun();
            Select(D + " – 0.01 ∙ " + _p.Main.H, (D + " – 0.01 ∙ " + _p.Main.H).Length).BoldRun();
            Select(PlastManager.CalcD(_p).ToString(), PlastManager.CalcD(_p).ToString().Length).BoldRun();
        }
        /// <summary> Форматирование текста formula5 </summary>
        private void FormatFormula5()
        {
            // $"НТ = d ∙ mпр = {_p.D()} ∙ {_p.MPr()} = {_p.Ht()} м."
            FormatVariable2("НТ");
            Select("d").ItalicRun();
            FormatVariable2("mпр");
            Select(PlastManager.CalcD(_p) + " ∙ " + PlastManager.MPr(_p) + " = " + PlastManager.Ht(_p),
                   (PlastManager.CalcD(_p) + " ∙ " + PlastManager.MPr(_p) + " = " + PlastManager.Ht(_p)).Length).ItalicRun();
        }

        #endregion

        #region Protected Method

        /// <summary> Запись части текстовой части отчета </summary>
        /// <param name="reporter"> Модель, отслеживающая запись процесса расчета в файл </param>
        protected override void WriteCalculation()
        {
            AddParagraph("Описание горных работ", FormatTitle, Align.wdAlignParagraphCenter, SpaceParagraph.missing);
            AddParagraph("3.1. Расчёт высоты распространения водопроводящих трещин над разрабатываемым пластом",
                FormatHeader, Align.wdAlignParagraphCenter, SpaceParagraph.standart);
            AddParagraph("Очистные работы на " + _p.Gorizont + "-м калийном горизонте при " +
                (_p.TypeDev == "столбовая" ? "столбовой" : "камерной") +
                " системе отработки будут проводиться со следующими параметрами:", FormatLine1);
            AddParagraph("Выемочная мощность " + Tab(5) + "-" +  Tab(1) + "mв = " + _p.Main.Mv + " м;", FormatListLine1, spaceAfter: SpaceParagraph.missing);
            AddParagraph("Глубина ведения горных работ" + Tab(3) + "-" + Tab(1) + _p.Main.H + " м;", FormatListLine2);
            AddParagraph("Коэффициент извлечения рудной массы");
            AddParagraph("в пределах вынимаемой мощности" + Tab(3) + " - " + Tab(1) + " kи = " +  (_p.Main as Camera).Ki + ".",
                FormatListLine32, spaceAfter: SpaceParagraph.doubleSpace);
            AddParagraph("Высота зоны распространения трещин над " + _p.Gorizont + "-м калийным горизонтом составит:",
                FormatLine2, spaceAfter: SpaceParagraph.missing);
            AddParagraph("HT = d ∙ mпр,", FormatFormula1, Align.wdAlignParagraphCenter);
            AddParagraph("mпр – приведенная вынимаемая мощность пласта.", FormatExplanation1);
            AddParagraph("mпр = mв ∙ kи ∙ k,", FormatFormula2, Align.wdAlignParagraphCenter);
            AddParagraph("mв – вынимаемая мощность пласта,", FormatExplanation2);
            AddParagraph("kи – коэффициент извлечения рудной массы в пределах вынимаемой мощности,", FormatExplanation3);
            AddParagraph("k – коэффициент, учитывающий размер выработанного пространства, принимаем k = 1.", FormatExplanation4);
            AddParagraph("Принимая выемочную мощность по " + _p.Gorizont + "-му калийному горизонту равную " +
                _p.Main.Mv + " метра, а коэффициент извлечения kи = " + (_p.Main as Camera).Ki + " получаем:", FormatLine3);
            AddParagraph("mпр = mв ∙ kи ∙ k = " + _p.Main.Mv + " ∙ " + (_p.Main as Camera).Ki + " ∙ " + _p.Main.K + " = " + PM.MPr(_p), FormatFormula3, Align.wdAlignParagraphCenter);
            AddParagraph("Определяем параметр d согласно приложению Б [1] в зависимости от глубины и системы разработки:");
            AddParagraph("d = " + PM.GetD(_p) + " – 0.01 ∙ Н = " + PM.GetD(_p) + " – 0.01 ∙ " + _p.Main.H+  " = " + PM.CalcD(_p),
                FormatFormula4, Align.wdAlignParagraphCenter);
            AddParagraph("Тогда:", align: Align.wdAlignParagraphJustify);
            AddParagraph("НТ = d ∙ mпр = " + PM.CalcD(_p) + " ∙ " + PM.MPr(_p) + " = " 
                         + PM.Ht(_p) + " м.", FormatFormula5, Align.wdAlignParagraphCenter);
        }

        #endregion
    }
}
