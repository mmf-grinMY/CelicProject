using System;
using System.Windows.Controls;
using System.Windows;

namespace Celic
{
	/// <summary>
	/// Description of Printer.
	/// </summary>
	public class Printer : Control
	{
		public readonly static DependencyProperty DescriptionProperty =
			DependencyProperty.Register("Description", typeof(string), typeof(Printer));
		public string Description
		{
			get { return GetValue(DescriptionProperty).ToString(); }
			set { SetValue(DescriptionProperty, value); }
		}
		public readonly static DependencyProperty InfoProperty =
			DependencyProperty.Register("Info", typeof(string), typeof(Printer));
		public string Info
		{
			get { return GetValue(InfoProperty).ToString(); }
			set { SetValue(InfoProperty, value); }
		}
		public static DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(string), typeof(Printer));
		public string Value
		{
			get { return GetValue(ValueProperty).ToString(); }
			set { SetValue(ValueProperty, value); }
		}
		public static readonly DependencyProperty GridWidthProperty =
			DependencyProperty.Register("GridWidth", typeof(GridLength), typeof(Printer));
		public GridLength GridWidth
		{
			get { return (GridLength)GetValue(GridWidthProperty); }
			set { SetValue(GridWidthProperty, value); }
		}
		public Printer()
		{
			GridWidth = new GridLength(2, GridUnitType.Star);
		}
	}
}