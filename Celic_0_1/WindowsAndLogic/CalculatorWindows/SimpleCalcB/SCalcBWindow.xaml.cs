using System.Windows;

namespace Celic
{
    /// <summary>
    /// Логика взаимодействия для CalcBWindow.xaml
    /// </summary>
    public partial class SCalcBWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainWindow"></param>
        public SCalcBWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = new SCalcBViewModel(mainWindow, this);
            this.Closing += (DataContext as SCalcBViewModel).Close;
        }
    }
}
