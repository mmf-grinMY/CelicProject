using System.Windows;
using System.Windows.Controls;
using static Celic.HelpManager;

namespace Celic
{
    /// <summary> Логика взаимодействия для EPlastDataBox.xaml </summary>
    public partial class EPlastDataBox : UserControl
    {
        #region Contructors

        /// <summary> Основной конструктор для данного класса </summary>
        public EPlastDataBox()
        {
            ECKVisibility = ELKVisibility = EKiVisibility = ELKiVisibility = ECKiVisibility = Visibility.Collapsed;
            InitializeComponent();
        }
        /// <summary> Статический конструктор для данного класса </summary>
        static EPlastDataBox()
        {
            KiSimpleInputVisibilityProperty = DependencyProperty.Register("SKiVisibility", typeof(Visibility), typeof(EPlastDataBox));
            KiExtentedInputVisibilityProperty = DependencyProperty.Register("EKiVisibility", typeof(Visibility), typeof(EPlastDataBox));
            KiExtentedCameraVisibilityProperty = DependencyProperty.Register("ECKiVisibility", typeof(Visibility), typeof(EPlastDataBox));
            KiExtentedLavaVisibilityProperty = DependencyProperty.Register("ELKiVisibility", typeof(Visibility), typeof(EPlastDataBox));

            KSimpleInputVisibilityProperty = DependencyProperty.Register("SKVisibility", typeof(Visibility), typeof(EPlastDataBox));
            KExtentedLavaInputVisibilityProperty = DependencyProperty.Register("ELKVisibility", typeof(Visibility), typeof(EPlastDataBox));
            KExtentedCameraInputVisibilityProperty = DependencyProperty.Register("ECKVisibility", typeof(Visibility), typeof(EPlastDataBox));

            ContiguosSimpleInputVisibility = DependencyProperty.Register("SContiguosVisibility", typeof(Visibility), typeof(EPlastDataBox));
            ContiguosExtentedInputVisibility = DependencyProperty.Register("EContiguosVisibility", typeof(Visibility), typeof(EPlastDataBox));
        }

        #endregion

        #region Ki InputFields Visibility

        // kiSimpleInput Visibility Properties
        //------------------------------------
        /// <summary> Свойство видимости kiSimpleInput </summary>
        public static DependencyProperty KiSimpleInputVisibilityProperty;
        /// <summary> Обертка для свойства видимости kiSimpleInput </summary>
        public Visibility SKiVisibility
        {
            set { SetValue(KiSimpleInputVisibilityProperty, kiSimpleInput.Visibility = value); }
            get { return (Visibility)GetValue(KiSimpleInputVisibilityProperty); }
        }
        //----------------------------------------
        // kiExtentedInput Visibility Properties
        //------------------------------------------
        /// <summary> Свойство видимости kiExtentedInput </summary>
        public static DependencyProperty KiExtentedInputVisibilityProperty;
        /// <summary> Обертка для свойства KiExtentedInput </summary>
        public Visibility EKiVisibility
        {
            set { SetValue(KiExtentedInputVisibilityProperty, value); }
            get { return (Visibility)GetValue(KiExtentedInputVisibilityProperty); }
        }
        //--------------------------------------------
        // kiExtentedCamera Visibility Properties
        //---------------------------------------
        /// <summary> Свойство видимости kiExtentedCameraInput </summary>
        public static DependencyProperty KiExtentedCameraVisibilityProperty;
        /// <summary> Обертка для свойства kiExtentedCameraInput </summary>
        public Visibility ECKiVisibility
        {
            set { SetValue(KiExtentedCameraVisibilityProperty, value); }
            get { return (Visibility)GetValue(KiExtentedCameraVisibilityProperty); }
        }
        //----------------------------------------
        // kiExtentedLavaInput Visibility Properties
        //------------------------------------------
        /// <summary> Свойство видимости kiExtentedLavaInput </summary>
        public static DependencyProperty KiExtentedLavaVisibilityProperty;
        /// <summary> Обертка для свойства kiExtentedLavaInput </summary>
        public Visibility ELKiVisibility
        {
            set { SetValue(KiExtentedLavaVisibilityProperty, value); }
            get { return (Visibility)GetValue(KiExtentedLavaVisibilityProperty); }
        }

        #endregion

        #region K InputFields Visibility

        // kSimpleInput Visibility Properties
        //------------------------------------
        /// <summary> Свойство видимости kSimpleInput </summary>
        public static DependencyProperty KSimpleInputVisibilityProperty;
        /// <summary> Обертка для свойства видимости kSimpleInput </summary>
        public Visibility SKVisibility
        {
            set { SetValue(KSimpleInputVisibilityProperty, value); }
            get { return (Visibility)GetValue(KSimpleInputVisibilityProperty); }
        }
        // kExtentedLavaInput Visibility Properties
        //------------------------------------
        /// <summary> Свойство видимости kExtentedLavaInput </summary>
        public static DependencyProperty KExtentedLavaInputVisibilityProperty;
        /// <summary> Обертка для свойства видимости kExtentedLavaInput </summary>
        public Visibility ELKVisibility
        {
            set { SetValue(KExtentedLavaInputVisibilityProperty, value); }
            get { return (Visibility)GetValue(KExtentedLavaInputVisibilityProperty); }
        }
        // kExtentedCameraInput Visibility Properties
        //------------------------------------
        /// <summary> Свойство видимости kExtentedCameraInput </summary>
        public static DependencyProperty KExtentedCameraInputVisibilityProperty;
        /// <summary> Обертка для свойства видимости kExtentedCameraInput </summary>
        public Visibility ECKVisibility
        {
            set { SetValue(KExtentedCameraInputVisibilityProperty, value); }
            get { return (Visibility)GetValue(KExtentedCameraInputVisibilityProperty); }
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

        /// <summary> Логика открытия дополнительных полей ввода для расчета Ki </summary>
        /// <param name="sender"> Вызываемый объект </param>
        /// <param name="e"> Аргументы вызова </param>
        private void CheckBoxKiExtented_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.IsChecked == true)
            {
                SKiVisibility = Visibility.Collapsed;
                EKiVisibility = Visibility.Visible;
                if (DataContext != null)
                {
                    if ((DataContext as SCalcBViewModel).SelectedPlast.TypeDev.Equals(CAMERA_DEV))
                    {
                        ECKiVisibility = Visibility.Visible;
                        ELKiVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        ECKiVisibility = Visibility.Collapsed;
                        ELKiVisibility = Visibility.Visible;
                    }
                }
                checkBox.IsChecked = false;
            }
        }
        /// <summary> Логика открытия главного поля ввода Ki </summary>
        /// <param name="sender"> Вызываемый объект </param>
        /// <param name="e"> Аргументы вызова </param>
        private void CheckBoxKiSimple_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.IsChecked == true)
            {
                ELKiVisibility = ECKiVisibility = EKiVisibility = Visibility.Collapsed;
                SKiVisibility = Visibility.Visible;
                checkBox.IsChecked = false;
            }
        }
        /// <summary> Обработчик события "Задание дополнительных параметров для расчета K" </summary>
        /// <param name="sender"> Вызываемый CheckBox </param>
        /// <param name="e"> Аргументы вызова </param>
        private void CheckBoxKExtented_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.IsChecked == true)
            {
                SKVisibility = Visibility.Collapsed;
                ELKVisibility = (DataContext as SCalcBViewModel).SelectedPlast.TypeDev.Equals(CAMERA_DEV) ? Visibility.Collapsed : Visibility.Visible;
                ECKVisibility = Visibility.Visible;
                checkBox.IsChecked = false;
            }
        }
        /// <summary> Обработчик события "Задание коэффициента выработанного пространства явным образом" </summary>
        /// <param name="sender"> Вызываемый CheckBox </param>
        /// <param name="e"> Аргументы вызова </param>
        private void CheckBoxKSimple_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.IsChecked == true)
            {
                ECKVisibility = ELKVisibility = Visibility.Collapsed;
                SKVisibility = Visibility.Visible;
                checkBox.IsChecked = false;
            }
        }
        /// <summary> Обработчик события "Изменение системы разработки пласта" </summary>
        /// <param name="sender"> Вызываемый ComboBox </param>
        /// <param name="e"> Аргументы вызова </param>
        private void ComboBoxTypeDev_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox && DataContext != null)
            {
                if (EKiVisibility == Visibility.Visible)
                {
                    if ((DataContext as Plast).TypeDev.Equals(LAVA_DEV))
                    {
                        ECKiVisibility = Visibility.Visible;
                        ELKiVisibility = Visibility.Collapsed;
                    }
                    else
                    {
                        ECKiVisibility = Visibility.Collapsed;
                        ELKiVisibility = Visibility.Visible;
                    }
                }
                if (SKVisibility == Visibility.Collapsed)
                    ELKVisibility = (DataContext as Plast).TypeDev.Equals(LAVA_DEV) ? Visibility.Collapsed : Visibility.Visible;
            }
        }
        /// <summary> Обработчик событий "Открыть возможность задания дополнительных данных пласта" </summary>
        /// <param name="sender"> Вызываемый ComboBox </param>
        /// <param name="e"> Аргументы вызова </param>
        private void CheckBoxPlastExtented_Click(object sender, RoutedEventArgs e)
        {
            if(sender is CheckBox checkBox && checkBox.IsChecked == true)
            {
                PlastSimpleInput.Visibility = Visibility.Collapsed;
                PlastExtentedInput.Visibility = Visibility.Visible;
                if(DataContext is Plast plast)
                {
                    plast.S = plast.Sz = plast.Kt = new EFloat(0);
                }
                checkBox.IsChecked = false;
            }
        }
        /// <summary> Обработчик событий "Закрыть возможность задания дополнительных данных пласта" </summary>
        /// <param name="sender"> Вызываемый ComboBox </param>
        /// <param name="e"> Аргументы вызова </param>
        private void CheckBoxPlastSimple_Click(object sender, RoutedEventArgs e)
        {
            if (sender is CheckBox checkBox && checkBox.IsChecked == true)
            {
                PlastExtentedInput.Visibility = Visibility.Collapsed; 
                PlastSimpleInput.Visibility = Visibility.Visible;
                if (DataContext is Plast plast)
                {
                    plast.S = plast.Sz = plast.Kt = new EFloat(1);
                }
                checkBox.IsChecked = false;
            }
        }

        #endregion
    }
}
