using System;

namespace Celic
{
    /// <summary> Шахтное поле </summary>
    public struct MineField
    {
        #region Private Fields

        /// <summary> Вынимаемая мощность пласта(высота камер), м ( поле ) </summary>
        private string _mv;
        /// <summary> Глубина ведения горных работ в пределах от 350 до 1000 м ( поле )</summary>
        private string _h;
        /// <summary> Коэффициент извлечения рудной массы в пределах вынимаемой мощности ( поле ) </summary>
        private string _ki;

        #endregion

        #region Public Properties

        /// <summary> Вынимаемая мощность пласта(высота камер), м </summary>
        public string Mv
        {
            set
            {
                if (value != "" && value != "0" && value != "0,")
                {
                    value = HelpManager.RmNull(value);
                    value = (value != "" && double.TryParse(value, out double tmp) && tmp > 0 && tmp <= 3) ? value : "0";
                }
                _mv = value;
            }
            get => _mv;
        }
        /// <summary> Система разработки шахтного поля </summary>
        public string TypeDev { get; set; }
        /// <summary> Глубина ведения горных работ в пределах от 350 до 1000 м </summary>
        public string H
        {
            set
            {
                if (value != "" && value != "0")
                    if (value.Length >= 4 && double.TryParse(value, out double h))
                    {
                        if (h > 1000)
                            value = "1000";
                    }
                    else if (value != "0,")
                    {
                        if (value.Length >= 4 && Char.IsDigit(value[0]) && Char.IsDigit(value[1]) &&
                                Char.IsDigit(value[2]) && Char.IsDigit(value[3]))
                            value = value.Remove(3, 1);
                        value = HelpManager.RmNull(value);
                        value = (value != "" && Double.TryParse(value, out h) && h > 0) ? value : "0";
                    }
                _h = value;
            }
            get => _h;
        }
        /// <summary> Коэффициент извлечения рудной массы в пределах вынимаемой мощности </summary>
        public string Ki
        {
            set
            {
                if (value != "" && value != "0" && value != "0,")
                {
                    value = HelpManager.RmNull(value);
                    value = (value != "" && double.TryParse(value, out double tmp) && tmp > 0 && tmp <= 1) ? value : "0";
                }
                _ki = value;
            }
            get => _ki;
        }

        #endregion

        #region Public Methods

        /// <summary> Рассчет коэффициента d, зависящего от системы разработки </summary>
        /// <returns> Значение коэффициента </returns>
        public double D()
        {
            _ = double.TryParse(_h, out double h);
            return -0.01 * h + (TypeDev == "камерная" ? 26 : 46);
        }
        /// <summary> Расчет приведенной  мощности </summary>
        /// <returns> Значение приведенной вынимаемой мощности </returns>
        public double MPr()
        {
            _ = double.TryParse(_mv, out double mv);
            _ = double.TryParse(_ki, out double ki);
            return mv * ki;
        }
        /// <summary> Расчет высоты ЗВТ для одиночного шахтного поля </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        public double Ht() => D() * MPr();

        #endregion
    }
}