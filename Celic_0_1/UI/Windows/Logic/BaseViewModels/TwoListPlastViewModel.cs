using System.Windows;
using System.Collections.ObjectModel;

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

        #region Public Properties

        /// <summary> Коллекция пластов, расположенных слева от целика </summary>
        public ObservableCollection<Plast> LeftPlasts { get; set; }
        /// <summary> Коллекция пластов, расположенных справа от целика </summary>
        public ObservableCollection<Plast> RightPlasts { get; set; }
        /// <summary> Выбранный пласт из левого столбца пластов </summary>
        public Plast LeftPlastSelected
        {
            get => _leftPlastSelected;
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
                OnPropertyChanged(nameof(LeftPlastSelected));
            }
        }
        /// <summary> Выбранный пласт из правого столбца пластов </summary>
        public Plast RightPlastSelected
        {
            get => _rightPlastSelected;
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
                OnPropertyChanged(nameof(RightPlastSelected));
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
            RemoveCommand = new RelayCommand(obj => 
            {
                LeftPlasts.Remove(SelectedPlast);
                RightPlasts.Remove(SelectedPlast);
            }, obj => SelectedPlast != null);
            SwapCommand = new RelayCommand(obj => 
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
            }, obj => RightPlasts.Count > 0 || LeftPlasts.Count > 0);
            CalcWithDocxCommand = new RelayCommand(obj =>
            {
                if (LeftPlasts.Count == RightPlasts.Count)
                    if (LeftPlasts.Count > 0)
                        _ = new ReportProgressWindow(this);
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
