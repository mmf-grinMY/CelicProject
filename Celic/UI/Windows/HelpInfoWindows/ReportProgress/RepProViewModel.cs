using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Threading;

namespace Celic
{
	class RepProViewModel
    {
        #region Private Fields

        /// <summary> Вызывающая окно модель </summary>
        private readonly ListPlastViewModel _model; 
        /// <summary> Окно, вызвавшее ViewModel </summary>
        private readonly RepProWindow _window;

        #endregion

        #region Constructors

        /// <summary> Основной конструктор дял данного класса </summary>
        public RepProViewModel(RepProWindow window, ListPlastViewModel model)
        {
            /*_model = model;
            (_window = window).Show();
            if(model.GetType() == typeof(SCalcBViewModel))
                new Thread(StartCalcB).Start();
            else if(model.GetType() == typeof(SCalcCViewModel))
                new Thread(StartCalcC).Start();
            else if(model.GetType() == typeof(SCalcDViewModel))
                new Thread(StartCalcD).Start();
            else
                _window.Close();*/
            _model = model;
            (_window = window).Show();
            new Thread(Calculate).Start();
        }

        #endregion
        
        /*

        private void StartCalcB()
        {
			
            // Creating Report about Calculation type B
            CalculatorB.WriteResult();

            WindowClose();
        }
        private void StartCalcC()
        {
			
            // Creating Report about Calculation type C
            // new CalculatorC((SCalcCViewModel)_model).WriteResult(this);

            WindowClose();
        }
        private void StartCalcD()
        {
			// Creating Report about Calculation type D
            // new CalculatorD((SCalcDViewModel)_model).WriteResult(this);

            WindowClose();
        }
        private void WindowClose()
        {
        	_window.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
            (ThreadStart)delegate { _window.Close(); });
        }
        */
        private void Calculate()
        {
        	ReportManager.WriteReport(_model);
        	_window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate { _window.Close(); });
        }
    }
}
