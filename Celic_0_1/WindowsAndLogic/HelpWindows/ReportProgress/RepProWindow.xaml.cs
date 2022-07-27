using System.Collections.ObjectModel;
using System.Windows;

namespace Celic
{
    /// <summary>
    /// Логика взаимодействия для RepProWindow.xaml
    /// </summary>
    partial class RepProWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public RepProWindow(ListPlastViewModel model)
        {
            InitializeComponent();
            DataContext = new RepProViewModel(this, model);
        }
    }
}
