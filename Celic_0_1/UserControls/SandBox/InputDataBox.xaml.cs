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
    /// Логика взаимодействия для InputDataBox.xaml
    /// </summary>
    public partial class InputDataBox : UserControl
    {
        static InputDataBox()
        {
            BlockTextProperty = DependencyProperty.Register("BlockText", typeof(string), typeof(InputDataBox));
            DataSourceProperty = DependencyProperty.Register("DataSource", typeof(string), typeof(InputDataBox),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnDataSourceChanged)));
            ToolTipTextProperty = DependencyProperty.Register("ToolTipText", typeof(string), typeof(InputDataBox),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnToolTipTextChanged)));
            IsEFloatProperty = DependencyProperty.Register("IsEFloat", typeof(bool), typeof(InputDataBox),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnIsEFloatChanged)));
            MinProperty = DependencyProperty.Register("Min", typeof(float), typeof(InputDataBox),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnIsEFloatChanged)));
            MaxProperty = DependencyProperty.Register("Max", typeof(float), typeof(InputDataBox),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnIsEFloatChanged)));
        }

        private static void OnIsEFloatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is InputDataBox box)
            {
                if (box.IsEFloat)
                {
                    BindingOperations.ClearBinding(box.txtBox, TextBox.TextProperty);
                    Binding binding = new Binding();
                    binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                    binding.Path = new PropertyPath(box.DataSource);
                    binding.Converter = new StringToEFloatConverter();
                    RangeValidationRule validation = new RangeValidationRule();
                    validation.Min = box.Min;
                    validation.Max = box.Max;
                    binding.ValidationRules.Add(validation);
                    box.txtBox.SetBinding(TextBox.TextProperty, binding);
                }
            }
        }

        private static void OnToolTipTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is InputDataBox box)
            {
                box.txtBlock.Text = box.ToolTipText;
            }
        }

        private static void OnDataSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is InputDataBox box)
            {
                Binding binding = new Binding();
                binding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                binding.Path = new PropertyPath(box.DataSource);
                box.txtBox.SetBinding(TextBox.TextProperty, binding);
            }
        }
        public InputDataBox()
        {
            InitializeComponent();
        }
        public static DependencyProperty BlockTextProperty;
        public string BlockText
        {
            get { return GetValue(BlockTextProperty).ToString(); }
            set { SetValue(BlockTextProperty, value); }
        }
        public static DependencyProperty DataSourceProperty;
        public string DataSource
        {
            get { return GetValue(DataSourceProperty).ToString(); }
            set { SetValue(DataSourceProperty, value); }
        }
        public static DependencyProperty ToolTipTextProperty;
        public string ToolTipText
        {
            get { return GetValue(ToolTipTextProperty).ToString(); }
            set { SetValue(ToolTipTextProperty, value); }
        }
        public static DependencyProperty IsEFloatProperty;
        public bool IsEFloat
        {
            get { return (bool)GetValue(IsEFloatProperty); }
            set { SetValue(IsEFloatProperty, value); }
        }
        public static DependencyProperty MinProperty;
        public float Min
        {
            get { return (float)GetValue(MinProperty); }
            set { SetValue(MinProperty, value); }
        }
        public static DependencyProperty MaxProperty;
        public float Max
        {
            get { return (float)GetValue(MaxProperty); }
            set { SetValue(MaxProperty, value); }
        }
    }
}
