using System;
using System.Collections.ObjectModel;
using Help = Celic.HelpManager;

namespace Celic
{
	/// <summary> Менеджер списка разрабатываемых пластов </summary>
    static class PlastCollectionManager
    {
        #region Public Static Methods

        /// <summary> Перерасчет всех дополнительных параметров пласта параметров </summary>
        public static void RecalcAllParams(ObservableCollection<Plast> plasts)
        {
            RecalcS(0, plasts);
            RecalcKT(plasts);
            RecalcSDzetta(plasts);
            RecalcK(plasts);
        }
        /// <summary> Проверка коллекции пластов на наличие сближенных по условиям водопроводимости </summary>
        /// <param name="plasts"> Коллекция рассматриваемых пластов </param>
        /// <returns> true, если есть сближенные; false, если нет ни одной пары сближенных пластов </returns>
        public static bool IsContiguous(ObservableCollection<Plast> plasts)
        {
            foreach (Plast plast1 in plasts)
                foreach (Plast plast2 in plasts)
                    if (plast1 != plast2 && PlastManager.IsContiguous(plast1, plast2))
                        return true;
            return false;
        }
        /// <summary> Сортировка коллекции пластов по возрастанию величины параметра H </summary>
        /// <param name="plasts"> Коллекция разрабатываемых пластов</param>
        /// <returns> Отсортированная коллекция пластов </returns>
        public static ObservableCollection<Plast> Sort(ObservableCollection<Plast> plasts)
        {
            for (int i = 1; i < plasts.Count; i++)
                for (int j = i; j > 0 && plasts[j - 1].Main.H > plasts[j].Main.H; j--)
                    plasts.Move(j - 1, j);
            return plasts;
        }

        #endregion

        #region Private Static Methods

        /// <summary> Перерасчет коэффициентов Kt </summary>
        private static void RecalcKT(ObservableCollection<Plast> plasts)
        {
            int maxT = 0;
            for (int i = 1; i < plasts.Count; i++)
                maxT = Math.Max(maxT, plasts[i].T);
            for (int i = 0; i < plasts.Count; i++)
            {
                byte St = (plasts[0].T - plasts[i].T >= 3 && plasts[0].T - maxT >= 5) ? (byte)1 : (byte)0;
                float hf, ht = PlastManager.Ht(plasts[i]);
                hf = plasts[i].Hf == -1 ? ht : plasts[i].Hf;
                plasts[i].Kt = ht != 0 ? hf / ht * (1 - 0.9F * St) : 1;
                plasts[i].Hf = hf;
            }
        }
        /// <summary> Коэффициент k при вычислении ЗВТ для нескольких пластов </summary>
        public static void RecalcK(ObservableCollection<Plast> plasts) 
        {
            MineField field1 = plasts[0].Main;
            for (int i = 0; i < plasts.Count; i++)
                if (plasts[i].TypeDev.Equals(HelpManager.CAMERA_DEV)) {
                    Camera camera = plasts[i].Main as Camera;
                    float d0 = 1.4F * (CameraManager.CalcD(field1 as Camera) * field1.Mv + (camera.H - field1.H));
                    camera.K = camera.D >= d0 ? 1 : d0 != 0 ? (float)Math.Sqrt(camera.D / d0) : 1;
                } else {
                    for (int j = 0; j < plasts[i].MineFields.Count - 1; j++)
                    	if (plasts[i].Main is Lava)
	                    {
                    		Lava lava = plasts[i].Main as Lava;
                            if (lava.B >= lava.D) {
                                LavaManager.RecalcK(lava);
                            } else {
                                float d0 = LavaManager.CalcD(field1 as Lava) * field1.Mv + (lava.H - field1.H);
                                float d1 = plasts[i].MineFields[j + 1].D;
                                float d = lava.D;
                                float b = lava.B;
                                if (300 >= Math.Max(d, d1))
                                    lava.K = Math.Max(d, d1) >= d0 ? 1 : Math.Max(d, d1) + b >= d0 ? (float)Math.Sqrt(d0 != 0 ? Math.Max(d, d1) / d0 : 0) : (d + 2 * b + d1) != 0 ? (d + d1) / (d + 2 * b + d1) : 0;
                            }
	                    }
                }
        }
        
		private static int GetSchema()
		{
			int[] param = new int[1] { -1 };
			new SchemaChoiceViewModel(param);
			return param[0];
		}
		private static void CalcDH(out float dh, out float dH, Plast p_i, Plast p_j)
		{
			// Расстояние по вертикали от кровли i-го до кровли нижерасположенного j-го пласта, м;
			dH = Math.Abs(p_i.Main.H - p_j.Main.H);
			// Расстояние по вертикали от границы техногенной трещиноватости от влияния отработки верхнего (1 - го) пласта до кровли i-го пласта, м;
        	dh = PlastManager.Ht(p_i) * p_i.S + dH;
		}
        private static void InnerCalc1(float angle1, float angle2, float angle3, ref float Lj0, ref float dL, Plast p_i, Plast p_j)
        {
        	float dh, dH;
        	CalcDH(out dh, out dH, p_i, p_j);
            Lj0 = (dh + dH) * Help.Ctg(angle1) + dh * Help.Ctg(angle2);
            dL = (dh + dH) * Help.Ctg(angle3) + dh * Help.Ctg(angle2);
        }
        private static void InnerCalc2(float angle1, float angle2, float angle3, ref float Lj0, ref float dL, Plast p_i, Plast p_j)
        {
        	float dh, dH;
        	CalcDH(out dh, out dH, p_i, p_j);
            Lj0 = (dh + dH) * Help.Ctg(angle1) + dh * Help.Ctg(angle2);
            dL = (dh + dH) * Help.Ctg(angle1) + dh * Help.Ctg(angle3);
        }
        /// <summary> Перерасчет коэффициентов S </summary>
        /// <param name="i"> Номер пласта, относительно которого будет произведен перерасчет </param>
        private static void RecalcS(int i, ObservableCollection<Plast> plasts) 
        {
            // Угол полных сдвижений в массиве пород, определяющий начало плоского дна мульды сдвижения, градус (для условий Старобинского месторождения равен 55 градусов)
            const float psi = 55 / 180 * (float)Math.PI;
            // Угол, определяющий места максимальных деформаций растяжения в массиве пород относительно границ остановки работ, градус (для условий Старобинского месторождения равен 80 градусов)
            const float delta_r = 80 / 180 * (float)Math.PI;
            // Угол, определяющий места максимальных деформаций сжатия в массиве пород относительно границ остановки работ, градус (для условий Старобинского месторождения равен 70 градусов)
            const float delta_sg = 70 / 180 * (float)Math.PI;
            // Граничный угол мульды сдвижения в массиве пород, градус (для условий Старобинского месторождения равен 60 градусов)
            const float delta_0 = 60 / 180 * (float)Math.PI;
            // Расстояние в плане между границами остановки работ на i-м и j - м пластах, при котором т.М_p(т.М_сж) попадает в плоское дно мульды сдвижения от влияния отработки нижележащего j - го пласта, м 
            float Lj0 = 0;
            // Расстояние в плане между границами остановки работ на i-м и j - м пластах, при котором точка М попадает в зону максимальных сжатий(т.М_сж) или растяжений(т.М_р) от влияния отработки нижележащего j - го пласта, м;
            float dL = 0;
            // Расстояние в плане между границами остановки работ на i-м и j - м пластах, м;
            // float Lij;
            // Номер схемы расположения целиков (задает пользователь)
            int schema;
            for (int j = i + 1; j < plasts.Count; j++) {
                schema = GetSchema();
                switch (schema) {
                	case 1: InnerCalc1(psi, delta_r, delta_sg, ref Lj0, ref dL, plasts[i], plasts[j]); break;
                    case 2: InnerCalc1(delta_0, delta_sg, delta_r, ref Lj0, ref dL, plasts[i], plasts[j]); break;
                    case 3: InnerCalc1(delta_0, delta_r, delta_r, ref Lj0, ref dL, plasts[i], plasts[j]); break;
                    case 4: InnerCalc1(psi, delta_sg, delta_sg, ref Lj0, ref dL, plasts[i], plasts[j]); break;
                }
                plasts[j].S = 1 - (Math.Abs(plasts[i].Lp - plasts[j].Lp) - dL) / (Lj0 - dL);
            }
            for (int j = i - 1; j >= 0; j--) {
                schema = GetSchema();
                switch (schema) {
                    case 1: InnerCalc2(delta_sg, delta_0, delta_r, ref Lj0, ref dL, plasts[i], plasts[j]); break;
                    case 2: InnerCalc2(delta_r, psi, delta_sg, ref Lj0, ref dL, plasts[i], plasts[j]); break;
                    case 3: InnerCalc2(delta_r, delta_0, delta_r, ref Lj0, ref dL, plasts[i], plasts[j]); break;
                    case 4: InnerCalc2(delta_sg, psi, delta_sg, ref Lj0, ref dL, plasts[i], plasts[j]); break;
                }
                plasts[j].S = 1 - (Math.Abs(plasts[i].Lp - plasts[j].Lp) - dL) / (Lj0 - dL);
            }
        }

        #endregion

        #region Private UnReady Methods

        /// <summary> Перерасчет коэффициентов Sz </summary>
        private static void RecalcSDzetta(ObservableCollection<Plast> plasts)
        {
            // Запрос у пользователя ввести более корректные значения коэффицентов
            // Либо просто приравнять все к 1 ( некоторые могут быть меньше 1 )
            throw new NotImplementedException("Метод RecalcSDzetta не реализован");
        }

        #endregion
    }
}
