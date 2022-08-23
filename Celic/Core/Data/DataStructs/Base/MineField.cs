/*
 * Created by SharpDevelop.
 * User: Максим
 * Date: 08/20/2022
 * Time: 19:11
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;

namespace Celic
{
	public class MineField : BaseViewModel
    {
        #region Private Fields

        /// <summary> Вынимаемая мощность пласта(высота камер) ( поле ) </summary>
        private float _mv;
        /// <summary> Глубина ведения горных работ в пределах от 350 до 1000 м ( поле ) </summary>
        private float _h;
        /// <summary> Ширина выработанного пространства ( поле ) </summary>
        private float _d;
        /// <summary> Коэффициент, учитывающий размер выработанного пространства ( степень подработанности породного массива ) ( поле ) </summary>
        private float _k; 

        #endregion

        #region Public Properties

        /// <summary> Вынимаемая мощность пласта(высота камер), м </summary>
        public float Mv
        {
            get 
            {
            	return _mv;
            }
            set
            {
                _mv = value;
                OnPropertyChanged("Mv");
            }
        }
        /// <summary> Глубина ведения горных работ в пределах от 350 до 1000 м </summary>
        public float H
        {
            get 
            {
            	return _h;
            }
            set
            {
                _h = value;
                OnPropertyChanged("H");
            }
        }
        /// <summary> Ширина выработанного пространства </summary>
        public float D
        {
            get 
            {
            	return _d;
            }
            set
            {
                _d = value;
                OnPropertyChanged("D");
            }
        }
        /// <summary> Коэффициент, учитывающий размер выработанного пространства ( степень подработанности породного массива ) </summary>
        public float K
        {
            get 
            {
            	return _k;
            }
            set
            {
                _k = value;
                OnPropertyChanged("K");
            }
        }

        #endregion

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        public MineField() 
        {
        	Mv = H = K = 0;
        }

        #endregion
        
        public virtual string Type { get {return "Undefine"; }}
    }
}
