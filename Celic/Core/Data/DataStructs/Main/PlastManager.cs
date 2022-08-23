using System;

namespace Celic
{    
    /// <summary> Класс, обслуживающий стуктуру данных Plast </summary>
    public static class PlastManager
    {
        /// <summary> Расчет высоты ЗВТ одиночного пласта </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        public static float Ht(Plast plast) 
        {
        	return MPr(plast) * CalcD(plast);
        }
        /// <summary> Расчет параметра, зависящего от системы разработки для пласта </summary>
        /// <returns> Значение параметра, зависящего от системы разработки </returns>
        public static float CalcD(Plast plast) 
        {
        	return plast.TypeDev.Equals(HelpManager.CAMERA_DEV) ? CameraManager.CalcD(plast.Main as Camera) : LavaManager.CalcD(plast.Main as Lava);
        }
        /// <summary> Расчет приведенной вынимаемой мощности для пласта </summary>
        /// <returns> Значение приведенной вынимаеомй мощности </returns>
        public static float MPr(Plast plast) 
        {
        	return plast.TypeDev.Equals(HelpManager.CAMERA_DEV) ? CameraManager.MPr(plast.Main as Camera) : LavaManager.MPr(plast.Main as Lava);
        }
        /// <summary> Перерасчет коэффициентов К для коллекции шахтных полей </summary>
        public static float RecalcK(Plast plast)
        {
            float maxK = 0;
            if (plast.TypeDev.Equals(HelpManager.CAMERA_DEV))
            {
                maxK = CameraManager.RecalcK(plast.Main as Camera);
            }
            else
            {
                for (int i = 0; i < plast.MineFields.Count - 1; i++)
                {
            		Lava lava1 = plast.MineFields[i] as Lava;
            		Lava lava2 = plast.MineFields[i + 1] as Lava;
                    maxK = Math.Max(maxK, lava1.B >= 0.58F * (LavaManager.Ht(lava1) + LavaManager.Ht(lava2)) ? LavaManager.RecalcK(lava1) : LavaManager.ERecalcK(lava1, lava2));
                }
                        
            }
            return plast.Main.K = maxK;
        }
        /// <summary> Проверка двух пластов на сближенность по условиям водопроводимости </summary>
        /// <param name="plast1"> Первый пласт </param>
        /// <param name="plast2"> Второй пласт </param>
        /// <returns> true, если пласты являются сближенными; false в протичном случае </returns>
        public static bool IsContiguous(Plast plast1, Plast plast2)
        {
            if(plast1.Main.H > plast2.Main.H)
                return plast1.Buttom.Equals(plast2.Name) && plast2.Top.Equals(plast1.Name);
            else
                return plast2.Buttom.Equals(plast1.Name) && plast1.Top.Equals(plast2.Name);
        }
        /// <summary> Выбор коэффициента параметра d при генерации отчета </summary>
        /// <param name="plast"> Рассматриваемый пласт </param>
        /// <returns> Значение коэффициента в виде строки </returns>
        public static string GetD(Plast plast) 
        {
        	switch(MineDevManager.ToMineDev(plast.TypeDev))
        	{
        		case MineDev.camera: return "26";
        		case MineDev.lava: return "46";
        		default: return "";
        	}
        }
    }
}
