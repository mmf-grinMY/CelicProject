/*
 * Created by SharpDevelop.
 * User: Максим
 * Date: 08/20/2022
 * Time: 20:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Celic
{
	/// <summary> Класс, обслуживающий структуру данных Lava </summary>
    static class LavaManager
    {
        /// <summary> Расчет высоты зоны водопроводящих целиков </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        public static float Ht(Lava lava) 
        {
        	return MPr(lava) * CalcD(lava);
        }
        /// <summary> Расчет приведенной вынимаемой мощности при столбовой системе разработки </summary>
        /// <returns> Значение приведенной вынимаемой мощности </returns>
        public static float MPr(Lava lava) 
        {
        	return lava.L != 0 ? lava.Sl / lava.L * lava.K : 0;
        }
        /// <summary> Расчет параметра, зависящего от системы разработки </summary>
        /// <returns> Значение параметра, зависящего от системы разработки </returns>
        public static float CalcD(Lava lava) 
        {
        	return 46 - 0.01F * lava.H;
        }
        /// <summary> Расчет значения коэффицента K при столбовой системе разработки для одиночной лавы </summary>
        /// <returns> Значение коэффицента K при столбовой системе разработки </returns>
        public static float RecalcK(Lava lava) {
            float d0 = 1.4F * (46 - 0.01F * lava.H) * lava.Mv;
            return lava.K = lava.D >= d0 ? 1 : (d0 != 0 ? (float)Math.Sqrt(lava.D / d0) : 0);
        }
        /// <summary> Расчет значения коэффицента K при столбовой системе разработки для неодиночной лавы </summary>
        /// <param name="other"> Соседняя лава </param>
        /// <returns> Значение коэффицента K при столбовой системе разработки </returns>
        public static float ERecalcK(Lava main, Lava other) {
            float d0 = 1.4F * CalcD(main) * main.Mv;
            float d1 = main.D;
            float d = other.D;
            float b = main.B;
            if (300 >= Math.Max(d, d1))
                return main.K = Math.Max(d, d1) >= d0 ? 1 : Math.Max(d, d1) + b >= d0 ? (float)Math.Sqrt(d0 != 0 ? Math.Max(d, d1) / d0 : 0) : (d + 2 * b + d1) != 0 ? (d + d1) / (d + 2 * b + d1) : 0;
            return 0;
        }
    }
}
