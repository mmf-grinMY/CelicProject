using System;
using System.Windows;
using System.Windows.Controls;

namespace Celic
{
    /// <summary> Логика работы TestUserControl </summary>
    public partial class TestUserControl : UserControl
    {
        /*#region Public DependencyProperties

        /// <summary> Текст в TextBox </summary>
        public static readonly DependencyProperty TextBoxTextProperty;
        /// <summary> Текст в TextBlock </summary>
        public static readonly DependencyProperty TextBlockTextProperty;

        #endregion

        #region Public RoutedEvents

        /// <summary> Событие, возникающее при изменении текста в TextBox </summary>
        public static readonly RoutedEvent TextBoxTextChangedEvent;

        #endregion

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        public TestUserControl() => InitializeComponent();
        /// <summary> Статический конструктор для данного класса ( вызывается при первом создании объекта ) </summary>
        static TestUserControl()
        {
            TextBoxTextProperty = DependencyProperty.Register("BoxText", typeof(string), typeof(TestUserControl),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnBoxTextChanged)));
            TextBlockTextProperty = DependencyProperty.Register("BlockText", typeof(string), typeof(TestUserControl),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnBlockTextChanged)));
            TextBoxTextChangedEvent = EventManager.RegisterRoutedEvent("BoxTextChanged", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<string>), typeof(TestUserControl));
        }

        #endregion

        #region Public Events Handlers

        /// <summary> Событие-обертка для изменения поля ввода TextBox </summary>
        public event RoutedPropertyChangedEventHandler<string> BoxTextChanged
        {
            add { AddHandler(TextBoxTextChangedEvent, value); }
            remove { RemoveHandler(TextBoxTextChangedEvent, value); }
        }

        #endregion

        #region Private Event Handlers

        /// <summary> Обработка события изменения текста TextBlock </summary>
        /// <param name="sender"> Объект, вызывающий событие </param>
        /// <param name="e"> Параметры вызова события </param>
        private static void OnBlockTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            TestUserControl control = (TestUserControl)sender;
            control.BlockText = e.NewValue.ToString();
        }
        /// <summary> Обработка события изменение поля ввода TextBox </summary>
        /// <param name="sender"> Объект, вызывающий событие </param>
        /// <param name="e"> Параметры вызова события </param>
        private static void OnBoxTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            int repeat = 0;
            TestUserControl control = (TestUserControl)sender;
            if (repeat == 0)
            {
                control.BoxText = e.NewValue.ToString();
                repeat++;
            }
            RoutedPropertyChangedEventArgs<string> args = 
                new RoutedPropertyChangedEventArgs<string>((string)e.OldValue, (string)e.NewValue);
            args.RoutedEvent = TextBoxTextChangedEvent;
            control.RaiseEvent(args);
        }

        #endregion

        #region Public Properties

        /// <summary> Задаваемый текст в TextBox </summary>
        public string BoxText
        {
            get => (string)GetValue(TextBoxTextProperty);
            set => SetValue(TextBoxTextProperty, value);
        }
        /// <summary> Задаваемый текст в TextBlock </summary>
        public string BlockText
        {
            get => (string)GetValue(TextBlockTextProperty);
            set => SetValue(TextBlockTextProperty, value);
        }

        #endregion*/

        #region Constructors

        public TestUserControl() : base() { }
        static TestUserControl()
        {
            KiSimpleInputVisibilityProperty = DependencyProperty.Register("SKiVisibility", typeof(Visibility), typeof(EPlastDataBox));
            KiExtentedInputVisibilityProperty = DependencyProperty.Register("EKiVisibility", typeof(Visibility), typeof(EPlastDataBox));
            KiExtentedCameraVisibilityProperty = DependencyProperty.Register("ECKiVisibility", typeof(Visibility), typeof(EPlastDataBox));
            KiExtentedLavaVisibilityProperty = DependencyProperty.Register("ELKiVisibility", typeof(Visibility), typeof(EPlastDataBox));
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
    }
}
