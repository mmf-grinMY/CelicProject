using System.Text;
using System.Windows;

namespace Celic
{
	class SCalcDViewModel : TwoListPlastViewModel
    {
        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        /// <param name="mainWindow"> Главное окно приложения </param>
        /// <param name="calcWindow"> Вызывающеее окно расчета </param>
        public SCalcDViewModel(MainWindow mainWindow, SCalcDWindow calcWindow) : base(mainWindow, calcWindow)
        {
            CalcWithoutDocxCommand = new RelayCommand(obj =>
            {
                string notEqualsOut = "Для успешного проведения расчета в столбцах должно находиться равное количество разрабатываемых пластов";
                var str = new StringBuilder("В результате значения ширины междубарьерных целиков равны:\n");
                // str.Append(new SCalculatorD(LeftPlasts, RightPlasts).Count());
                string trueOut = str.ToString();
                string falseOut = "Мы не можем произвести расчеты для несуществующих пластов";
                MessageBox.Show(LeftPlasts.Count == RightPlasts.Count ? (LeftPlasts.Count > 0 ? trueOut : falseOut) : notEqualsOut);
            }, obj => LeftPlasts.Count == RightPlasts.Count && LeftPlasts.Count > 0);
        }

        #endregion
    }
}
