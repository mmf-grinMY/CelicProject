﻿using System;
using System.Collections.ObjectModel;
using static Celic.HelpManager;
using static System.Math;

namespace Celic
{
    /// <summary> Менеджер списка разрабатываемых пластов </summary>
    class CollectionPlasts
    {
        #region Private Fields

        /// <summary> Список разрабатываемых пластов </summary>
        private readonly ObservableCollection<Plast> _plasts;

        #endregion

        #region Constructors

        public CollectionPlasts(ObservableCollection<Plast> plasts) => _plasts = plasts;
        public CollectionPlasts() => _plasts = new ObservableCollection<Plast>();

        #endregion

        #region Public Properties

        public Plast this[int index] { get => _plasts[index]; } 
        public Lava this[int indexInPlasts, int indexInMineFields] { get => _plasts[indexInPlasts].TypeDev.Equals(LAVA_DEV) ? (Lava)_plasts[indexInPlasts][indexInMineFields] : null; }
        public int Count { get => _plasts.Count; }

        #endregion

        #region Public Methods

        /// <summary> Перерасчет всех дополнительных параметров пласта параметров </summary>
        public void RecalcAllParams()
        {
            RecalcS(0);
            RecalcKT();
            RecalcSDzetta();
            RecalcK();
        }

        #endregion

        #region Private Methods

        /// <summary> Перерасчет коэффициентов Kt </summary>
        private void RecalcKT()
        {
            int maxT = 0;
            for (int i = 1; i < _plasts.Count; i++)
                maxT = Max(maxT, _plasts[i].T);
            for (int i = 0; i < _plasts.Count; i++)
            {
                byte St = (_plasts[0].T - _plasts[i].T >= 3 && _plasts[0].T - maxT >= 5) ? (byte)1 : (byte)0;
                float hf, ht = _plasts[i].Ht();
                hf = _plasts[i].Hf.V == -1 ? ht : _plasts[i].Hf.V;
                _plasts[i].Kt = new EFloat(ht != 0 ? hf / ht * (1 - 0.9F * St) : 1);
                _plasts[i].Hf = new EFloat(hf);
            }
        }
        /// <summary> Коэффициент k при вычислении ЗВТ для нескольких пластов </summary>
        public void RecalcK()
        {
            Plast p1 = _plasts[0];
            for (int i = 0; i < _plasts.Count; i++)
                if (_plasts[i].TypeDev.Equals(CAMERA_DEV))
                {
                    float d0 = 1.4F * (p1.CalcD() * p1.Mv.V + (_plasts[i].H - p1.H));
                    _plasts[i].K = new EFloat(_plasts[i].D.V >= d0 ? 1 : d0 != 0 ? (float)Sqrt(_plasts[i].D.V / d0) : 1);
                }
                else
                {
                    for (int j = 0; j < _plasts[i].Count - 1; j++)
                        if (_plasts[i][j] is Lava lava)
                            if (lava.B >= lava.D)
                            {
                                lava.RecalcK();
                            }
                            else
                            {
                                float d0 = p1.CalcD() * p1.Mv.V + (_plasts[i].H - p1.H);
                                float d1 = _plasts[i][j + 1].D.V;
                                float d = lava.D.V;
                                float b = lava.B.V;
                                if (300 >= Max(d, d1) && Max(d, d1) >= d0)
                                    lava.K = new EFloat(1);
                                else if (Max(d, d1) < d0)
                                    lava.K = new EFloat(Max(d, d1) + b >= d0 ? (float)Sqrt(d0 != 0 ? Max(d, d1) / d0 : 0) :
                                        (d + 2 * b + d1) != 0 ? (d + d1) / (d + 2 * b + d1) : 0);
                            }
                }
        }
        /// <summary> Запрос у пользователя схемы расположения целиков </summary>
        /// <returns> Номер схемы, выбранной пользователем </returns>
        private int GetSchema()
        {
            int[] param = new int[1] { -1 };
            new SchemaChoiceViewModel(param);
            return param[0];
        }
        /// <summary> Перерасчет коэффициентов S </summary>
        /// <param name="i"> Номер пласта, относительно которого будет произведен перерасчет </param>
        private void RecalcS(int i)
        {
            // Угол полных сдвижений в массиве пород, определяющий начало плоского дна мульды сдвижения,
            // градус (для условий Старобинского месторождения равен 55 градусов)
            const float psi = 55 / 180 * (float)PI;
            // Угол, определяющий места максимальных деформаций растяжения в массиве пород относительно
            // границ остановки работ, градус (для условий Старобинского месторождения равен 80 градусов)
            const float delta_r = 80 / 180 * (float)PI;
            // Угол, определяющий места максимальных деформаций сжатия в массиве пород относительно
            // границ остановки работ, градус (для условий Старобинского месторождения равен 70 градусов)
            const float delta_sg = 70 / 180 * (float)PI;
            // Граничный угол мульды сдвижения в массиве пород, градус (для условий Старобинского месторождения равен 60 градусов)
            const float delta_0 = 60 / 180 * (float)PI;
            // Расстояние по вертикали от границы техногенной трещиноватости от влияния отработки
            // верхнего (1 - го) пласта до кровли i-го пласта, м;
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
                Lij = Abs(_plasts[i].Lp.V - _plasts[j].Lp.V);
                dH = Abs(_plasts[i].H.V - _plasts[j].H.V);
                dh = _plasts[i].Ht() * _plasts[i].S.V + dH;
                switch (schema)
                {
                    case 1: InnerCalc1(psi, delta_r, delta_sg); break;
                    case 2: InnerCalc1(delta_0, delta_sg, delta_r); break;
                    case 3: InnerCalc1(delta_0, delta_r, delta_r); break;
                    case 4: InnerCalc1(psi, delta_sg, delta_sg); break;
                }
                _plasts[j].S = new EFloat(1 - (Lij - dL) / (Lj0 - dL));
            }

            void InnerCalc2(float angle1, float angle2, float angle3)
            {
                Lij = (dh + dH) * Ctg(angle1) + dh * Ctg(angle2);
                dL = (dh + dH) * Ctg(angle1) + dh * Ctg(angle3);
            }

            for (int j = i - 1; j >= 0; j--)
            {
                schema = GetSchema();
                Lij = Abs(_plasts[i].Lp.V - _plasts[j].Lp.V);
                dH = Abs(_plasts[i].H.V - _plasts[j].H.V);
                dh = _plasts[i].Ht() * _plasts[i].S.V + dH;
                switch (schema)
                {
                    case 1: InnerCalc2(delta_sg, delta_0, delta_r); break;
                    case 2: InnerCalc2(delta_r, psi, delta_sg); break;
                    case 3: InnerCalc2(delta_r, delta_0, delta_r); break;
                    case 4: InnerCalc2(delta_sg, psi, delta_sg); break;
                }
                _plasts[j].S = new EFloat(1 - (Lij - dL) / (Lj0 - dL));
            }
        }

        #endregion

        #region Private UnReady Methods

        /// <summary> Перерасчет коэффициентов Sz </summary>
        private void RecalcSDzetta()
        {
            // Запрос у пользователя ввести более корректные значения коэффицентов
            // Либо просто приравнять все к 1 ( некоторые могут быть меньше 1 )
            throw new NotImplementedException("Метод RecalcSDzetta не реализован");
        }

        #endregion
    }
}
