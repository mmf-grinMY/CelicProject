using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Windows.Threading;

namespace Celic
{
    class RepProViewModel : BaseViewModel
    {
        #region Private Fields

        /// <summary>
        /// Сведения о процессе генерации отчета
        /// </summary>
        private string _resultReport;
        /// <summary>
        /// Процентое выполнение генерации отчтета
        /// </summary>
        private string _statusReport;
        /// <summary>
        /// Вызывающая окно модель
        /// </summary>
        private readonly ListPlastViewModel _model; 
        /// <summary>
        /// Окно, вызвавшее ViewModel
        /// </summary>
        private readonly RepProWindow _window;

        #endregion

        #region Constructors

        /// <summary>
        /// Основной конструктор дял данного класса
        /// </summary>
        public RepProViewModel(RepProWindow window, ListPlastViewModel model)
        {
            _model = model;
            (_window = window).Show();
            if(model.GetType() == typeof(SCalcBViewModel))
                new Thread(StartCalcB).Start();
            else if(model.GetType() == typeof(SCalcCViewModel))
                new Thread(StartCalcC).Start();
            else if(model.GetType() == typeof(SCalcDViewModel))
                new Thread(StartCalcD).Start();
            else
                _window.Close();
        }

        #endregion
        
        #region Private Methods

        private void StartCalcB()
        {
            ResultReport = "Начато вычисление параметров";
            StatusReport = "5";

            new SCalculatorB((SCalcBViewModel)_model).WriteResult(this);

            _window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, 
            (ThreadStart) delegate { _window.Close(); } ); 
        }
        private void StartCalcC()
        {
            ResultReport = "Начато вычисление параметров";
            StatusReport = "5";

            new SCalculatorC((SCalcCViewModel)_model).WriteResult(this);

            _window.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
            (ThreadStart)delegate { _window.Close(); });
        }
        private void StartCalcD()
        {
            ResultReport = "Начато вычисление параметров";
            StatusReport = "5";

            new SCalculatorD((SCalcDViewModel)_model).WriteResult(this);

            _window.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
            (ThreadStart)delegate { _window.Close(); });
        }

        #endregion

        #region Public Properties

        /// <summary> Название этапа проведения операции </summary>
        public string ResultReport
        {
            get => _resultReport;
            set
            {
                _resultReport = value;
                OnPropertyChanged(nameof(ResultReport));
            }
        }
        /// <summary> Этап проведения операции </summary>
        public string StatusReport
        {
            get => _statusReport;
            set
            {
                _statusReport = value;
                OnPropertyChanged(nameof(StatusReport));
            }
        }

        #endregion
    }
}
