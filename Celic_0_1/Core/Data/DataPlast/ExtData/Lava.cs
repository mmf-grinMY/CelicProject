namespace Celic
{
    /// <summary> Шахтное поле со столбовой системой разработки </summary>
    public class Lava
    {
        #region Contructors

        /// <summary> Основной конструктор для данного класса </summary>
        public Lava() => B = Sl = L;

        #endregion

        #region Public Properties

        /// <summary> Расстояние между штреками </summary>
        public string B { set; get; }
        /// <summary> Площадь поперечного сечения лавы </summary>
        public string Sl { set; get; }
        /// <summary> Длина лавы </summary>
        public string L { set; get; }

        #endregion
    }
}
