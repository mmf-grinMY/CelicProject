using System.Windows;

namespace Celic
{
    /// <summary>
    /// Логика взаимодействия для CalcBWindow.xaml
    /// </summary>
    public partial class CalculationBWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainWindow"></param>
        public CalculationBWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = new CalculationBViewModel(mainWindow, this);
            this.Closing += (DataContext as CalculationBViewModel).Close;
        }
    }
}
