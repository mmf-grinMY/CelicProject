using System;
using System.Collections.ObjectModel;
using static Celic.HelpManager;

namespace Celic
{
    class AddPlastViewModel : BaseViewModel
    {
        #region Private Fields

        /// <summary>
        /// Окно, с которым в данный момент работает ViewModel
        /// </summary>
        private AddPlastWindow _plastWindow;
        /// <summary>
        /// Основной вводимый пласт
        /// </summary>
        private Plast _mainPlast;
        /// <summary>
        /// Сбилженный пласт № 1
        /// </summary>
        private Plast _contiguosPlast1;
        /// <summary>
        /// Сближеный пласт № 2
        /// </summary>
        private Plast _contiguosPlast2;
        /// <summary>
        /// Коллекция разрабатываемых пластов
        /// </summary>
        private readonly ObservableCollection<Plast> _plasts;
        /// <summary>
        /// Флаг выбора добавления сближенного пласта № 1
        /// </summary>
        private bool isAddContCom1Clicked = false;
        /// <summary>
        /// Флаг выбора добавления сближенного пласта № 2
        /// </summary>
        private bool isAddContCom2Clicked = false;

        #endregion

        #region Public Properties

        /// <summary>
        /// Свойство для _mainPlast
        /// </summary>
        public Plast MainPlast
        {
            get => _mainPlast;
            set
            {
                _mainPlast = value;
                OnPropertyChanged(nameof(MainPlast));
            }
        }
        /// <summary>
        /// Свойство для _contiguosPlast1
        /// </summary>
        public Plast ContiguosPlast1
        {
            get => _contiguosPlast1;
            set
            {
                _contiguosPlast1 = value;
                OnPropertyChanged(nameof(ContiguosPlast1));
            }
        }
        /// <summary>
        /// Свойство для _contiguosPlast2
        /// </summary>
        public Plast ContiguosPlast2
        {
            get => _contiguosPlast2;
            set
            {
                _contiguosPlast2 = value;
                OnPropertyChanged(nameof(ContiguosPlast2));
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Метод для корректного закрытия окна добавления пластов
        /// </summary>
        /// <param name="sender">Вызываемый объект</param>
        /// <param name="e"></param>
        public void Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Sort(_plasts);
            _plastWindow = null;
            GC.Collect();
            GC.WaitForFullGCComplete();
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Основной конструктор для AddPlastViewModel
        /// </summary>
        /// <param name="plasts">Коллекция пластов</param>
        /// <param name="plastWindow">Окно вызова</param>
        public AddPlastViewModel(ObservableCollection<Plast> plasts, AddPlastWindow plastWindow)
        {
            _plasts = plasts;
            plasts.Add(MainPlast = new Plast());
            _plastWindow = plastWindow;
        }

        #endregion

        #region Commands

        /// <summary>
        /// Команда закрытия окна с сохранением заданных пользователем пластов
        /// </summary>
        private RelayCommand exitWithSaveCommand;
        /// <summary>
        /// Логика работы команды exitWithSaveCommand
        /// </summary>
        public RelayCommand ExitWithSaveCommand
        {
            get
            {
                return exitWithSaveCommand ??
                (exitWithSaveCommand = new RelayCommand(obj =>
                {
                    _plastWindow.Close();
                }));
            }
        }

        /// <summary>
        /// Команда закрытия окна без сохранения заданых пользователем пластов
        /// </summary>
        private RelayCommand exitWithoutSaveCommand;
        /// <summary>
        /// Логика работы команды exitWithoutSaveCommand
        /// </summary>
        public RelayCommand ExitWithoutSaveCommand
        {
            get
            {
                return exitWithoutSaveCommand ??
                (exitWithoutSaveCommand = new RelayCommand(obj =>
                {
                    _plasts.Remove(MainPlast);
                    _plasts.Remove(ContiguosPlast1);
                    _plasts.Remove(ContiguosPlast2);
                    _plastWindow.Close();
                }));
            }
        }

        /// <summary>
        /// Команда открытия возможности вводить данные для сближенного пласта № 1, при выборе CheckBox
        /// </summary>
        private RelayCommand addContiguosPlastCommand1;
        /// <summary>
        /// Логика работы команды addContiguosPlastCommand
        /// </summary>
        public RelayCommand AddContiguosPlastCommand1
        {
            get
            {
                return addContiguosPlastCommand1 ??
                (addContiguosPlastCommand1 = new RelayCommand(obj =>
                {
                    if (!isAddContCom1Clicked)
                    {
                        _plastWindow.Plast1.IsEnabled = isAddContCom1Clicked = true;
                        _plasts.Add(ContiguosPlast1 = new Plast());
                        // ContiguosPlast1.Contiguos = MainPlast.GetHashCode();
                    }
                    else
                    {
                        _plasts.Remove(ContiguosPlast1);
                        ContiguosPlast1 = null;
                        _plastWindow.Plast1.IsEnabled = isAddContCom1Clicked = false;
                    }
                }));
            }
        }

        /// <summary>
        /// Команда открытия возможности вводить данные для сближенного пласта № 2, при выборе CheckBox
        /// </summary>
        private RelayCommand addContiguosPlastCommand2;
        /// <summary>
        /// Логика работы команды addContiguosPlastCommand2
        /// </summary>
        public RelayCommand AddContiguosPlastCommand2
        {
            get
            {
                return addContiguosPlastCommand2 ??
                (addContiguosPlastCommand2 = new RelayCommand(obj =>
                {
                    if (isAddContCom1Clicked)
                    {
                        if (!isAddContCom2Clicked)
                        {
                            isAddContCom2Clicked = _plastWindow.Plast2.IsEnabled = true;
                            _plasts.Add(ContiguosPlast2 = new Plast());
                            // ContiguosPlast2.Contiguos = MainPlast.GetHashCode();
                        }
                        else
                        {
                            _plastWindow.Plast2.IsEnabled = isAddContCom2Clicked = false;
                            _plasts.Remove(ContiguosPlast2);
                            ContiguosPlast2 = null;
                        }
                    }
                }));
            }
        }

        #endregion
    }
}