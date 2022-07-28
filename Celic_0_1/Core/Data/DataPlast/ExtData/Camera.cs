namespace Celic
{
    /// <summary> Шахтное поле с камерной системой разработки </summary>
    public class Camera
    {
        #region Contructros

        /// <summary> Основной конструктор дял данного класса </summary>
        public Camera() => Si = L = "";

        #endregion

        #region Public Properties

        /// <summary> Сумма поперечных сечений выработок, составляющих очистную камеру </summary>
        public string Si { get; set; }
        /// <summary> Расстояние между соседними осями междукамерных целиков </summary>
        public string L { get; set; }

        #endregion
    }
}
