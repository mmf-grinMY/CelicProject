namespace Celic
{
    /// <summary> Шахтное поле с камерной системой разработки </summary>
    class Camera : MineField
    {
        #region Contructros

        /// <summary> Основной конструктор дял данного класса </summary>
        public Camera()
        {
            TypeDev = HelpManager.CAMERA_DEV;
        }
        #endregion

        #region Public Properties

        /// <summary> Сумма поперечных сечений выработок, составляющих очистную камеру </summary>
        public EFloat Si { get; set; }
        /// <summary> Расстояние между соседними осями междукамерных целиков </summary>
        public EFloat L { get; set; }
        /// <summary> Коэффициент извлечения рудной массы в пределах вынимаемой мощности </summary>
        public EFloat Ki { get; set; }

        #endregion
    }
}