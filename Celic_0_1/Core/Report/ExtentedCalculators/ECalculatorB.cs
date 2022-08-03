﻿using System;
using System.Collections.ObjectModel;
using static Celic.HelpManager;

namespace Celic
{
    class ECalculatorB
    {
        #region Private Fields

        /// <summary> Список разрабатываемых пластов </summary>
        private readonly CollectionPlasts _plasts;

        #endregion

        #region Constructors

        /// <summary> Дополнительный конструктор для расчета без логирования </summary>
        /// <param name="plasts"> Коллекция разрабатываемых пластов </param>
        public ECalculatorB(ObservableCollection<Plast> plasts) => _plasts = new CollectionPlasts(plasts);

        #endregion

        #region Private Methods

        /// <summary> Расчет высоты ЗВТ без логирования произвденных операций </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        private float CountHt()
        {
            float InnerCountHt(float h)
            {
                float ht = _plasts[0].Ht() * _plasts[0].S.V;
                for (int i = 1; i < _plasts.Count; i++)
                    ht += h * _plasts[i].Ht() * _plasts[i].S.V * _plasts[i].Sz.V * _plasts[i].Kt.V / (h + _plasts[i].H.V - _plasts[0].H.V);
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

        #endregion

        #region Public Methods

        /// <summary> Метод для расчета итогового значения ЗВТ </summary>
        /// <returns> Итоговое значение ЗВТ </returns>
        public double Count()
        {
            float ht = 0;
            if (_plasts != null)
                if ((ht = CountHt()) <= M)
                {
                    _plasts.RecalcAllParams();
                    ht = CountHt();
                }
            return ht;
        }

        #endregion
    }
}
