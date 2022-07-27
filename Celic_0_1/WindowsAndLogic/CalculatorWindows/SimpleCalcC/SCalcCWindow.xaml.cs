using System.Windows;

namespace Celic
{
    /// <summary>
    /// Логика взаимодействия для SimpleCalcCWindow.xaml
    /// </summary>
    public partial class SCalcCWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainWindow"></param>
        public SCalcCWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = new SCalcCViewModel(mainWindow, this);
            Closing += (DataContext as SCalcCViewModel).Close;
        }
    }
}
