using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Celic.WPF.CustomControls
{
    public class NameScanner : Control
    {
		public readonly static DependencyProperty DescriptionProperty =
			DependencyProperty.Register("Description", typeof(string), typeof(NameScanner));
		public string Description
		{
			get { return GetValue(DescriptionProperty).ToString(); }
			set { SetValue(DescriptionProperty, value); }
		}
		public readonly static DependencyProperty InfoProperty =
			DependencyProperty.Register("Info", typeof(string), typeof(NameScanner));
		public string Info
		{
			get { return GetValue(InfoProperty).ToString(); }
			set { SetValue(InfoProperty, value); }
		}
	}
}
