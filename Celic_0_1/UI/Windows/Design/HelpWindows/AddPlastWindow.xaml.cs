using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Celic
{
    /// <summary> Логика взаимодействия для AddPlastWindow.xaml </summary>
    public partial class AddPlastWindow : Window
    {
        /// <summary> Основной конструктор для данного класса </summary>
        /// <param name="plasts"> Изначальный список разрабатываемых пластов </param>
        public AddPlastWindow(ObservableCollection<Plast> plasts)
        {
            InitializeComponent();
            DataContext = new AddPlastViewModel(plasts, this);
            this.Closing += (DataContext as AddPlastViewModel).Close;
        }
    }
}
