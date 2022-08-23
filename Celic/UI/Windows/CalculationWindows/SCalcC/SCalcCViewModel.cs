using System;
using System.Text;

namespace Celic
{
	class SCalcCViewModel : OneListPlastViewModel
    {
        #region Private Fields

        /// <summary> Угол поворота плоскости разлома ( поле ) </summary>
        private float _alfa;

        #endregion

        #region Public Properties

        /// <summary> Угол поворота плоскости разлома </summary>
        public float Alfa
        {
            get 
            {
            	return _alfa;
            }
            set
            {
                _alfa = value; 
                OnPropertyChanged("Alfa");
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
        	    var str = new StringBuilder("В результате величины приразломных целиков равны:\n");
        	    // str.Append(new SCalculatorC(Plasts, Alfa).Count());
        	    string trueOut = str.ToString();
                string falseOut = "Мы не можем произвести расчеты для несуществующих пластов";
                System.Windows.MessageBox.Show(Plasts.Count > 0 ? trueOut : falseOut);
            }, obj => Plasts.Count > 0);
        }

        #endregion
    }
}
