using System;
using System.Collections.Generic;
using Word = Microsoft.Office.Interop.Word;
using System.Collections.ObjectModel;
using Align = Microsoft.Office.Interop.Word.WdParagraphAlignment;
using Microsoft.Win32;
using System.IO;

namespace Celic
{
    /// <summary> Основная работа с текстом при генерации отчета </summary>
    abstract class BaseTextManager
    {
        #region Protected Fields

        /// <summary> Открытое приложение Microsoft Office Word </summary>
        protected Word.Application _app = null;
        /// <summary> Константа отступа абзаца ( нормальный отступ ) </summary>
        protected const float SPACE_PARAGRAPH = 8f;
        /// <summary> Константа отступа абзаца ( нулевой отступ ) </summary>
        protected const float NULL_SPACE_PARAGRAPH = 0f;
        /// <summary> Интервал текста </summary>
        protected Word.Range _range = null;

        #endregion

        #region Protected Methods

        /// <summary> Выделение текста из последнего параграфа </summary>
        /// <param name="start"> Выделяемый текст </param>
        /// <param name="length"> Длина выделяемого текста </param>
        /// <param name="offset"> Количество симолов, на которые необходимо сместиться </param>
        /// <returns> Выделенная область </returns>
        protected Word.Selection Select(string start, int length = 1, int offset = 0)
        {
            Word.Document doc = _app.ActiveDocument;
            string txt = _range.Text;
            int index = txt.IndexOf(start);
            if (index != -1)
            {
                index += doc.Range().Text.Length - txt.Length + offset;
                doc.Range(index, index + length).Select();
            }
            return _app.Selection;
        }
        /// <summary> Добавление параграфа в документ </summary>
        /// <param name="text"> Содержание параграфа </param>
        /// <param name="form"> Форматирование текста </param>
        /// <param name="align"> Позиция текста на странице </param>
        /// <param name="spaceAfter"> Интервал после параграфа </param>
        protected void AddParagraph(string text, Formating form = null, Align align = Align.wdAlignParagraphJustify, float spaceAfter = SPACE_PARAGRAPH)
        {
            _range = _app.ActiveDocument.Paragraphs.Add().Range;
            _range.Text = text;
            _range.ParagraphFormat.Alignment = align;
            _app.ActiveDocument.Paragraphs.Last.SpaceAfter = spaceAfter;
            form?.Invoke();
            _range.InsertParagraphAfter();
        }
        /// <summary> Табуляция </summary>
        /// <param name="repeat"> Количество повторений табуляции </param>
        /// <returns> Строка с заданным количеством табуляций </returns>
        protected string Tab(int repeat)
        {
            string result = "";
            for (int i = 0; i < repeat; i++)
                result += "	";
            return result;
        }
        /// <summary> Форматирование специального двухбуквенного выражения </summary>
        /// <param name="start"> Строковое представление выражения </param>
        protected void FormatVariable2(string start)
        {
            Word.Document doc = _app.ActiveDocument;
            Word.Selection select = _app.Selection;
            string txt = _range.Text;
            int index = txt.IndexOf(start);
            if (index != -1)
            {
                int length = start.Length;
                index += doc.Range().Text.Length - txt.Length + length;
                doc.Range(index - length, index).Select();
                select.ItalicRun();
                length--;
                doc.Range(index - length, index).Select();
                select.Font.Subscript = 1;
            }
        }
        /// <summary> Простая запись параграфа </summary>
        /// <param name="text"> Записываемый текст </param>
        protected void AddParagraph(string text)
        {
            _range = _app.ActiveDocument.Paragraphs.Add().Range;
            _range.Text = text;
            _range.InsertParagraphAfter();
        }
        /// <summary> Запись расчета высоты ЗВТ </summary>
        /// <param name="i"> Индекс пласта в списке </param>
        /// <param name="ht"> Массив высот ЗВТ </param>
        /// <param name="plasts"> Коллекция разрабатываемых пластов </param>
        protected void AddParagraphCalcHt(int i, float[] ht, ObservableCollection<Plast> plasts)
        {
            PlastManager manager = new PlastManager(plasts[i]);
            _range = _app.ActiveDocument.Paragraphs.Add().Range;
            _range.Text = $"HT = d ∙ mпр = {manager.CalcD()} ∙ {manager.MPr()} = {ht[i] = manager.Ht()} м;";
            FormatVariable2("HT");
            Select("d ∙ mпр", 7, 0).ItalicRun();
            Select("mпр", 2, 1).Font.Subscript = 1;
            _range.ParagraphFormat.Alignment = Align.wdAlignParagraphJustify;
            _range.InsertParagraphAfter();
            _range.ParagraphFormat.Alignment = Align.wdAlignParagraphCenter;
        }
        /// <summary> Запись математических формул и их корректное форматирование </summary>
        protected void AddParagraphMath(List<string> mathText, RepProViewModel reporter = null)
        {
            float status = 79f, offset = 20f / mathText.Count;
            Word.OMaths math = _app.ActiveDocument.OMaths;
            for (int i = 0; i < mathText.Count; i++)
            {
                _range = _app.ActiveDocument.Paragraphs.Add().Range;
                _range.Text = mathText[i];
                _range.Font.Size = 9;
                math.Add(_range);
                _range.InsertParagraphAfter();
                if(reporter != null)
                    reporter.StatusReport = (status += offset).ToString();
            }
            math.BuildUp();
            mathText.Clear();
            if(reporter != null)
                reporter.StatusReport = 100.ToString();
        }
        /// <summary> Запись параграфа со специальным текстом </summary>
        /// <param name="text"> Записываемый текст </param>
        /// <param name="selected1"> Первый специальный кусок текста </param>
        /// <param name="selected2"> Второй специальный кусок текста </param>
        /// <param name="selected3"> Третий специальный кусок текста </param>
        protected void AddParagraphSpecial(string text, string selected1, string selected2, string selected3)
        {
            _range = _app.ActiveDocument.Paragraphs.Add().Range;
            _range.Text = text;
            FormatVariable3(selected1);
            FormatVariable3(selected2);
            Select(selected3, 5, 0).ItalicRun();
            Select(selected3, 3, 2).Font.Subscript = 1;
            _range.InsertParagraphAfter();
        }
        /// <summary> Запись параграфа с двумя трехбуквенными специальными кусками текста </summary>
        /// <param name="text"> Записываемый текст </param>
        /// <param name="selected1"> Первый специальный кусок текста </param>
        /// <param name="selected2"> Второй специальный кусок текста </param>
        protected void AddParagraphWithSelectedThree(string text, string selected1, string selected2)
        {
            _range = _app.ActiveDocument.Paragraphs.Add().Range;
            _range.Text = text;
            FormatVariable3(selected1);
            FormatVariable3(selected2);
            _range.InsertParagraphAfter();
        }
        /// <summary> Форматирование специальных параметров </summary>
        /// <param name="start"> Строковое представление параметра </param>
        protected void FormatVariable3(string start)
        {
            Word.Document doc = _app.ActiveDocument;
            string txt = _range.Text;
            Word.Selection select = _app.Selection;
            int index = txt.IndexOf(start);
            if (index != -1)
            {
                int offset = 0, length = 3;
                int rLength = doc.Range().Text.Length - txt.Length;
                doc.Range(index + rLength + offset, index + rLength + length + offset).Select();
                select.ItalicRun();
                length = offset = 1;
                doc.Range(index + rLength + offset, index + rLength + length + offset).Select();
                select.Font.Subscript = 1;
                offset = 2;
                doc.Range(index + rLength + offset, index + rLength + length + offset).Select();
                select.Font.Subscript = 2;
            }
        }
        /// <summary> Выбор пути сохранения файла путем выбора пользователем в открывшемся окне </summary>
        /// <returns> Абсолютный путь сохранения файла </returns>
        protected string GetSavePath()
        {
            SaveFileDialog save = new SaveFileDialog
            {
                AddExtension = true,
                InitialDirectory = @"D:\",
                RestoreDirectory = true,
                Title = "Выбор файла",
                DefaultExt = "docx",
                Filter = "docx files (*.docx)|*.docx|All files (*.*)|*.*",
                FilterIndex = 1,
                CreatePrompt = false
            };
            save.ShowDialog();
            if (File.Exists(save.FileName))
                File.Delete(save.FileName);
            File.Create(save.FileName).Close();
            return save.FileName;
        }
        /// <summary> Работа с документом </summary>
        /// <param name="reporter"> Модель, отслеживающая запись процесса расчета в файл </param>
        /// <returns> true, если запись произошла успешно; false в противном случае </returns>
        protected bool WriteWordDocument(RepProViewModel reporter)
        {
            try
            {
                _app = new Word.Application();
                _app.Documents.Open(GetSavePath());
                _app.ActiveDocument.Range().Font.Size = 12;
                _app.ActiveDocument.Range().Font.Name = "Times New Roman";
                WriteCalculation(reporter);
                _app.ActiveDocument.Save();
                _app.ActiveDocument.Close();
                return true;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
            finally
            {
                if (_app != null)
                    _app.Quit();
            }
            return false;
        }
        /// <summary> Расчет необходимой величины с логированием </summary>
        /// <param name="reporter"> Модель, отслеживающая запись процесса расчета в файл </param>
        protected abstract void WriteCalculation(RepProViewModel reporter);

        #endregion

        #region Public Methods

        /// <summary> Генерация отчета о проведенных операциях расчета и результат вычисления </summary>
        /// <param name="reporter"> Модель, отслеживающая запись процесса расчета в файл </param>
        public void WriteResult(RepProViewModel reporter) =>
            System.Windows.MessageBox.Show(WriteWordDocument(reporter) ? "Файл успешно записан!" : "Файл не удалось записать!");

        #endregion

        #region Protected Delegate

        /// <summary> Форматирование текста параграфа </summary>
        protected delegate void Formating();

        #endregion
    }
}
