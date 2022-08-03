namespace Celic
{
    /// <summary> Шахтное поле </summary>
    public abstract class MineField
    {
        #region Public Properties

        /// <summary> Вынимаемая мощность пласта(высота камер), м </summary>
        public EFloat Mv { get; set; }
        /// <summary> Глубина ведения горных работ в пределах от 350 до 1000 м </summary>
        public EFloat H { get; set; }
        /// <summary> Система разработки шахтного поля </summary>
        public string TypeDev { get; set; }
        /// <summary> Ширина выработанного пространства </summary>
        public EFloat D { get; set; }
        /// <summary> Коэффициент, учитывающий размер выработанного пространства ( степень подработанности породного массива ) </summary>
        public EFloat K { get; set; }

        #endregion

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        public MineField()
        {
            TypeDev = HelpManager.UNDEFINE_DEV;
            Mv = new EFloat(3);
            H = new EFloat(300);
            K = new EFloat(1);
        }

        #endregion

        #region Public Methods

        /// <summary> Параметр, зависящий от системы разработки </summary>
        /// <returns> Значение параметра </returns>
        public abstract float CalcD();
        /// <summary> Приведенная вынимаемая мощность </summary>
        /// <returns> Значение приведенной вынимаемой мощности </returns>
        public abstract float MPr();
        /// <summary> Высота зоны водопроводящих целиков </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        public float Ht() => MPr() * CalcD();

        #endregion
    }
}
