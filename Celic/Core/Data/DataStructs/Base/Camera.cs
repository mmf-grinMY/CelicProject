/*
 * Created by SharpDevelop.
 * User: Максим
 * Date: 08/20/2022
 * Time: 20:44
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Celic
{
	/// <summary> Шахтное поле с камерной системой разработки </summary>
    public class Camera : MineField
    {
        #region Private Fields

        /// <summary> Сумма поперечных сечений выработок, составляющих очистную камеру ( поле ) </summary>
        private float _si;
        /// <summary> Расстояние между соседними осями междукамерных целиков ( поле ) </summary>
        private float _l;
        /// <summary> Коэффициент извлечения рудной массы в пределах вынимаемой мощности ( поле ) </summary>
        private float _ki;

        #endregion

        #region Public Properties

        /// <summary> Сумма поперечных сечений выработок, составляющих очистную камеру </summary>
        public float Si
        {
            get 
            {
            	return _si;
            }
            set
            {
                _si = value;
                OnPropertyChanged("Si");
            }
        }
        /// <summary> Расстояние между соседними осями междукамерных целиков </summary>
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
        /// <summary> Коэффициент извлечения рудной массы в пределах вынимаемой мощности </summary>
        public float Ki
        {
            get 
            {
            	return _ki;
            }
            set
            {
                _ki = value;
                OnPropertyChanged("Ki");
            }
        }

        #endregion

        #region Contructros

        /// <summary> Основной конструктор дял данного класса </summary>
        public Camera() : base()
        {
        	Si = Ki = L = 0;
        }

        #endregion
        
		public override string Type
		{
			get
			{
				return "Camera";
			}
		}
    }
}
