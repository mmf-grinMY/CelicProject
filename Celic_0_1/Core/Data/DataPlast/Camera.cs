using System;

namespace Celic
{
    /// <summary> Шахтное поле с камерной системой разработки </summary>
    class Camera : MineField
    {
        #region Contructros

        /// <summary> Основной конструктор дял данного класса </summary>
        public Camera() : base()
        {
            TypeDev = HelpManager.CAMERA_DEV;
            Si = Ki = L = new EFloat(-1);
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

        #region Public Methods

        /// <summary> Расчет значения коэффицента K при камерной системе разработки </summary>
        /// <returns> Значение коэффициента </returns>
        public float CalcK()
        {
            float d0 = (26 - 0.01F * H.V) * Mv.V * Ki.V;
            return D.V >= 1.4F * d0 ? 1 : (float)Math.Sqrt(0.8 * (D.V / (d0 - 0.2)));
        }
        /// <summary> Расчет значения коэффициента Ki </summary>
        /// <returns> Значение коэффициента </returns>
        public EFloat CalcKi() => Ki = new EFloat(L.V != 0 && Mv.V != 0 ? Si.V / (L * Mv) : 0);
        public override float CalcD() => 26 - 0.01F * H.V;
        public override float MPr() => Mv * Ki * K.V;

        #endregion
    }
}