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

namespace Celic.WPF.CustomControls
{
    /// <summary>
    /// Выполните шаги 1a или 1b, а затем 2, чтобы использовать этот пользовательский элемент управления в файле XAML.
    ///
    /// Шаг 1a. Использование пользовательского элемента управления в файле XAML, существующем в текущем проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Celic.WPF.CustomControls"
    ///
    ///
    /// Шаг 1б. Использование пользовательского элемента управления в файле XAML, существующем в другом проекте.
    /// Добавьте атрибут XmlNamespace в корневой элемент файла разметки, где он 
    /// будет использоваться:
    ///
    ///     xmlns:MyNamespace="clr-namespace:Celic.WPF.CustomControls;assembly=Celic.WPF.CustomControls"
    ///
    /// Потребуется также добавить ссылку из проекта, в котором находится файл XAML,
    /// на данный проект и пересобрать во избежание ошибок компиляции:
    ///
    ///     Щелкните правой кнопкой мыши нужный проект в обозревателе решений и выберите
    ///     "Добавить ссылку"->"Проекты"->[Поиск и выбор проекта]
    ///
    ///
    /// Шаг 2)
    /// Теперь можно использовать элемент управления в файле XAML.
    ///
    ///     <MyNamespace:InputDataField/>
    ///
    /// </summary>
    [TemplatePart(Name ="PART_ClearableTextBox", Type = typeof(ClearableTextBox))]
    public class InputDataField : Label
    {
        static InputDataField()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(InputDataField), new FrameworkPropertyMetadata(typeof(InputDataField)));
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(InputDataField),
                new FrameworkPropertyMetadata(OnTextChanged));
        }

        private static void OnTextChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (!e.OldValue.ToString().Contains("Binding"))
            {
                (sender as InputDataField).Text = e.NewValue.ToString();
            }
        }
        public static DependencyProperty TextProperty;

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set
            {
                SetValue(TextProperty, value);
            }
        }

        
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var clearTB = this.GetTemplateChild("PART_ClearableTextBox") as ClearableTextBox;
        }
    }
}
