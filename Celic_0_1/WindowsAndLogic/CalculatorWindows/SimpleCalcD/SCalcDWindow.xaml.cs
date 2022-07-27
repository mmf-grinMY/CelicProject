using System.Windows;

namespace Celic
{
    /// <summary>
    /// Логика взаимодействия для SimpleCalcDWindow.xaml
    /// </summary>
    public partial class SCalcDWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainWindow"></param>
        public SCalcDWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = new SCalcDViewModel(mainWindow, this);
            Closing += (DataContext as SCalcDViewModel).Close;
        }
    }
}
