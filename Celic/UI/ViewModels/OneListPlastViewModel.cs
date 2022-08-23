using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Celic
{
	/// <summary> Общая логика окна с одним списком пластов </summary>
    abstract class OneListPlastViewModel : ListPlastViewModel
    {
        #region Public Properties

        /// <summary> Коллекция разрабатываемых пластов </summary>
        public ObservableCollection<Plast> Plasts { get; set; }

        #endregion

        #region Constructors

        /// <summary> Основной консруктор для данного класса </summary>
        /// <param name="mainWindow"> Главное окно приложения </param>
        /// <param name="window"> Вызываемое окно для расчета </param>
        public OneListPlastViewModel(MainWindow mainWindow, Window window) : base(mainWindow, window)
        {
            Plasts = new ObservableCollection<Plast>();
            CalcWithDocxCommand = new RelayCommand(obj =>
            {
                Window _;
                if (Plasts.Count > 0)
                	 _ = new RepProWindow(this);
                else
                    MessageBox.Show("Мы не можем произвести расчеты для несуществующих пластов");
            }, obj => Plasts.Count > 0);
            RemoveCommand = new RelayCommand(obj =>
            {
                Plasts.Remove(SelectedPlast);
                if (Plasts.Count > 0)
                    SelectedPlast = Plasts[0];
            }, obj => Plasts.Count > 0);
            RemoveSelectionCommand = new RelayCommand(obj =>
            {
                if (_selectedPlast != null)
                    SelectedPlast = null;
            }, obj => _selectedPlast != null);
        }

        #endregion
    }
}
