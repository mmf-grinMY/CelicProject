using System;
using System.Windows.Data;

namespace Celic
{
	/// <summary>
	/// Description of MineDevConverter.
	/// </summary>
	public class MineDevConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return MineDevManager.ToString((MineDev)value);
		}
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return value != null ? MineDevManager.ToMineDev(value.ToString()) : MineDev.undefine;	
		}
		
	}
}
