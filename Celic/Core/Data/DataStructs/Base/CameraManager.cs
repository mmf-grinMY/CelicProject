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
	/// <summary> Класс, обслуживающий стуктуру данных Camera </summary>
    static class CameraManager
    {
        /// <summary> Расчет высоты зоны водопроводящих целиков </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        public static float Ht(Camera camera) 
        {
        	return MPr(camera) * CalcD(camera);
        }
        /// <summary> Расчет значения коэффицента K при камерной системе разработки </summary>
        /// <returns> Значение коэффицента K при камерной системе разработки </returns>
        public static float RecalcK(Camera camera) {
            float d0 = (26 - 0.01F * camera.H) * camera.Mv * camera.Ki;
            return camera.K = camera.D >= 1.4F * d0 ? 1 : (float)Math.Sqrt(0.8 * (camera.D / (d0 - 0.2)));
        }
        /// <summary> Расчет значения коэффициента Ki </summary>
        /// <returns> Значение коэффициента Ki </returns>
        public static float CalcKi(Camera camera) 
        {
        	return camera.Ki = camera.L != 0 && camera.Mv != 0 ? camera.Si / (camera.L * camera.Mv) : 0;
        }
        /// <summary> Расчет параметра, зависящего от системы разработки при камерной системы разработки </summary>
        /// <returns> Значение параметра, зависящего от системы разработки </returns>
        public static float CalcD(Camera camera) 
        {
        	return 26 - 0.01F * camera.H;
        }
        /// <summary> Расчет приведенной вынимаемой мощности при камерной системе разработки </summary>
        /// <returns> Значение приведенной вынимаемой мощности </returns>
        public static float MPr(Camera camera) 
        {
        	return camera.Mv * camera.Ki * camera.K;
        }
    }
}
