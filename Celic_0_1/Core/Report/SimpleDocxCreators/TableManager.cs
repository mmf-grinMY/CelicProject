using System.Collections.ObjectModel;
using Word = Microsoft.Office.Interop.Word;

namespace Celic
{
    /// <summary> Запись таблицы и ее заполнение </summary>
    class TableManager
    {
        #region Private Fields

        /// <summary> Приложение Ворд </summary>
        private readonly Word.Application _app = null;
        /// <summary> Новая таблица </summary>
        private Word.Table _table = null;
        /// <summary> Ячейка таблицы </summary>
        private Word.Cell _cell = null;
        /// <summary> Длина заполненной части документа </summary>
        private int _rangeLength = 0;
        /// <summary> Коллекция разрабатываемых пластов </summary>
        private readonly ObservableCollection<Plast> _plasts = null;

        #endregion

        #region Public Properties

        /// <summary> Выбранная ячейка таблицы ( ячейка, с которой в данный момент работают методы ) </summary>
        public Word.Cell Cell { get => _cell; }

        #endregion

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        /// <param name="application"> Запущенное приложение Microsoft Word </param>
        /// <param name="plasts"> Коллекция разрабатываемых пластов </param>
        public TableManager(Word.Application application, ObservableCollection<Plast> plasts)
        {
            _app = application;
            _rangeLength = _app.ActiveDocument.Range().Text.Length;
            _plasts = plasts;
        }

        #endregion

        #region Public Methods

        /// <summary> Метод для записи таблицы </summary>
        public int WriteTable(RepProViewModel reporter)
        {
            float status = 55f, offset = 3f;
            CreateTable();
            reporter.StatusReport = (status += offset).ToString();
            _cell = _table.Rows[1].Cells[1];
            WriteRow1();
            reporter.StatusReport = (status += offset).ToString();
            WriteRow2();
            reporter.StatusReport = (status += offset).ToString();
            WriteRow3();
            reporter.StatusReport = (status += offset).ToString();
            WriteRow4();
            reporter.StatusReport = (status += offset).ToString();
            WriteRow5();
            reporter.StatusReport = (status += offset).ToString();

            _cell = _cell.Next.Next.Next;
            _cell.Range.Text = "Высота зоны развития водопроводящих трещин от совместного влияния отработки разрабатываемых пластов, НT, м";
            Select("пластов, НT, м", 1, 8).Font.Subscript = 1;
            _rangeLength += _cell.Range.Text.Length;
            reporter.StatusReport = (status += offset).ToString();

            _cell = _table.Cell(6, 2);

            _table.Rows[6].Cells[2].Merge(_table.Rows[6].Cells[3]);
            _table.Rows[6].Cells[2].Merge(_table.Rows[6].Cells[3]);
            _table.Rows[1].Cells[1].Merge(_table.Rows[2].Cells[1]);
            reporter.StatusReport = (status += offset).ToString();

            return _rangeLength;
        }

        #endregion

        #region Private Methods

        /// <summary> Метод для выделения области таблицы </summary>
        /// <param name="start"> Начальная строка </param>
        /// <param name="length"> Длина выделения </param>
        /// <param name="offset"> Количество символов, на которые нужно сместиться </param>
        /// <returns> Выделенная область таблицы </returns>
        private Word.Selection Select(string start, int length = 1, int offset = 0)
        {
            int index = _cell.Range.Text.IndexOf(start);
            if (index != -1)
            {
                index += -1 - _cell.ColumnIndex - _cell.RowIndex + _rangeLength + offset;
                _app.ActiveDocument.Range(index, index + length).Select();
            }
            return _app.Selection;
        }
        /// <summary> Метод для создания таблицы </summary>
        /// <param name="rows"> Количество строк таблицы </param>
        private void CreateTable(int rows = 6)
        {
            Word.Document doc = _app.ActiveDocument;
            _table = doc.Tables.Add(doc.Range(_rangeLength-1), rows, _plasts.Count + 1);
            _table.Borders.Enable = 1;
            _table.AllowPageBreaks = true;
            _table.Range.Font.Name = "Times New Roman";
            _table.Range.Font.Size = 12;
        }
        /// <summary> Метод для заполенения первой строки таблицы </summary>
        private void WriteRow1()
        {
            int contiguosNumber = 1;
            for(int i = 0; i < _table.Columns.Count; i++)
            {
                _cell.Range.Text = i == 0 ? "Наименование параметра" :
                    i + "-й пласт " + (_plasts[i - 1].Contiguos != 0 ? "(Сближенный слой " + contiguosNumber++ + ")" : "(Пласт)");
                _rangeLength += _cell.Range.Text.Length;
                _cell = _cell.Next;
            }
        }
        /// <summary> Метод для заполнения второй строки таблицы </summary>
        private void WriteRow2()
        {
            for (int i = 0; i < _plasts.Count; i++)
            {
                _cell = _cell.Next;
                _cell.Range.Text = _plasts[i].TypeDev;
                _rangeLength += _cell.Range.Text.Length;
            }
        }
        /// <summary> Метод для заполнения третьей строки таблицы </summary>
        private void WriteRow3()
        {
            for(int i = 0; i < _table.Columns.Count; i++)
            {
                _cell = _cell.Next;
                if (i == 0)
                {
                    _cell.Range.Text = "Приведенная вынимаемая мощность, mпр, м";
                    Select("mпр", 2, 1).Font.Subscript = 1;
                }
                else
                {
                    _cell.Range.Text = "mпр" + i + " = mв ∙ k' = " + _plasts[i - 1].Mv + " ∙ " 
                        + _plasts[i - 1].K + " = " + _plasts[i - 1].MPr() + " м";
                    Select("mпр" + i, 3, 1).Font.Subscript = 1;
                    Select("mв",1,1).Font.Subscript = 1;
                }
                _rangeLength += _cell.Range.Text.Length;
            }
        }
        /// <summary> Метод для заполнения четвертой строки таблицы </summary>
        private void WriteRow4()
        {
            for (int i = 0; i < _table.Columns.Count; i++)
            {
                _cell = _cell.Next;
                if (i == 0)
                {
                    _cell.Range.Text = "Параметр d, м";
                }
                else
                {
                    _cell.Range.Text = "d" + i +" = " + HelpManager.GetD(_plasts[i - 1]) + 
                        " - 0,01 ∙ H = " + HelpManager.GetD(_plasts[i - 1]) + " - 0,01 ∙ " + _plasts[i - 1].H + " = " + _plasts[i - 1].D();
                    Select("d" + i, 1, -1).Font.Subscript = 1;
                }
                _rangeLength += _cell.Range.Text.Length;
            }
        }
        /// <summary> Метод для заполнения пятой строки таблицы  </summary>
        private void WriteRow5()
        {
            _cell = _cell.Next;
            _cell.Range.Text = "Высота распространения зоны техногенных водопроводящих трещин от " +
                "влияния отработки самого верхнего пласта, h, м ";
            _rangeLength += _cell.Range.Text.Length;

            _cell = _cell.Next;
            _cell.Range.Text = "h1 = d1 ∙ mпр1 ∙ S1 = " + _plasts[0].D() +" ∙ " + 
                _plasts[0].MPr() + " ∙ " + _plasts[0].S + " = " + _plasts[0].Ht() + " м";
            Select("h1", 1, -3).Font.Subscript = 1;
            Select("d1", 1, -3).Font.Subscript = 1;
            Select("mпр1", 3, -3).Font.Subscript = 1;
            Select("S1", 1, -3).Font.Subscript = 1;
            _rangeLength += _cell.Range.Text.Length;
        }

        #endregion
    }
}
