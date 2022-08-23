using System;
using System.Windows.Controls;
using System.Windows;

namespace Celic
{
	/// <summary>
	/// Description of HorizontalList.
	/// </summary>
	public class HorizontalList : Control
	{
		public static readonly DependencyProperty TemplateProperty = 
			DependencyProperty.Register("Template", typeof(DependencyObject), typeof(HorizontalList));
		public DependencyObject Template
		{
			get {return (DependencyObject)GetValue(TemplateProperty);}
			set{SetValue(TemplateProperty, value);}
		}
	}
}
