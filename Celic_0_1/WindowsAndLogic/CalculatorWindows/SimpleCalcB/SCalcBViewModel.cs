namespace Celic
{
    /// <summary> Логика работы окна с расчетом высоты ЗВТ </summary>
    class SCalcBViewModel : OneListPlastViewModel
    {
        // Добавить возможность определять сближенность пластов
        // При генерации отчета
        // -- выделять новый поток под генерацию и расчет
        // -- показывать с помощью ProgressBar прогресс записи в файл

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        public SCalcBViewModel(MainWindow mainWindow, SCalcBWindow calcBWindow) : base(mainWindow, calcBWindow)
        {
            CalcWithoutDocxCommand = new RelayCommand(obj =>
            {
                string trueOut = $"В результате минимально необходимая высота расчета ЗВТ равна {new SCalculatorB(Plasts).Count()} м.";
                string falseOut = "Мы не можем произвести расчеты для несуществующих пластов";
                System.Windows.MessageBox.Show(Plasts.Count > 0 ? trueOut : falseOut);
            }, obj => Plasts.Count > 0);
            SelectedTop = SelectedButtom = "пласт не задан";
        }

        #endregion

        #region Private Fields

        private string _buttom;
        private string _top;
        private string _selectedButtom;
        private string _selectedTop;
        private Plast _next;
        private Plast _prev;
        private readonly string not = "отсутствует";
        private readonly string undefine = "пласт не задан";

        #endregion

        #region Public Properties

        public new Plast SelectedPlast
        {
            set
            {
                _selectedPlast = value;
                next = prev = null;
                if (_selectedPlast != null)
                {
                    SelectedTop = _selectedPlast.Top;
                    SelectedButtom = _selectedPlast.Buttom;
                    (_calcWindow as SCalcBWindow).selected.IsEnabled = true;
                    int index = Plasts.IndexOf(_selectedPlast);
                    if (index != -1)
                    {
                        if (index != Plasts.Count - 1)
                            next = Plasts[index + 1];
                        if (index != 0)
                            prev = Plasts[index - 1];
                    }
                }
                OnPropertyChanged(nameof(SelectedPlast));
            }
            get => _selectedPlast;
        }
        public string Buttom
        {
            get => _buttom;
            set
            {
                _buttom = value;
                OnPropertyChanged(nameof(Buttom));
            }
        }
        public string Top
        {
            get => _top;
            set
            {
                _top = value;
                OnPropertyChanged(nameof(Top));
            }
        }
        public string SelectedButtom
        {
            get => _selectedButtom;
            set
            {
                if(SelectedPlast != null)
                {
                    _selectedButtom = value;
                    if (_selectedButtom.Equals(not))
                    {
                        if (_next != null)
                            _next.Top = not;
                        _selectedPlast.Buttom = not;
                    }
                    else if (_selectedButtom.Equals(_buttom))
                    {
                        if (_selectedButtom.Equals(undefine))
                        {
                            _selectedPlast.Buttom = undefine;
                        }
                        else
                        {
                            _next.Top = _selectedPlast.Name;
                            _selectedPlast.Buttom = _next.Name;
                        }
                    } 
                }
                OnPropertyChanged(nameof(SelectedButtom));
            }
        }
        public string SelectedTop
        {
            get => _selectedTop;
            set
            {
                if (SelectedPlast != null)
                {
                    _selectedTop = value;
                    if (_selectedTop.Equals(not))
                    {
                        if (_prev != null)
                            _prev.Buttom = not;
                        _selectedPlast.Top = not;
                    }
                    else if (_selectedTop.Equals(_top))
                    {
                        if (_selectedTop.Equals(undefine))
                        {
                            _selectedPlast.Top = undefine;
                        }
                        else
                        {
                            _prev.Buttom = _selectedPlast.Name;
                            _selectedPlast.Top = _prev.Name;
                        }
                    }
                }
                OnPropertyChanged(nameof(SelectedTop));
            }
        }
        private Plast next
        {
            get => _next;
            set => Buttom = (_next = value) != null ? _next.Name : undefine;
        }
        private Plast prev
        {
            get => _prev;
            set => Top = (_prev = value) != null ? _prev.Name : undefine;
        }

        #endregion
    }
}