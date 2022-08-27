using System.Windows;

namespace Celic
{
    /// <summary>
    /// Логика взаимодействия для SimpleCalcDWindow.xaml
    /// </summary>
    public partial class CalculationDWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainWindow"></param>
        public CalculationDWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = new CalculationDViewModel(mainWindow, this);
            Closing += (DataContext as CalculationDViewModel).Close;
        }
    }
}
