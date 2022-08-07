using System;
using System.Windows.Controls;

namespace Celic
{
    /// <summary> Валидация числа EFloat на вхождение в определенный диапазон значений </summary>
    public class RangeValidationRule : ValidationRule
    {
        #region Private Fields

        /// <summary> Начальное значение диапазона ( поле ) </summary>
        private float _min = 0;
        /// <summary> Конечное значенеи диапазона ( поле ) </summary>
        private float _max = 1;

        #endregion

        #region Public Properties

        /// <summary> Начальное значение диапазона ( поле ) </summary>
        public float Min
        {
            get => _min;
            set => _min = value;
        }
        /// <summary> Конечное значенеи диапазона ( поле ) </summary>
        public float Max
        {
            get => _max;
            set => _max = value;
        }

        #endregion

        #region Public Methods

        /// <summary> Проверка числа на вхождение в указанный диапазон </summary>
        /// <param name="value"> Число </param>
        /// <param name="ci"> Региональные параметры </param>
        /// <returns> Результат валидации </returns>
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo ci)
        {
            if(value != null && value.ToString() != "")
            {
                EFloat range = EFloat.Parse(HelpManager.StringIsNumber(value.ToString()));
                return (range.V >= Min && (range.V < Max || (range.V == Max && range.IsFloat()))) ?
                    new ValidationResult(true, null) :
                    new ValidationResult(false, "Значение не входит в допустимый диапазон " + Min + " до " + Max + ".");                    
            }
            return new ValidationResult(true, null);
        }

        #endregion
    }
}
