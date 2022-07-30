namespace Celic
{
    class SCalcCViewModel : OneListPlastViewModel
    {
        #region Private Fields

        /// <summary> Угол поворота плоскости разлома ( поле ) </summary>
        private string _alfa;

        #endregion

        #region Public Properties

        /// <summary> Угол поворота плоскости разлома </summary>
        public string Alfa
        {
            get => _alfa;
            set
            {
                _alfa = HelpManager.ValidateStringRange(value, 0, 90); 
                OnPropertyChanged(nameof(Alfa));
            }
        }

        #endregion

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        /// <param name="mainWindow"> Главное окно приложения </param>
        /// <param name="calcWindow"> Вызывающее окно расчета </param>
        public SCalcCViewModel(MainWindow mainWindow, SCalcCWindow calcWindow) : base(mainWindow, calcWindow) 
        {
            CalcWithoutDocxCommand = new RelayCommand(obj =>
            {
                string trueOut = $"В результате величины приразломных целиков равны:\n {new SCalculatorC(Plasts, Alfa).Count()}";
                string falseOut = "Мы не можем произвести расчеты для несуществующих пластов";
                System.Windows.MessageBox.Show(Plasts.Count > 0 ? trueOut : falseOut);
            }, obj => Plasts.Count > 0);
        }

        #endregion
    }
}