using System;
using System.Collections.ObjectModel;
using static System.Single;
using static Celic.HelpManager;

namespace Celic
{
    class ECalculatorB
    {
        #region Code From OldCelicVersion
        
        /*/// <summary> Коэффициент k при вычислении ЗВТ для нескольких пластов </summary>
        public void k_1()
        {
            if(arr[0] is Camera camera)
            {
                camera.D_0 = 1.4 * ((26 - 0.01 * camera.H) * camera.M_v + DH);
                camera.k_1();
            }
            else
            {
                for(int i = 0; i < arr.Count - 1; i++)
                {
                    if(arr[i] is Lava lava)
                    {
                        if (lava.B >= lava.D_0)
                        {
                            lava.k();
                        }
                        else
                        {

                            double D_1 = arr[i + 1].D; 
                            if(300 >= Math.Max(lava.D, D_1) && Math.Max(lava.D, D_1) >= lava.D_0)
                            {
                                lava.K = 1;
                            }
                            else if(Math.Max(lava.D, D_1) < lava.D_0)
                            {
                                if(Math.Max(lava.D, D_1) + lava.B >= lava.D_0)
                                {
                                    lava.K = Math.Sqrt(Math.Max(lava.D, D_1) / lava.D_0);
                                }
                                else
                                {
                                    lava.K = (lava.D + D_1) / (lava.D + 2 * lava.B + D_1);
                                }
                            }
                        }
                    }
                }
            }
        }*/

        #endregion

        #region Private Fields

        /// <summary> Список разрабатываемых пластов </summary>
        private readonly ObservableCollection<Plast> _plasts;

        #endregion

        #region Constructors

        /// <summary> Дополнительный конструктор для расчета без логирования </summary>
        /// <param name="plasts"> Коллекция разрабатываемых пластов </param>
        public ECalculatorB(ObservableCollection<Plast> plasts) => _plasts = plasts;

        #endregion

        #region Private Methods

        /// <summary> Расчет высоты ЗВТ без логирования произвденных операций </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        private float CountHt()
        {
            float InnerCountHt(float h)
            {
                float ht = _plasts[0].Ht();
                for (int i = 1; i < _plasts.Count; i++)
                    ht += h * _plasts[i].Ht() / (h + _plasts[i].H.V - _plasts[0].H.V);
                return ht;
            }

            float oldHt, newHt;
            oldHt = _plasts[0].Ht();
            newHt = InnerCountHt(oldHt);
            while (newHt - oldHt >= 2)
            {
                oldHt = newHt;
                newHt = InnerCountHt(oldHt);
            }
            newHt = (float)Math.Round(newHt * 100) / 100;
            return newHt;
        }
        /// <summary> Перерасчет коэффициентов Kt </summary>
        private void RecalcKT()
        {
            float maxT = 0;
            for (int i = 1; i < _plasts.Count; i++)
                maxT = Math.Max(maxT, _plasts[i].T);
            for (int i = 0; i < _plasts.Count; i++)
            {
                float St = (_plasts[0].T - _plasts[i].T >= 3 && _plasts[0].T - maxT >= 5) ? 1 : 0;
                float hf, ht = _plasts[i].Ht();
                hf = _plasts[i].Hf.V == 0 ? ht : 0;
                _plasts[i].Kt = new EFloat(ht != 0 ? hf / ht * (1 - 0.9F * St) : 1, 0);
                _plasts[i].Hf = new EFloat(hf, 0);
            }
        }
        /// <summary> Перерасчет коэффициентов S </summary>
        /// <param name="i"> Номер пласта, относительно которого будет произведен перерасчет </param>
        private void RecalcS(int i)
        {
            // Угол полных сдвижений в массиве пород, определяющий начало плоского дна мульды сдвижения,
            // градус (для условий Старобинского месторождения равен 55 градусов)
            const float psi = 55 / 180 * (float)Math.PI;
            // Угол, определяющий места максимальных деформаций растяжения в массиве пород относительно
            // границ остановки работ, градус (для условий Старобинского месторождения равен 80 градусов)
            const float delta_r = 80 / 180 * (float)Math.PI;
            // Угол, определяющий места максимальных деформаций сжатия в массиве пород относительно
            // границ остановки работ, градус (для условий Старобинского месторождения равен 70 градусов)
            const float delta_sg = 70 / 180 * (float)Math.PI;
            // Граничный угол мульды сдвижения в массиве пород, градус (для условий Старобинского месторождения равен 60 градусов)
            const float delta_0 = 60 / 180 * (float)Math.PI;
            // Расстояние по вертикали от границы техногенной трещиноватости от влияния отработки
            // верхнего(1 - го) пласта до кровли i-го пласта, м;
            float dh;
            // Расстояние по вертикали от кровли i-го до кровли нижерасположенного j-го пласта, м;
            float dH;
            // Расстояние в плане между границами остановки работ на i-м и j - м пластах, при котором
            // т.М_p(т.М_сж) попадает в плоское дно мульды сдвижения от влияния отработки нижележащего j - го пласта, м 
            float Lj0 = 0;
            // Расстояние в плане между границами остановки работ на i-м и j - м пластах, при котором точка М
            // попадает в зону максимальных сжатий(т.М_сж) или растяжений(т.М_р) от влияния отработки нижележащего j - го пласта, м;
            float dL = 0;
            // – Расстояние в плане между границами остановки работ на i-м и j - м пластах, м;
            float Lij;
            // Номер схемы расположения целиков (задает пользователь)
            int schema;

                void InnerCalc1(float angle1, float angle2, float angle3)
            {
                Lj0 = (dh + dH) * Ctg(angle1) + dh * Ctg(angle2);
                dL = (dh + dH) * Ctg(angle3) + dh * Ctg(angle2);
            }

            for (int j = i + 1; j < _plasts.Count; j++)
            {
                schema = GetSchema();
                Lij = Math.Abs(_plasts[i].Lp.V - _plasts[j].Lp.V);
                dH = Math.Abs(_plasts[i].H.V - _plasts[j].H.V);
                dh = _plasts[i].Ht() * _plasts[i].S.V + dH;
                switch (schema)
                {
                    case 1: InnerCalc1(psi, delta_r, delta_sg); break;
                    case 2: InnerCalc1(delta_0, delta_sg, delta_r); break;
                    case 3: InnerCalc1(delta_0, delta_r, delta_r); break;
                    case 4: InnerCalc1(psi, delta_sg, delta_sg); break;
                }
                _plasts[i].S = new EFloat(1 - (Lij - dL) / (Lj0 - dL));
            }

            void InnerCalc2(float angle1, float angle2, float angle3)
            {
                Lij = (dh + dH) * Ctg(angle1) + dh * Ctg(angle2);
                dL = (dh + dH) * Ctg(angle1) + dh * Ctg(angle3);
            }

            for (int j = i - 1; j >= 0; j--)
            {
                schema = GetSchema();
                Lij = Math.Abs(_plasts[i].Lp.V - _plasts[j].Lp.V);
                dH = Math.Abs(_plasts[i].H.V - _plasts[j].H.V);
                dh = _plasts[i].Ht() * _plasts[i].S.V + dH;
                switch (schema)
                {
                    case 1: InnerCalc2(delta_sg, delta_0, delta_r); break;
                    case 2: InnerCalc2(delta_r, psi, delta_sg); break;
                    case 3: InnerCalc2(delta_r, delta_0, delta_r); break;
                    case 4: InnerCalc2(delta_sg, psi, delta_sg); break;
                }
                _plasts[i].S = new EFloat(1 - (Lij - dL) / (Lj0 - dL));
            }
        }
        /// <summary> Запрос у пользователя схемы расположения целиков </summary>
        /// <returns> Номер схемы, выбранной пользователем </returns>
        private int GetSchema()
        {
            //Добавить всплывающее окно с выбором схемы расположения целиков
            throw new NotImplementedException();
            // return 0;
        }


        #endregion

        #region Public Methods

        /// <summary> Метод для расчета итогового значения ЗВТ </summary>
        /// <returns> Итоговое значение ЗВТ </returns>
        public double Count()
        {
            float ht = 0;
            if (_plasts != null)
            {
                if ((ht = CountHt()) <= M)
                {
                    RecalcS(0);
                    // RecalcSDzetta();
                    RecalcKT();
                    // RecalcK1();
                    ht = CountHt();
                }
            }
            return ht;
        }

        #endregion

    }
}
