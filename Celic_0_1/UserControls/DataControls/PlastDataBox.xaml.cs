using System.Collections.ObjectModel;
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
            ContiguosSimpleInputVisibility = DependencyProperty.Register("SContiguosVisibility", typeof(Visibility), typeof(PlastDataBox));
            ContiguosExtentedInputVisibility = DependencyProperty.Register("EContiguosVisibility", typeof(Visibility), typeof(PlastDataBox));
            KiValueVisibilityProperty = DependencyProperty.Register("KiValueVisibility", typeof(Visibility), typeof(PlastDataBox));
            KiAppendValuesCameraVisibilityProperty = DependencyProperty.Register("KiAppendValuesCameraVisibility", typeof(Visibility), typeof(PlastDataBox));
            KiAppendValuesLavaVisibilityProperty = DependencyProperty.Register("KiAppendValuesLavaVisibility", typeof(Visibility), typeof(PlastDataBox));
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

        #region DependencyProperty

        public static readonly DependencyProperty KiValueVisibilityProperty;
        public Visibility KiValueVisibility
        {
            get { return (Visibility)GetValue(KiValueVisibilityProperty); }
            set { SetValue(KiValueVisibilityProperty, value); }
        }
        public static readonly DependencyProperty KiAppendValuesCameraVisibilityProperty;
        public Visibility KiAppendValuesCameraVisibility
        {
            get { return (Visibility)GetValue(KiAppendValuesCameraVisibilityProperty); }
            set { SetValue(KiAppendValuesCameraVisibilityProperty, value); }
        }
        public static readonly DependencyProperty KiAppendValuesLavaVisibilityProperty;
        public Visibility KiAppendValuesLavaVisibility
        {
            get { return (Visibility)GetValue(KiAppendValuesLavaVisibilityProperty); }
            set { SetValue(KiAppendValuesLavaVisibilityProperty, value); }
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

        #region New EventHandlers

        private void AddMineFieldMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(lstBoxMine.ItemsSource is ObservableCollection<MineField> mineFields)
            {
                if((lstBoxMine.DataContext as Plast).TypeDev.Equals(CAMERA_DEV))
                {
                    mineFields.Add(new Camera());
                }
                else
                {
                    mineFields.Add(new Lava());
                }
            }
        }

        private void RemoveMineFieldMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if(lstBoxMine.ItemsSource is ObservableCollection<MineField> mineFields)
            {
                if (lstBoxMine.SelectedItem != null)
                    mineFields.Remove((MineField)lstBoxMine.SelectedItem);
            }
        }

        private void RemoveSelectionMenuItem_Click(object sender, RoutedEventArgs e)
        {
            if (lstBoxMine.SelectedItem != null)
                lstBoxMine.SelectedItem = null;
        }

        private void CoefficientKiNewValuesExpander_Expanded(object sender, RoutedEventArgs e)
        {
            KiValueVisibility = Visibility.Collapsed;
            if((lstBoxMine.DataContext as Plast).TypeDev.Equals(CAMERA_DEV))
            {
                KiAppendValuesCameraVisibility = Visibility.Visible;
                KiAppendValuesLavaVisibility = Visibility.Collapsed;
            }
            else
            {
                KiAppendValuesCameraVisibility = Visibility.Collapsed;
                KiAppendValuesLavaVisibility = Visibility.Visible;
            }
        }

        private void CoefficientKiNewValuesExpander_Collapsed(object sender, RoutedEventArgs e)
        {
            KiValueVisibility = Visibility.Visible;
        }

        #endregion
    }
}
