using System.Collections.ObjectModel;
using System.Windows;

namespace Celic
{
    /// <summary>
    /// Логика взаимодействия для RepProWindow.xaml
    /// </summary>
    partial class ReportProgressWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public ReportProgressWindow(ListPlastViewModel model)
        {
            InitializeComponent();
            DataContext = new RepProViewModel(this, model);
        }
    }
}
