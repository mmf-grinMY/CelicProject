using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Celic
{
    /// <summary>
    /// Общая логика окна со спиком ( списками ) пластов
    /// </summary>
    public abstract class ListPlastViewModel : BaseViewModel, ICalcViewModel
    {
        #region Protected Fields

        /// <summary> Главное окно приложения </summary>
        protected MainWindow _mainWindow = null;
        /// <summary> Окно проведения расчета </summary>
        protected Window _calcWindow = null;
        /// <summary> Выбранный пласт </summary>
        protected Plast _selectedPlast = null;

        #endregion

        #region Private Methods

        /// <summary> Инициализация параметров окна выбора файлов </summary>
        /// <param name="open">Открываемое окно взаимодействия с пользователем</param>
        private FileDialog InitializeFileDialog(FileDialog open)
        {
            open.InitialDirectory = @"D:\";
            open.RestoreDirectory = true;
            open.Title = "Выбор файла";
            open.DefaultExt = "plast";
            open.Filter = "plast files (*.plast)|*.plast";
            open.FilterIndex = 1;
            open.ShowDialog();
            return open;
        }

        #endregion

        #region Public Properties

        /// <summary> Выбранный пласт </summary>
        public Plast SelectedPlast
        {
            get => _selectedPlast;
            set
            {
                if (_calcWindow.GetType() == typeof(SCalcBWindow)) ;
                // (_calcWindow as SCalcBWindow).selected.IsEnabled = (_selectedPlast = value) != null;
                else if (_calcWindow.GetType() == typeof(SCalcCWindow))
                    (_calcWindow as SCalcCWindow).selected.IsEnabled = (_selectedPlast = value) != null;
                else if (_calcWindow.GetType() == typeof(SCalcDWindow))
                    (_calcWindow as SCalcDWindow).selected.IsEnabled = (_selectedPlast = value) != null;
                OnPropertyChanged(nameof(SelectedPlast));
            }
        }

        #endregion

        #region Public Methods

        /// <summary> Корректное закрытие окна для проведения расчета </summary>
        /// <param name="sender">Вызываемый объект</param>
        /// <param name="e">Аргументы закрытия окна</param>
        public void Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            _calcWindow = null;
            GC.Collect();
            GC.WaitForFullGCComplete();
            _mainWindow.Show();
        }

        #endregion

        #region Constructors

        /// <summary> Основной конструктор для класса </summary>
        public ListPlastViewModel(MainWindow mainWindow, Window window)
        {
            _mainWindow = mainWindow;
            _calcWindow = window;
            SelectedPlast = null;
            AddCommand = new RelayCommand(obj =>
            {
                if (obj is ObservableCollection<Plast> plasts)
                    new AddPlastWindow(plasts).ShowDialog();
                else
                    MessageBox.Show("Мы не можем добавить новые пласты к существующим!");
            });
            OpenMainWindowCommand = new RelayCommand(obj => _calcWindow.Close());
            OpenReadCommand = new RelayCommand(obj => new PlastReader(InitializeFileDialog(new OpenFileDialog()).FileName, this).Read());
            SimpleSaveCommand = new RelayCommand(obj => new PlastWriter(this).Write());
            SaveWithPathCommand = new RelayCommand(obj => new PlastWriter(InitializeFileDialog(new SaveFileDialog()).FileName, this).Write());
        }

        #endregion

        #region ListBoxCommnads

        /// <summary> Команда удаления пласта из системы разработки </summary>
        public RelayCommand RemoveCommand { protected set; get; }
        /// <summary> Команда вызова окна добавления пластов в систему разработки </summary>
        public RelayCommand AddCommand { private set; get; }
        /// <summary> Удаление выделения элемента </summary>
        public RelayCommand RemoveSelectionCommand { protected set; get; }

        #endregion

        #region MenuCommands

        /// <summary> Команда открытия главного окна </summary>
        public RelayCommand OpenMainWindowCommand { private set; get; }
        /// <summary> Команда расчета высоты ЗВТ с генерацией отчета </summary>
        public RelayCommand CalcWithDocxCommand { protected set; get; }
        /// <summary> Команда расчета ЗВТ без записи в файл </summary>
        public RelayCommand CalcWithoutDocxCommand { protected set; get; }
        /// <summary> Команда считывания данных из выбранного пользователем файла </summary>
        public RelayCommand OpenReadCommand { private set; get; }
        /// <summary> Команда сохранения данных в файле с заранее определенным программой расположением ( по умолчанию "D:\" ) </summary>
        public RelayCommand SimpleSaveCommand { private set; get; }
        /// <summary> Команда сохранения файла с выбором пути расположения </summary>
        public RelayCommand SaveWithPathCommand { private set; get; }

        #endregion
    }
}
