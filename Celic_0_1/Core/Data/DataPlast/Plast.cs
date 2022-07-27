using System.ComponentModel;
using System.Runtime.CompilerServices;
using static System.Char;
using static System.Single;
using static Celic.HelpManager;

namespace Celic
{
    /// <summary> Хранение данных о пласте </summary>
    public class Plast
    {
        #region PropertyChanged

        /// <summary> Событие при изменении свойства компонента </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary> Обработчик для события Propertychanged </summary>
        /// <param name="prop"> Назввание свойства </param>
        public void OnPropertyChanged([CallerMemberName] string prop = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

        #endregion

        #region Private Fields

        /// <summary> Вынимаемая мощность пласта(высота камер), м ( поле ) </summary>
        private string _mv;
        /// <summary> Глубина ведения горных работ в пределах от 350 до 1000 м ( поле )</summary>
        private string _h;
        /// <summary> Коэффициент извлечения рудной массы в пределах вынимаемой мощности ( поле ) </summary>
        private string _ki;
        /// <summary> Калийный горизонт ( поле ) </summary>
        private string _gorizont;
        /// <summary> Система разработки шахтного поля ( поле ) </summary>
        private string _typeDev;
        /// <summary> Коэффициент, учитывающий размер выработанного пространства ( степень подработанности породного массива ) ( поле ) </summary>
        private string _k;

        #endregion

        #region Public Properties

        /// <summary> Вынимаемая мощность пласта(высота камер), м </summary>
        public string Mv
        {
            set
            {
                if (value != "" && value != "0" && value != "0,")
                {
                    value = RmNull(value);
                    value = (value != "" && TryParse(value, out float tmp) && tmp > 0 && tmp <= 3) ? value : "0";
                }
                _mv = value;
                OnPropertyChanged(nameof(Mv));
            }
            get => _mv;
        }
        /// <summary> Система разработки шахтного поля </summary>
        public string TypeDev 
        {
            set
            {
                _typeDev = value;
                OnPropertyChanged(nameof(TypeDev));
            }
            get => _typeDev;
        }
        /// <summary> Глубина ведения горных работ в пределах от 350 до 1000 м </summary>
        public string H
        {
            set
            {
                if (value != "" && value != "0")
                    if (value.Length >= 4 && TryParse(value, out float h))
                    {
                        if (h > 1000)
                            value = "1000";
                    }
                    else if (value != "0,")
                    {
                        if (value.Length >= 4 && IsDigit(value[0]) && IsDigit(value[1]) &&
                                IsDigit(value[2]) && IsDigit(value[3]))
                            value = value.Remove(3, 1);
                        value = RmNull(value);
                        value = (value != "" && TryParse(value, out h) && h > 0) ? value : "0";
                    }
                _h = value;
                OnPropertyChanged(nameof(H));
            }
            get => _h;
        }
        /// <summary> Коэффициент извлечения рудной массы в пределах вынимаемой мощности </summary>
        public string Ki
        {
            set
            {
                _ki = ValidateOneNumber(value);
                OnPropertyChanged(nameof(Ki));
            }
            get => _ki;
        }
        /// <summary> Калийный горизонт </summary>
        public string Gorizont
        {
            get => _gorizont;
            set
            {
                _gorizont = value;
                OnPropertyChanged(nameof(Gorizont));
            }
        }
        /// <summary> Коэффициент, учитывающий размер выработанного пространства ( степень подработанности породного массива ) </summary>
        public string K
        {
            get => _k;
            set
            {
                _k = ValidateOneNumber(value);
                OnPropertyChanged(nameof(K));
            } 
        }
        /// <summary> Глубина ведения горных работ в пределах от 350 до 1000 м ( число ) </summary>
        public float Height{ get => TryParse(_h, out float h) ? h : 0; }
        /// <summary> Ширина предохранительного приразломного целика на уровне данного пласта со стороны лежачего крыла разломной зоны, м </summary>
        public float Bl { get; set; }
        /// <summary> Ширина предохранительного приразломного целика на уровне данного пласта со стороны висячего крыла разломной зоны, м </summary>
        public float Bv { get; set; }

        //=====================================

        /// <summary> Коэффициент, учитывающий степень влияния выемки разрабатываемых пластов на развитие техногенных 
        /// водопроводящих трещин в зависимости от взаимного положения ( смещения в плане ) границ остановки работ </summary>
        public double S { get; set; }
        /// <summary> Коэффициент, учитывающий характер деформирования массива пород в краевой части мульды сдвижения над
        /// угловыми участками выработанного пространства, определяемые согласно отдельным рекомендациям
        /// специализированной организации( 0 ≤ S_z ≤ 1 ); при отсутствии таких рекомендаций значение принимается равным 1 </summary>
        public double Sz { get; set; }
        /// <summary> Коэффициент, учитывающий порядок разработки пластов и интервал времени между их отработкой </summary>
        public double Kt { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Contiguos { get; set; }

        #endregion

        #region Private Methods

        /// <summary> Валидация данных для коэффициента от 0 до 1 </summary>
        /// <param name="value"> Вводимое пользователем значение коэффициента </param>
        /// <returns> Корректное значение переменной </returns>
        private string ValidateOneNumber(string value)
        {
            if (value != "" && value != "0" && value != "0,")
            {
                value = RmNull(value);
                value = (value != "" && TryParse(value, out float tmp) && tmp > 0 && tmp <= 1) ? value : "0";
            }
            return value;
        }

        #endregion
        #region Public Methods

        /// <summary> Расчет высоты ЗВТ одиночного пласта </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        public float Ht()
        {
            _ = TryParse(H, out float h);
            _ = TryParse(Mv, out float mv);
            _ = TryParse(Ki, out float ki);
            _ = TryParse(K, out float k);
            return mv * ki * k * (-0.01F * h + (TypeDev == "камерная" ? 26 : 46));
        }
        /// <summary> Расчет коэффициента d, зависящего от системы разраьотки для пласта </summary>
        /// <returns> Значение коэффициента d</returns>
        public float D()
        {
            _ = TryParse(H, out float h);
            return -0.01F * h + (TypeDev == "камерная" ? 26 : 46);
        }
        /// <summary> Расчет приведенной вынимаемой мощности для пласта </summary>
        /// <returns> Значение приведенной вынимаеомй мощности </returns>
        public float MPr()
        {
            _ = TryParse(Mv, out float mv);
            _ = TryParse(Ki, out float ki);
            _ = TryParse(K, out float k);
            return mv * ki * k;
        }

        #endregion
    }

    /// <summary> Обработка данных пласта </summary>
    public static class PlastManager
    {
        
    }
}
