using System.Windows;

namespace Celic
{
    /// <summary>
    /// Логика взаимодействия для SimpleCalcCWindow.xaml
    /// </summary>
    public partial class CalculationCWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainWindow"></param>
        public CalculationCWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = new CalculationCViewModel(mainWindow, this);
            Closing += (DataContext as CalculationCViewModel).Close;
        }
    }
}
