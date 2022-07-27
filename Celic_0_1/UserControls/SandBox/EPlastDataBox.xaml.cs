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
    /// Логика взаимодействия для EPlastDataBox.xaml
    /// </summary>
    public partial class EPlastDataBox : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public EPlastDataBox()
        {
            InitializeComponent();
        }
        private void CheckBoxKiExtented_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.IsChecked == true 
                && kiSimpleInput.Visibility == Visibility.Visible && DataContext is Plast plast)
            {
                kiExtentedInput.Visibility = Visibility.Visible;
                kiSimpleInput.Visibility = Visibility.Collapsed;
                if(plast.TypeDev == "камерная")
                {
                    CameraKiExtentedInput.Visibility = Visibility.Visible;
                    LavaKiExtentedInput.Visibility = Visibility.Collapsed;
                }
                else
                {
                    CameraKiExtentedInput.Visibility = Visibility.Collapsed;
                    LavaKiExtentedInput.Visibility = Visibility.Visible;
                }
                checkBox.IsChecked = false;
            }
        }

        private void CheckBoxKiSimple_Click(object sender, RoutedEventArgs e)
        {
            if(sender is CheckBox checkBox)
            {
                if (kiExtentedInput.Visibility == Visibility.Visible && checkBox.IsChecked == true)
                {
                    kiExtentedInput.Visibility = Visibility.Collapsed;
                    kiSimpleInput.Visibility = Visibility.Visible;
                    checkBox.IsChecked = false;
                }
            }
        }
        private void CheckBoxKExtented_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.IsChecked == true 
                && kSimpleInput.Visibility == Visibility.Visible && DataContext is Plast plast)
            {
                kExtentedInput.Visibility = Visibility.Visible;
                kSimpleInput.Visibility = Visibility.Collapsed;
                if (plast.TypeDev == "камерная")
                {
                    BInput.Visibility = Visibility.Collapsed;
                }
                else
                {
                    BInput.Visibility = Visibility.Visible;
                }
                checkBox.IsChecked = false;
            }
        }

        private void CheckBoxKSimple_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if (kExtentedInput.Visibility == Visibility.Visible && checkBox.IsChecked == true)
                {
                    kExtentedInput.Visibility = Visibility.Collapsed;
                    kSimpleInput.Visibility = Visibility.Visible;
                    checkBox.IsChecked = false;
                }
            }
        }

        private void CheckBoxContiguosExtented_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if (contiguosSimpleInput.Visibility == Visibility.Visible && checkBox.IsChecked == true)
                {
                    contiguosSimpleInput.Visibility = Visibility.Collapsed;
                    contiguosExtentedInput.Visibility = Visibility.Visible;
                    checkBox.IsChecked = false;
                }
            }
        }
        private void CheckBoxContiguosSimple_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox)
            {
                if (contiguosExtentedInput.Visibility == Visibility.Visible && checkBox.IsChecked == true)
                {
                    contiguosExtentedInput.Visibility = Visibility.Collapsed;
                    contiguosSimpleInput.Visibility = Visibility.Visible;
                    checkBox.IsChecked = false;
                }
            }
        }

        private void TestUserControl_BoxTextChanged(object sender, RoutedPropertyChangedEventArgs<string> e)
        {
            Console.WriteLine("Обработчик успешно сработал");
        }
    }
}
