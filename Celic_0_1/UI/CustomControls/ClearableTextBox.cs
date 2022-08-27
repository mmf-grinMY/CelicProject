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
    [TemplatePart(Name = "PART_Button", Type = typeof(Button))]
    public class ClearableTextBox : TextBox
    {
        static ClearableTextBox()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(ClearableTextBox), new FrameworkPropertyMetadata(typeof(ClearableTextBox)));
        }
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            
            var button = this.GetTemplateChild("PART_Button") as Button;
            if(button != null)
            {
                button.Click += button_Click;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var oldText = Text;
            Text = string.Empty;
            RaiseTextCleared(oldText);
        }

        public event EventHandler<TextClearedEventArgs> TextCleared;
        protected void RaiseTextCleared(string oldText)
        {
            if(TextCleared != null)
            {
                TextCleared?.Invoke(this, new TextClearedEventArgs(oldText));
            }
        }
    }

    public class TextClearedEventArgs : EventArgs
    {
        public TextClearedEventArgs(string oldText)
        {
            OldText = oldText;
        }
        public string OldText { get; set; }
    }
}
