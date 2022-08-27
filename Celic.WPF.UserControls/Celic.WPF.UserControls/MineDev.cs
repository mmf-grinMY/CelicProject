using System;
using System.Windows.Data;

namespace Celic.WPF.UserControls
{
	/// <summary> Система разработки шахтного поля </summary>
	public enum MineDev
	{
        undefine = 0,
		camera = 1,
        lava = 2
	}
	
	/// <summary>  </summary>
	public static class MineDevManager
	{
		public const string CAMERA_DEV = "камерная";
		public const string LAVA_DEV = "столбовая";
		public static string ToString(MineDev dev)
		{
			switch (dev)
			{
				default:
				case MineDev.undefine: return string.Empty;
				case MineDev.camera: return CAMERA_DEV;
				case MineDev.lava: return LAVA_DEV;
			}
		}
		public static MineDev ToMineDev(string str)
		{
			switch (str)
			{
				case CAMERA_DEV: return MineDev.camera;
				case LAVA_DEV: return MineDev.lava;
				default: return MineDev.undefine;
			}
		}
	}
	/// <summary> </summary>
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