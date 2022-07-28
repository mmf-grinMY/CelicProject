namespace Celic
{
    /// <summary> Главный класс обработки системы разработки </summary>
    public class MainViewModel : BaseViewModel
    {
        #region Private Fields

        private readonly MainWindow _mainWindow;

        #endregion

        /*#region Test
        private string _value;
        /// <summary>
        /// 
        /// </summary>
        public string Value {
            get => _value;
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }
        #endregion*/

        #region Constructors

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainWindow"></param>
        public MainViewModel(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
        }

        #endregion

        #region Commands
        
        private RelayCommand simpleCalcBCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand SimpleCalcBCommand
        {
            get
            {
                return simpleCalcBCommand ??
                (simpleCalcBCommand = new RelayCommand(obj => 
                {
                    SCalcBWindow calcBWindow = new SCalcBWindow(_mainWindow);
                    _mainWindow.Hide();
                    calcBWindow.ShowDialog();
                }));
            }
        }
        private RelayCommand simpleCalcCCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand SimpleCalcCCommand
        {
            get
            {
                return simpleCalcCCommand ??
                (simpleCalcCCommand = new RelayCommand(obj =>
                {
                    SCalcCWindow calcCWindow = new SCalcCWindow(_mainWindow);
                    _mainWindow.Hide();
                    calcCWindow.ShowDialog();
                }));
            }
        }
        private RelayCommand simpleCalcDCommand;
        /// <summary>
        /// 
        /// </summary>
        public RelayCommand SimpleCalcDCommand
        {
            get
            {
                return simpleCalcDCommand ??
                (simpleCalcDCommand = new RelayCommand(obj =>
                {
                    SCalcDWindow calcDWindow = new SCalcDWindow(_mainWindow);
                    _mainWindow.Hide();
                    calcDWindow.ShowDialog();
                }));
            }
        }

        #endregion
    }
}