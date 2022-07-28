using System.Windows;
using Celic.WPF.CustomControls;

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

        private void ClearableTextBox_TextCleared(object sender, TextClearedEventArgs e)
        {
            MessageBox.Show($"Old Text value is \"{e.OldText}\"");
        }
    }
}
