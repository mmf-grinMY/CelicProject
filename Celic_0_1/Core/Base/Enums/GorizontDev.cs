using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace Celic
{
    public enum GorizontDev
    {
        undefine = 0,
        I = 1,
        II = 2,
        III = 3,
        IV = 4,
        special = 5
    }
    public static class GorizontManager
    {
        private const string I = "I";
        private const string II = "II";
        private const string III = "III";
        private const string IV = "IV";
        private const string special = "-305 м";

        public static GorizontDev ToGorizont(string data)
        {
            switch (data)
            {
                case I: return GorizontDev.I;
                case II: return GorizontDev.II;
                case III: return GorizontDev.III;
                case IV: return GorizontDev.IV;
                case special: return GorizontDev.special;
                default: return GorizontDev.undefine;
            }
        }
        public static string ToString(GorizontDev data)
        {
            switch (data)
            {
                case GorizontDev.I: return I;
                case GorizontDev.II: return II;
                case GorizontDev.III: return III;
                case GorizontDev.IV: return IV;
                case GorizontDev.special: return special;
                default: return string.Empty;
            }
        }
    }
    public class GorizontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GorizontManager.ToString((GorizontDev)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null ? GorizontManager.ToGorizont(value.ToString()) : GorizontDev.undefine;
        }
    }
}
