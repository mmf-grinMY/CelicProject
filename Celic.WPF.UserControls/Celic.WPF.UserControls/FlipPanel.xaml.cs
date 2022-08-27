using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Celic.WPF.UserControls
{
    /// <summary> Логика взаимодействия для FlipPanel.xaml </summary>
    public partial class FlipPanel : UserControl
    {
        /// <summary> Основной конструктор для данного класса </summary>
        public FlipPanel()
        {
            InitializeComponent();
        }
        static FlipPanel()
        {
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(FlipPanel));
        }

        public static readonly DependencyProperty TextProperty;
        public string Text
        {
            get { return GetValue(TextProperty).ToString(); }
            set { SetValue(TextProperty, value); }
        }
    }
}
