using System.Windows;
using System.Windows.Controls;
using static Celic.HelpManager;

namespace Celic
{
    /// <summary> Логика взаимодействия для EPlastDataBox.xaml </summary>
    public partial class PlastDataBox : UserControl
    {
        #region Contructors

        /// <summary> Основной конструктор для данного класса </summary>
        public PlastDataBox()
        {
            InitializeComponent();
        }
        /// <summary> Статический конструктор для данного класса </summary>
        static PlastDataBox()
        {
            ContiguosSimpleInputVisibility = DependencyProperty.Register("SContiguosVisibility", typeof(Visibility), typeof(EPlastDataBox));
            ContiguosExtentedInputVisibility = DependencyProperty.Register("EContiguosVisibility", typeof(Visibility), typeof(EPlastDataBox));
        }

        #endregion

        #region Contiguos InputFields Visibility

        // contiguosSimpleInput Visibility Properties
        //------------------------------------
        /// <summary> Свойство видимости contiguosSimpleInput </summary>
        public static DependencyProperty ContiguosSimpleInputVisibility;
        /// <summary> Обертка для свойства видимости contiguosSimpleInput </summary>
        public Visibility SContiguosVisibility
        {
            set { SetValue(ContiguosSimpleInputVisibility, value); }
            get { return (Visibility)GetValue(ContiguosSimpleInputVisibility); }
        }
        // contiguosExtentedInput Visibility Properties
        //------------------------------------
        /// <summary> Свойство видимости contiguosExtentedInput </summary>
        public static DependencyProperty ContiguosExtentedInputVisibility;
        /// <summary> Обертка для свойства видимости contiguosExtentedInput </summary>
        public Visibility EContiguosVisibility
        {
            set { SetValue(ContiguosExtentedInputVisibility, value); }
            get { return (Visibility)GetValue(ContiguosExtentedInputVisibility); }
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

        #endregion

        #region EventHandlers

        /// <summary> Обработчик события "Изменение системы разработки пласта" </summary>
        /// <param name="sender"> Вызываемый ComboBox </param>
        /// <param name="e"> Аргументы вызова </param>
        private void ComboBoxTypeDev_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox && DataContext != null)
            {
                
            }
        }
        private void OpenExtentedInput_Click(object sender, RoutedEventArgs e)
        {
            simpleInputKt.Visibility = Visibility.Collapsed;
            extentedInputKt.Visibility = Visibility.Visible;
        }

        private void OpenSimpleInput_Click(object sender, RoutedEventArgs e)
        {
            extentedInputKt.Visibility = Visibility.Collapsed;
            simpleInputKt.Visibility = Visibility.Visible;
        }

        #endregion


    }
}
