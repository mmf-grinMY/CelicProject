/*
 * Created by SharpDevelop.
 * User: Максим
 * Date: 08/20/2022
 * Time: 20:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

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
            get 
            {
            	return _b;
            }
            set
            {
                _b = value;
                OnPropertyChanged("B");
            }
        }
        /// <summary> Площадь поперечного сечения лавы </summary>
        public float Sl
        {
            get 
            {
            	return _sl;
            }
            set
            {
                _sl = value;
                OnPropertyChanged("Sl");
            }
        }
        /// <summary> Длина лавы </summary>
        public float L
        {
            get 
            {
            	return _l;
            }
            set
            {
                _l = value;
                OnPropertyChanged("L");
            }
        }

        #endregion

        #region Contructors

        /// <summary> Основной конструктор для данного класса </summary>
        public Lava() : base() 
        {
        	B = Sl = L = 0;
        }

        #endregion
        
		public override string Type
		{
			get 
			{
				return "Lava";
			}
		}
    }
}
