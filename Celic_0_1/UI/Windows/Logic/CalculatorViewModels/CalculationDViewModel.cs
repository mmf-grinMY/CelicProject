using System.Windows;

namespace Celic
{
    internal sealed class CalculationDViewModel : TwoListPlastViewModel
    {
        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        /// <param name="mainWindow"> Главное окно приложения </param>
        /// <param name="calcWindow"> Вызывающеее окно расчета </param>
        public CalculationDViewModel(MainWindow mainWindow, CalculationDWindow calcWindow) : base(mainWindow, calcWindow)
        {
            CalcWithoutDocxCommand = new RelayCommand(obj =>
            {
                string notEqualsOut = "Для успешного проведения расчета в столбцах должно находиться равное количество разрабатываемых пластов";
                string trueOut = $"В результате значения ширины междубарьерных целиков равны:\n" +
                                    $"{new SCalculatorD(LeftPlasts, RightPlasts).Count()}";
                string falseOut = "Мы не можем произвести расчеты для несуществующих пластов";
                MessageBox.Show(LeftPlasts.Count == RightPlasts.Count ? (LeftPlasts.Count > 0 ? trueOut : falseOut) : notEqualsOut);
            }, obj => LeftPlasts.Count == RightPlasts.Count && LeftPlasts.Count > 0);
        }

        #endregion
    }
}
