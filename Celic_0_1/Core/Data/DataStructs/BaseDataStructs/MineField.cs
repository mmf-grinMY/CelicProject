namespace Celic
{
    /// <summary> Шахтное поле </summary>
    public class MineField : BaseViewModel
    {
        #region Private Fields

        /// <summary> Вынимаемая мощность пласта(высота камер) ( поле ) </summary>
        private float _mv;
        /// <summary> Глубина ведения горных работ в пределах от 350 до 1000 м ( поле ) </summary>
        private float _h;
        /// <summary> Ширина выработанного пространства ( поле ) </summary>
        private float _d;
        /// <summary> Коэффициент, учитывающий размер выработанного пространства ( степень подработанности породного массива ) ( поле ) </summary>
        private float _k;
        /// <summary> Коэффициент извлечения рудной массы в пределах вынимаемой мощности ( поле ) </summary>
        private float _ki;
        /// <summary> Расстояние между соседними осями междукамерных целиков ( поле ) </summary>
        private float _l;

        #endregion

        #region Public Properties

        /// <summary> Вынимаемая мощность пласта(высота камер), м </summary>
        public float Mv
        {
            get => _mv;
            set
            {
                _mv = value;
                OnPropertyChanged("Mv");
            }
        }
        /// <summary> Глубина ведения горных работ в пределах от 350 до 1000 м </summary>
        public float H
        {
            get => _h;
            set
            {
                _h = value;
                OnPropertyChanged("H");
            }
        }
        /// <summary> Ширина выработанного пространства </summary>
        public float D
        {
            get => _d;
            set
            {
                _d = value;
                OnPropertyChanged("D");
            }
        }
        /// <summary> Коэффициент, учитывающий размер выработанного пространства ( степень подработанности породного массива ) </summary>
        public float K
        {
            get => _k;
            set
            {
                _k = value;
                OnPropertyChanged("K");
            }
        }
        /// <summary> Коэффициент извлечения рудной массы в пределах вынимаемой мощности </summary>
        public float Ki
        {
            get => _ki;
            set
            {
                _ki = value;
                OnPropertyChanged("Ki");
            }
        }
        /// <summary> Расстояние между соседними осями междукамерных целиков </summary>
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

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        public MineField() => Mv = H = K = 0;

        #endregion
    }
}
