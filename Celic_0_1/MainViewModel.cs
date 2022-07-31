namespace Celic
{
    /// <summary> Главный класс обработки системы разработки </summary>
    public class MainViewModel : BaseViewModel
    {
        #region Private Fields

        /// <summary> Главное окно приложения </summary>
        private readonly MainWindow _mainWindow;

        #endregion

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        /// <param name="mainWindow"> Главное окно приложения </param>
        public MainViewModel(MainWindow mainWindow) : base() => _mainWindow = mainWindow;

        #endregion

        #region Commands

        /// <summary> Команда открытия окна расчета высоты ЗВТ ( поле ) </summary>
        private RelayCommand simpleCalcBCommand;
        /// <summary> Команда открытия окна расчета высоты ЗВТ </summary>
        public RelayCommand SimpleCalcBCommand
        {
            get
            {
                return simpleCalcBCommand ??
                (simpleCalcBCommand = new RelayCommand(obj => 
                {
                    _mainWindow.Hide();
                    new SCalcBWindow(_mainWindow).ShowDialog();
                }));
            }
        }
        /// <summary> Команда открытия окна расчета приразломных целиков ( поле ) </summary>
        private RelayCommand simpleCalcCCommand;
        /// <summary> Команда открытия окна расчета приразломных целиков </summary>
        public RelayCommand SimpleCalcCCommand
        {
            get
            {
                return simpleCalcCCommand ??
                (simpleCalcCCommand = new RelayCommand(obj =>
                {
                    _mainWindow.Hide();
                    new SCalcCWindow(_mainWindow).ShowDialog();
                }));
            }
        }
        /// <summary> Команда открытия окна расчета ширины междубарьерных целиков ( поле ) </summary>
        private RelayCommand simpleCalcDCommand;
        /// <summary> Команда открытия окна расчета ширины междубарьерных целиков </summary>
        public RelayCommand SimpleCalcDCommand
        {
            get
            {
                return simpleCalcDCommand ??
                (simpleCalcDCommand = new RelayCommand(obj =>
                {
                    _mainWindow.Hide();
                    new SCalcDWindow(_mainWindow).ShowDialog();
                }));
            }
        }

        #endregion
    }
}