using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Celic.WPF.UserControls
{
    /// <summary>
    /// Логика взаимодействия для InputBox.xaml
    /// </summary>
    public partial class InputBox : UserControl
    {
        /// <summary> Основной конструктор для данного класса </summary>
        public InputBox()
        {
            InitializeComponent();
        }
        public static readonly DependencyProperty TextProperty;
        public string Text
        {
            get { return GetValue(TextProperty)?.ToString(); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextBoxTextProperty;
        public string TextBoxText
        {
            get { return GetValue(TextBoxTextProperty)?.ToString(); }
            set { SetValue(TextBoxTextProperty, value); }
        }
        static InputBox()
        {
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(InputBox));
            TextBoxTextProperty = DependencyProperty.Register("TextBoxText", typeof(string), typeof(InputBox));
        }
    }
}
