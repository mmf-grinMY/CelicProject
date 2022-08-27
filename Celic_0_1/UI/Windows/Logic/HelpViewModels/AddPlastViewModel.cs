using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// <summary> Коллекция разрабатываемых пластов </summary>
        private readonly ObservableCollection<Plast> _plasts;

        #endregion

        #region Public Properties

        /// <summary> Основной вводимый пласт </summary>
        public Plast MainPlast
        {
            get => _mainPlast;
            set
            {
                _mainPlast = value;
                OnPropertyChanged(nameof(MainPlast));
            }
        }

        #endregion

        #region Public Methods

        /// <summary> Корректное закрытие окна добавления пластов </summary>
        /// <param name="sender"> Закрываемое окно </param>
        /// <param name="e"> Параметры закрытия окна </param>
        public void Close(object sender, System.ComponentModel.CancelEventArgs e)
        {
            HelpManager.Sort(_plasts);
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
                _plastWindow.Close();
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

        #endregion
    }
}
