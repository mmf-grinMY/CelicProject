namespace Celic
{
    class MineField
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

        public MineField()
        {
            TypeDev = HelpManager.UNDEFINE_DEV;
            Mv = new EFloat(3);
            H = new EFloat(300);
            K = new EFloat(1);
        }

        #endregion

        #region Protected Methods


        #endregion

        #region Public Methods
        #endregion
    }
}
