using System;
using System.Collections.ObjectModel;
using PCM = Celic.PlastCollectionManager;

namespace Celic
{
	/// <summary> Логика работы окна добавления пластов </summary>
    class AddPlastViewModel : BaseViewModel
    {
        #region Private Fields

        /// <summary> Окно, с которым в данный момент работает ViewModel </summary>
        private AddPlastWindow _plastWindow;
        /// <summary> Основной вводимый пласт ( поле ) </summary>
        private Plast _mainPlast;
        /// <summary> Сближенный пласт № 1 ( поле ) </summary>
        private Plast _contiguosPlast1;
        /// <summary> Сближеный пласт № 2 ( поле ) </summary>
        private Plast _contiguosPlast2;
        /// <summary> Коллекция разрабатываемых пластов </summary>
        private readonly ObservableCollection<Plast> _plasts;
        /// <summary> Флаг выбора добавления сближенного пласта № 1 </summary>
        private bool isAddContCom1Clicked = false;
        /// <summary> Флаг выбора добавления сближенного пласта № 2 </summary>
        private bool isAddContCom2Clicked = false;

        #endregion

        #region Public Properties

        /// <summary> Основной вводимый пласт </summary>
        public Plast MainPlast
        {
            get 
            {
            	return _mainPlast;
            }
            set
            {
                _mainPlast = value;
                OnPropertyChanged("MainPlast");
            }
        }
        /// <summary> Сближенный пласт № 1 </summary>
        public Plast ContiguosPlast1
        {
            get 
            {
            	return _contiguosPlast1;
            }
            set
            {
                _contiguosPlast1 = value;
                OnPropertyChanged("ContiguosPlast1");
            }
        }
        /// <summary> Сближеный пласт № 2 </summary>
        public Plast ContiguosPlast2
        {
            get 
            {
            	return _contiguosPlast2;
            }
            set
            {
                _contiguosPlast2 = value;
                OnPropertyChanged("ContiguosPlast2");
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
            PCM.Sort(_plasts);
            _plastWindow = null;
            GC.Collect();
            GC.WaitForFullGCComplete();
        }

        #endregion

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        /// <param name="plasts"> Коллекция пластов </param>
        /// <param name="plastWindow"> Вызывающее модель окно </param>
        public AddPlastViewModel(ObservableCollection<Plast> plasts, AddPlastWindow plastWindow)
        {
            ExitWithSaveCommand = new RelayCommand(obj => _plastWindow.Close());
            ExitWithoutSaveCommand = new RelayCommand(obj =>
            {
                _plasts.Remove(MainPlast);
                _plasts.Remove(ContiguosPlast1);
                _plasts.Remove(ContiguosPlast2);
                _plastWindow.Close();
            });
            AddContiguosPlastCommand1 = new RelayCommand(obj =>
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
            });
            AddContiguosPlastCommand2 = new RelayCommand(obj =>
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
            });
            _plasts = plasts;
            plasts.Add(MainPlast = new Plast());
            _plastWindow = plastWindow;
        }

        #endregion

        #region Commands

        /// <summary> Закрытие окна с сохранением записанных пластов </summary>
        public RelayCommand ExitWithSaveCommand { private set; get; }
        /// <summary> Команда закрытия окна без сохранения заданых пользователем пластов </summary>
        public RelayCommand ExitWithoutSaveCommand { private set; get; }
        /// <summary> Команда открытия возможности вводить данные для сближенного пласта № 1, при выборе CheckBox </summary>
        public RelayCommand AddContiguosPlastCommand1 { private set; get; }
        /// <summary> Команда открытия возможности вводить данные для сближенного пласта № 2, при выборе CheckBox </summary>
        public RelayCommand AddContiguosPlastCommand2 { private set; get; }

        #endregion
    }
}
