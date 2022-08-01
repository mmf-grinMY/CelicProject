namespace Celic
{
    /// <summary> Шахтное поле со столбовой системой разработки </summary>
    class Lava : MineField
    {
        #region Contructors

        /// <summary> Основной конструктор для данного класса </summary>
        public Lava()
        {
            TypeDev = HelpManager.LAVA_DEV;
        }

        #endregion

        #region Public Properties

        /// <summary> Расстояние между штреками </summary>
        public EFloat B { set; get; }
        /// <summary> Площадь поперечного сечения лавы </summary>
        public EFloat Sl { set; get; }
        /// <summary> Длина лавы </summary>
        public EFloat L { set; get; }

        #endregion

        #region Public Methods

        public float Ht() => MPr() * CalcD();
        public float MPr()
        {
            return 0;
        }
        public float CalcD()
        {
            return 0;
        }

        #endregion
    }
}
