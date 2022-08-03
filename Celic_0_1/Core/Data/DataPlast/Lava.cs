using System;

namespace Celic
{
    /// <summary> Шахтное поле со столбовой системой разработки </summary>
    class Lava : MineField
    {
        #region Contructors

        /// <summary> Основной конструктор для данного класса </summary>
        public Lava() : base()
        {
            TypeDev = HelpManager.LAVA_DEV;
            B = Sl = L = new EFloat(-1);
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

        public override float MPr() => L.V != 0 ? Sl / L * K.V : 0;
        public override float CalcD() => 46 - 0.01F * H.V;
        /// <summary> Расчет значения коэффицента K при столбовой системе разработки</summary>
        public void RecalcK()
        {
            float d0 = 1.4F * (46 - 0.01F * H.V) * Mv.V;
            K = new EFloat(D.V >= d0 ? 1 : (d0 != 0 ? (float)Math.Sqrt(D.V / d0) : 0));
        }

        #endregion
    }
}
