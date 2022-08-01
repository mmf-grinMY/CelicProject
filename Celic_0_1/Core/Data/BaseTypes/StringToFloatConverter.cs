using System;
using System.Globalization;
using System.Windows.Data;

namespace Celic
{
    /// <summary> Преобразование типов System.String и Celic.EFloat друг в друга </summary>
    [ValueConversion(typeof(string), typeof(EFloat))]
    public class StringToFloatConverter : IValueConverter
    {
        /// <summary> Преобразование переменной из EFloat в string </summary>
        /// <param name="value"> Исходное значение преобразуемой переменной </param>
        /// <param name="typeTarget"> Тип, в который необходимо конвертировать </param>
        /// <param name="param"> Используемый параметр преобразователя </param>
        /// <param name="culture"> Язык и региональные параметры, используемые в преобразователе </param>
        /// <returns> Преобразованное значение, если исходная переменная не равна null и string.Empty в противном случае </returns>
        public object Convert(object value, Type typeTarget, object param, CultureInfo culture) =>
             value != null ? value.ToString() : string.Empty;
        /// <summary> Преобразование переменной из string в EFloat </summary>
        /// <param name="value"> Исходное значение преобразуемой переменной </param>
        /// <param name="typeTarget"> Тип, в который необходимо конвертировать </param>
        /// <param name="param"> Используемый параметр преобразователя </param>
        /// <param name="culture"> Язык и региональные параметры, используемые в преобразователе </param>
        /// <returns> Преобразованное значение, если исходная переменная не равна null и значение EFloat по умолчанию  в противном случае </returns>
        public object ConvertBack(object value, Type typeTarget, object param, CultureInfo culture) =>
            value != null && value.ToString() != "" ? (object)EFloat.Parse(value.ToString()) : new EFloat();
    }
}
