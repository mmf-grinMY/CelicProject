using static System.Math;

namespace Celic
{
    /// <summary> Шахтное поле со столбовой системой разработки </summary>
    class Lava : MineField
    {
        #region Private Fields

        /// <summary> Расстояние между штреками ( поле ) </summary>
        private float _b;
        /// <summary> Площадь поперечного сечения лавы ( поле ) </summary>
        private float _sl;
        /// <summary> Длина лавы ( поле ) </summary>
        private float _l;

        #endregion

        #region Public Properties

        /// <summary> Расстояние между штреками </summary>
        public float B
        {
            get => _b;
            set
            {
                _b = value;
                OnPropertyChanged("B");
            }
        }
        /// <summary> Площадь поперечного сечения лавы </summary>
        public float Sl
        {
            get => _sl;
            set
            {
                _sl = value;
                OnPropertyChanged("Sl");
            }
        }
        /// <summary> Длина лавы </summary>
        public float L
        {
            get => _l;
            set
            {
                _l = value;
                OnPropertyChanged("L");
            }
        }

        #endregion

        #region Contructors

        /// <summary> Основной конструктор для данного класса </summary>
        public Lava() : base() => B = Sl = L = 0;

        #endregion
    }

    /// <summary> Класс, обслуживающий структуру данных Lava </summary>
    class LavaManager : IMineFieldManage
    {
        #region Private Fields

        /// <summary> Шахтное поле со столбовой системой разработки </summary>
        private readonly Lava _lava;

        #endregion

        #region Public Methods

        /// <summary> Расчет высоты зоны водопроводящих целиков </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        public float Ht() => MPr() * CalcD();
        /// <summary> Расчет приведенной вынимаемой мощности при столбовой системе разработки </summary>
        /// <returns> Значение приведенной вынимаемой мощности </returns>
        public float MPr() => _lava.L != 0 ? _lava.Sl / _lava.L * _lava.K : 0;
        /// <summary> Расчет параметра, зависящего от системы разработки </summary>
        /// <returns> Значение параметра, зависящего от системы разработки </returns>
        public float CalcD() => 46 - 0.01F * _lava.H;
        /// <summary> Расчет значения коэффицента K при столбовой системе разработки для одиночной лавы </summary>
        /// <returns> Значение коэффицента K при столбовой системе разработки </returns>
        public float RecalcK()
        {
            float d0 = 1.4F * (46 - 0.01F * _lava.H) * _lava.Mv;
            return _lava.K = _lava.D >= d0 ? 1 : (d0 != 0 ? (float)Sqrt(_lava.D / d0) : 0);
        }
        /// <summary> Расчет значения коэффицента K при столбовой системе разработки для неодиночной лавы </summary>
        /// <param name="other"> Соседняя лава </param>
        /// <returns> Значение коэффицента K при столбовой системе разработки </returns>
        public float ERecalcK(Lava other)
        {
            float d0 = 1.4F * CalcD() * _lava.Mv;
            float d1 = _lava.D;
            float d = other.D;
            float b = _lava.B;
            if (300 >= Max(d, d1))
                return _lava.K = Max(d, d1) >= d0 ? 1 : Max(d, d1) + b >= d0 ? (float)Sqrt(d0 != 0 ? Max(d, d1) / d0 : 0) : (d + 2 * b + d1) != 0 ? (d + d1) / (d + 2 * b + d1) : 0;
            return 0;
        }

        #endregion

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        /// <param name="lava"> Рассматриваемая лава </param>
        public LavaManager(Lava lava) => _lava = lava;

        #endregion
    }
}
