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
    /// <summary> Логика взаимодействия для EPlastDataItem.xaml </summary>
    public partial class EPlastDataItem : UserControl 
    {
        #region Constructors

        /// <summary> Основной конструктор дял данного класса </summary>
        public EPlastDataItem() => InitializeComponent();
        /// <summary> Статический конструктор для данного класса </summary>
        static EPlastDataItem()
        {
            KiSimpleInputVisibilityProperty = DependencyProperty.Register("SKiVisibility", typeof(Visibility), typeof(EPlastDataItem));
            KiExtentedInputVisibilityProperty = DependencyProperty.Register("EKiVisibility", typeof(Visibility), typeof(EPlastDataItem));
            KiExtentedCameraVisibilityProperty = DependencyProperty.Register("ECKiVisibility", typeof(Visibility), typeof(EPlastDataItem));
            KiExtentedLavaVisibilityProperty = DependencyProperty.Register("ELKiVisibility", typeof(Visibility), typeof(EPlastDataItem));

            KSimpleInputVisibilityProperty = DependencyProperty.Register("SKVisibility", typeof(Visibility), typeof(EPlastDataItem));
            KExtentedLavaInputVisibilityProperty = DependencyProperty.Register("ELKVisibility", typeof(Visibility), typeof(EPlastDataItem));
            KExtentedCameraInputVisibilityProperty = DependencyProperty.Register("ECKVisibility", typeof(Visibility), typeof(EPlastDataItem));
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
            set { SetValue(KiSimpleInputVisibilityProperty, value); }
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
    }
}
