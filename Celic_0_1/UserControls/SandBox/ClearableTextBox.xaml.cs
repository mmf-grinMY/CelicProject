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

namespace Celic
{
    /// <summary>
    /// Логика взаимодействия для ClearableTextBox.xaml
    /// </summary>
    public partial class ClearableTextBox : Control
    {
        /// <summary>
        /// 
        /// </summary>
        public ClearableTextBox()
        {
            // InitializeComponent();
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ClearableTextBox), new PropertyMetadata());
        }
    }
}
