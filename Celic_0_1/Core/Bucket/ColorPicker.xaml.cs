using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Celic
{
    /// <summary>
    /// Логика взаимодействия для ColorSelector.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        /// <summary>
        /// 
        /// </summary>
        public static DependencyProperty ColorProperty;
        /// <summary>
        /// 
        /// </summary>
        public static DependencyProperty RedProperty;
        /// <summary>
        /// 
        /// </summary>
        public static DependencyProperty GreenProperty;
        /// <summary>
        /// 
        /// </summary>
        public static DependencyProperty BlueProperty;
        /// <summary>
        /// 
        /// </summary>
        public Color Color
        {
            get => (Color)GetValue(ColorProperty);
            set => SetValue(ColorProperty, value); 
        }
        /// <summary>
        /// 
        /// </summary>
        public byte Red
        {
            get => (byte)GetValue(RedProperty);
            set => SetValue(RedProperty, value);
        }
        /// <summary>
        /// 
        /// </summary>
        public byte Green
        {
            get => (byte)GetValue(GreenProperty);
            set => SetValue(GreenProperty, value);
        }
        /// <summary>
        /// 
        /// </summary>
        public byte Blue
        {
            get => (byte)GetValue(BlueProperty);
            set => SetValue(BlueProperty, value);
        }
        private static void OnColorRGBChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ColorPicker colorPicker = (ColorPicker)sender;
            Color color = colorPicker.Color;
            if (e.Property == RedProperty)
                color.R = (byte)e.NewValue;
            else if (e.Property == GreenProperty)
                color.G = (byte)e.NewValue;
            else if (e.Property == BlueProperty)
                color.B = (byte)e.NewValue;
            colorPicker.Color = color;
        }
        private static void OnColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            Color newColor = (Color)e.NewValue;
            ColorPicker colorPicker = (ColorPicker)sender;
            colorPicker.Red = newColor.R;
            colorPicker.Green = newColor.G;
            colorPicker.Blue = newColor.B;
            Color oldColor = (Color)e.OldValue;
            RoutedPropertyChangedEventArgs<Color> args =
                new RoutedPropertyChangedEventArgs<Color>(oldColor, newColor)
                {
                    RoutedEvent = ColorPicker.ColorChangedEvent
                };
            colorPicker.RaiseEvent(args);
        }
        /// <summary>
        /// 
        /// </summary>
        public static readonly RoutedEvent ColorChangedEvent;
        /// <summary>
        /// 
        /// </summary>
        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add => AddHandler(ColorChangedEvent, value); 
            remove => RemoveHandler(ColorChangedEvent, value); 
        }
        /// <summary>
        /// 
        /// </summary>
        public ColorPicker()
        {
            InitializeComponent();
        }
        static ColorPicker()
        {
            ColorProperty = DependencyProperty.Register("Color", typeof(Color), typeof(ColorPicker),
                new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChanged)));
            RedProperty = DependencyProperty.Register("Red", typeof(byte), typeof(ColorPicker),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            GreenProperty = DependencyProperty.Register("Green", typeof(byte), typeof(ColorPicker),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            BlueProperty = DependencyProperty.Register("Blue", typeof(byte), typeof(ColorPicker),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            ColorChangedEvent = EventManager.RegisterRoutedEvent("СоlorChanged", RoutingStrategy.Bubble,
                typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorPicker));
        }
    }
}
