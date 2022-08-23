using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Celic
{
	abstract class TwoListPlastViewModel : ListPlastViewModel
    {
        #region Private Fields

        /// <summary> Выбранный пласт из правого столбца пластов ( поле ) </summary>
        private Plast _rightPlastSelected;
        /// <summary> Выбранный пласт из левого столбца пластов ( поле ) </summary>
        private Plast _leftPlastSelected;

        #endregion

        #region Private Methods

        /// <summary> Логика работы команды удаления пласта </summary>
        /// <param name="obj"> Удаляемый пласт </param>
        private void RemoveCommandMethod(object obj)
        {
            if (obj != null && obj is Plast)
            {
            	Plast plast = obj as Plast;
                if (plast.Equals(LeftPlastSelected))
                {
                    LeftPlasts.Remove(LeftPlastSelected);
                    if (LeftPlasts.Count > 0)
                        LeftPlastSelected = LeftPlasts[0];
                }
                else if (plast == RightPlastSelected)
                {
                    RightPlasts.Remove(RightPlastSelected);
                    if (RightPlasts.Count > 0)
                        RightPlastSelected = RightPlasts[0];
                }
            }
            else
            {
                MessageBox.Show(RightPlasts.Count > 0 || LeftPlasts.Count > 0 ?
                    "Нельзя удалить элемент из этого столбца, так как нет выделенного пласта!" :
                    "Нельзя удалить элемент из пустого списка!");
            }
        }
        /// <summary> Логика работы команды удаления выделения пласта </summary>
        /// <param name="obj"> Выделенный в столбце пласт </param>
        private void RemoveSelectionCommandMethod(object obj)
        {
            if (SelectedPlast != null)
                if ((obj as Plast == LeftPlastSelected && SelectedPlast == LeftPlastSelected) ||
                (obj as Plast == RightPlastSelected && SelectedPlast == RightPlastSelected))
                    SelectedPlast = null;
                else
                    MessageBox.Show("Выбранный пласт находится в другом списке! " +
                        "Если вы хотите убрать выделение, то выберите удалить выделенный пласт в соседнем списке");
            else
                MessageBox.Show("Вы не можете удалить выделение пласта, так как оно отсутствует!");
        }
        /// <summary> Логика работы команды изменения столбцов </summary>
        private void SwapCommandMethod()
        {
            ObservableCollection<Plast> tmp = new ObservableCollection<Plast>();
            foreach (Plast plast in RightPlasts)
                tmp.Add(plast);
            RightPlasts.Clear();
            foreach (Plast plast in LeftPlasts)
                RightPlasts.Add(plast);
            LeftPlasts.Clear();
            foreach (Plast plast in tmp)
                LeftPlasts.Add(plast);
            tmp.Clear();
        }

        #endregion

        #region Public Properties

        /// <summary> Коллекция пластов, расположенных слева от целика </summary>
        public ObservableCollection<Plast> LeftPlasts { get; set; }
        /// <summary> Коллекция пластов, расположенных справа от целика </summary>
        public ObservableCollection<Plast> RightPlasts { get; set; }
        /// <summary> Выбранный пласт из левого столбца пластов </summary>
        public Plast LeftPlastSelected
        {
            get 
            {
            	return _leftPlastSelected;
            }
            set
            {
                if ((_leftPlastSelected = value) != null)
                {
                    RightPlastSelected = null;
                    SelectedPlast = _leftPlastSelected;
                }
                else
                {
                    SelectedPlast = RightPlastSelected;
                }
                OnPropertyChanged("LeftPlastSelected");
            }
        }
        /// <summary> Выбранный пласт из правого столбца пластов </summary>
        public Plast RightPlastSelected
        {
            get 
            {
            	return _rightPlastSelected;
            }
            set
            {
                if ((_rightPlastSelected = value) != null)
                {
                    LeftPlastSelected = null;
                    SelectedPlast = _rightPlastSelected;
                }
                else
                {
                    SelectedPlast = LeftPlastSelected;
                }
                OnPropertyChanged("RightPlastSelected");
            }
        }

        #endregion

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        /// <param name="mainWindow"> Главное окно приложения </param>
        /// <param name="calcWindow"> Вызывающеее окно расчета </param>
        public TwoListPlastViewModel(MainWindow mainWindow, Window calcWindow) : base(mainWindow, calcWindow)
        {
            LeftPlasts = new ObservableCollection<Plast>();
            RightPlasts = new ObservableCollection<Plast>();
            RemoveCommand = new RelayCommand(obj => RemoveCommandMethod(obj));
            RemoveSelectionCommand = new RelayCommand(obj => RemoveSelectionCommandMethod(obj), obj => _selectedPlast != null);
            SwapCommand = new RelayCommand(obj => SwapCommandMethod(),
                obj => RightPlasts != null && LeftPlasts != null);
            CalcWithDocxCommand = new RelayCommand(obj =>
            {
                Window _;
                if (LeftPlasts.Count == RightPlasts.Count)
                    if (LeftPlasts.Count > 0)
                        _ = new RepProWindow(this);
                    else
                        MessageBox.Show("Мы не можем произвести расчеты для несуществующих пластов");
                else
                    MessageBox.Show("Для успешного проведения расчета в столбцах должно находиться равное количество разрабатываемых пластов");
            }, obj => LeftPlasts.Count == RightPlasts.Count && LeftPlasts.Count > 0);
        }

        #endregion

        #region ListBox Commands

        /// <summary> Команда изменения столбцов местами </summary>
        public RelayCommand SwapCommand { private set; get; }

        #endregion
    }
}
