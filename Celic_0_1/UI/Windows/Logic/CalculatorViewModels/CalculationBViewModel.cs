namespace Celic
{
    /// <summary> Логика работы окна с расчетом высоты ЗВТ </summary>
    internal sealed class CalculationBViewModel : OneListPlastViewModel
    {
        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        public CalculationBViewModel(MainWindow mainWindow, CalculationBWindow calcBWindow) : base(mainWindow, calcBWindow)
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
                base.SelectedPlast = value;
                Next = Prev = null;
                if (base.SelectedPlast != null)
                {
                    SelectedTop = base.SelectedPlast.Top;
                    SelectedButtom = base.SelectedPlast.Buttom;
                    (_calcWindow as CalculationBWindow).selected.IsEnabled = true;
                    int index = Plasts.IndexOf(base.SelectedPlast);
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
            get => base.SelectedPlast;
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
                        base.SelectedPlast.Buttom = not;
                    }
                    else if (_selectedButtom.Equals(_buttom))
                    {
                        if (_selectedButtom.Equals(undefine))
                        {
                            base.SelectedPlast.Buttom = undefine;
                        }
                        else
                        {
                            _next.Top = base.SelectedPlast.Name;
                            base.SelectedPlast.Buttom = _next.Name;
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
                        base.SelectedPlast.Top = not;
                    }
                    else if (_selectedTop.Equals(_top))
                    {
                        if (_selectedTop.Equals(undefine))
                        {
                            base.SelectedPlast.Top = undefine;
                        }
                        else
                        {
                            _prev.Buttom = base.SelectedPlast.Name;
                            base.SelectedPlast.Top = _prev.Name;
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