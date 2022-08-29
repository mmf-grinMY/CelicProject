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
        private readonly ReportProgressWindow _window;

        #endregion

        #region Constructors

        /// <summary>
        /// Основной конструктор дял данного класса
        /// </summary>
        public RepProViewModel(ReportProgressWindow window, ListPlastViewModel model)
        {
            _model = model;
            (_window = window).Show();
            if(model.GetType() == typeof(CalculationBViewModel))
                new Thread(StartCalcB).Start();
            else if(model.GetType() == typeof(CalculationCViewModel))
                new Thread(StartCalcC).Start();
            else if(model.GetType() == typeof(CalculationDViewModel))
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

            new SCalculatorB((CalculationBViewModel)_model).WriteResult(this);

            _window.Dispatcher.BeginInvoke(DispatcherPriority.Normal, 
            (ThreadStart) delegate { _window.Close(); } ); 
        }
        private void StartCalcC()
        {
            ResultReport = "Начато вычисление параметров";
            StatusReport = "5";

            new SCalculatorC((CalculationCViewModel)_model).WriteResult(this);

            _window.Dispatcher.BeginInvoke(DispatcherPriority.Normal,
            (ThreadStart)delegate { _window.Close(); });
        }
        private void StartCalcD()
        {
            ResultReport = "Начато вычисление параметров";
            StatusReport = "5";

            new SCalculatorD((CalculationDViewModel)_model).WriteResult(this);

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
