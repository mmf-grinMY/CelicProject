namespace Celic
{
    /// <summary> Логика работы окна с расчетом высоты ЗВТ </summary>
    class SCalcBViewModel : OneListPlastViewModel
    {
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
            SelectedTop = SelectedButtom = undefine;
        }

        #endregion

        #region Private Fields

        /// <summary> Имя пласта, расположенного ниже выбранного ( поле ) </summary>
        private string _buttom;
        /// <summary> Имя пласта, расположенного выше выбранного ( поле ) </summary>
        private string _top;
        /// <summary> Имя выбранного сближенного пласта снизу выбранного пласта ( поле ) </summary>
        private string _selectedButtom;
        /// <summary> Имя выбранного сближенного пласта сверху выбранного пласта ( поле ) </summary>
        private string _selectedTop;
        /// <summary> Пласт, расположенный снизу выбранного ( поле ) </summary>
        private Plast _next;
        /// <summary> Пласт, расположенный сверху выбранного ( поле ) </summary>
        private Plast _prev;
        /// <summary> Отсутствие сближенного пласта </summary>
        private readonly string not = "отсутствует";
        /// <summary> Сближенный пласт по каким-то причинам не может быть определен </summary>
        private readonly string undefine = "пласт не задан";

        #endregion

        #region Public Properties

        /// <summary> Выбранный пользователем пласт </summary>
        public new Plast SelectedPlast
        {
            set
            {
                _selectedPlast = value;
                Next = Prev = null;
                if (_selectedPlast != null)
                {
                    SelectedTop = _selectedPlast.Top;
                    SelectedButtom = _selectedPlast.Buttom;
                    (_calcWindow as SCalcBWindow).selected.IsEnabled = true;
                    int index = Plasts.IndexOf(_selectedPlast);
                    if (index != -1)
                    {
                        if (index != Plasts.Count - 1)
                            Next = Plasts[index + 1];
                        if (index != 0)
                            Prev = Plasts[index - 1];
                    }
                }
                OnPropertyChanged(nameof(SelectedPlast));
            }
            get => _selectedPlast;
        }
        /// <summary> Имя пласта, расположенного ниже выбранного </summary>
        public string Buttom
        {
            get => _buttom;
            set
            {
                _buttom = value;
                OnPropertyChanged(nameof(Buttom));
            }
        }
        /// <summary> Имя пласта, расположенного выше выбранного </summary>
        public string Top
        {
            get => _top;
            set
            {
                _top = value;
                OnPropertyChanged(nameof(Top));
            }
        }
        /// <summary> Имя выбранного сближенного пласта снизу выбранного пласта </summary>
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
        /// <summary> Имя выбранного сближенного пласта сверху выбранного пласта </summary>
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
        /// <summary> Пласт, расположенный снизу выбранного </summary>
        private Plast Next { set => Buttom = (_next = value) != null ? _next.Name : undefine; }
        /// <summary> Пласт, расположенный сверху выбранного </summary>
        private Plast Prev {  set => Top = (_prev = value) != null ? _prev.Name : undefine; }

        #endregion
    }
}