using System;
using System.Windows;
using System.Windows.Controls;

namespace Celic.WPF.CustomControls
{
	/// <summary>
	/// Description of Scanner.
	/// </summary>
	public class Scanner : Control
	{
		public readonly static DependencyProperty DescriptionProperty =
			DependencyProperty.Register("Description", typeof(string), typeof(Scanner));
		public string Description
		{
			get { return GetValue(DescriptionProperty).ToString(); }
			set { SetValue(DescriptionProperty, value); }
		}
		public readonly static DependencyProperty InfoProperty =
			DependencyProperty.Register("Info", typeof(string), typeof(Scanner));
		public string Info
		{
			get { return GetValue(InfoProperty).ToString(); }
			set { SetValue(InfoProperty, value); }
		}
		public static DependencyProperty ValueProperty =
			DependencyProperty.Register("Value", typeof(string), typeof(Scanner));
		public string Value
		{
			get { return GetValue(ValueProperty).ToString(); }
			set { SetValue(ValueProperty, value); }
		}
		public static readonly DependencyProperty GridWidthProperty =
			DependencyProperty.Register("GridWidth", typeof(GridLength), typeof(Scanner));
		public GridLength GridWidth
		{
			get { return (GridLength)GetValue(GridWidthProperty); }
			set { SetValue(GridWidthProperty, value); }
		}
		public Scanner()
		{
			GridWidth = new GridLength(2, GridUnitType.Star);
		}
	}
}