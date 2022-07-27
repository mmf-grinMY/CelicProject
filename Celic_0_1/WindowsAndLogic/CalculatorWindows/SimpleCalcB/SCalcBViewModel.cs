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
        }

        #endregion
    }
}