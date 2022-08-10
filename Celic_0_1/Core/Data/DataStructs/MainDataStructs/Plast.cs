using System;
using System.Collections.ObjectModel;
using static Celic.HelpManager;
using static System.Math;

namespace Celic
{
    /// <summary> Хранение данных о пласте </summary>
    public class Plast : BaseViewModel
    {
        #region Private Fields

        /// <summary> Имя пласта ( поле) </summary>
        private string _name;
        /// <summary> Калийный горизонт ( поле ) </summary>
        private string _gorizont;
        /// <summary> Система разработки пласта </summary>
        private string _typeDev;
        /// <summary> Уникальный идентификатор пласта ( поле ) </summary>
        private readonly int myID;
        /// <summary> Количество созданных пластов </summary>
        private static int id;
        /// <summary> Сближенный к данному пласт, находящийся сверху ( поле ) </summary>
        private string _top;
        /// <summary> Сближенный к данному пласт, находящийся снизу ( поле ) </summary>
        private string _buttom;
        /// <summary> Коэффициент степени влияния выемки ( поле ) </summary>
        private float _s;
        /// <summary> Коэффициент деформирования массива ( поле ) </summary>
        private float _sz;
        /// <summary> Коэффициент порядка разработки пластов ( поле ) </summary>
        private float _kt;
        /// <summary> Год отработки рассматриваемого пласта, год ( поле ) </summary>
        private int _t;
        /// <summary> Фактическая высота ЗВТ ( поле ) </summary>
        private float _hf;
        /// <summary> Расстояние в плане между границами остановки работ на 1-м и рассматриваемом пластах, м ( поле ) </summary>
        private float _lp;
        /// <summary> Главное шахтное поле среди списка рассматриваемых ( поле ) </summary>
        private MineField _mainMineField;

        #endregion

        #region Public Properties

        /// <summary> Имя пласта </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        /// <summary> Система разработки шахтного поля </summary>
        public string TypeDev
        {
            get => _typeDev;
            set
            {
                if(_typeDev != value)
                {
                    _typeDev = value;
                    if (_typeDev.Equals(LAVA_DEV))
                    {
                        MineFields?.Clear();
                        MineFields.Add(new Lava());
                    }
                    else
                    {
                        MineFields?.Clear();
                        MineFields.Add(new Camera());
                    }
                    OnPropertyChanged("TypeDev");
                }
            }
        }
        /// <summary> Калийный горизонт </summary>
        public string Gorizont
        {
            get => _gorizont;
            set
            {
                _gorizont = value;
                OnPropertyChanged("Gorizont");
            }
        }
        /// <summary> Сближенный к данному пласт, находящийся сверху </summary>
        public string Top
        {
            get => _top;
            set
            {
                _top = value;
                OnPropertyChanged("Top");
            }
        }
        /// <summary> Сближенный к данному пласт, находящийся снизу </summary>
        public string Buttom
        {
            get => _buttom;
            set
            {
                _buttom = value;
                OnPropertyChanged("Buttom");
            }
        }
        /// <summary> Коэффициент, учитывающий степень влияния выемки разрабатываемых пластов на развитие техногенных водопроводящих трещин в зависимости от взаимного положения ( смещения в плане ) границ остановки работ </summary>
        public float S
        {
            get => _s;
            set
            {
                _s = value;
                OnPropertyChanged("S");
            }
        }
        /// <summary> Коэффициент, учитывающий характер деформирования массива пород в краевой части мульды сдвижения над угловыми участками выработанного пространства, определяемые согласно отдельным рекомендациям специализированной организации( 0 ≤ S_z ≤ 1 ); при отсутствии таких рекомендаций значение принимается равным 1 </summary>
        public float Sz 
        {
            get => _sz;
            set
            {
                _sz = value;
                OnPropertyChanged("Sz");
            }
        }
        /// <summary> Коэффициент, учитывающий порядок разработки пластов и интервал времени между их отработкой </summary>
        public float Kt 
        {
            get => _kt;
            set
            {
                _kt = value;
                OnPropertyChanged("Kt");
            }
        }
        /// <summary> Год отработки рассматриваемого пласта, год. </summary>
        public int T
        {
            get => _t;
            set
            {
                _t = value <= DateTime.Now.Year && value >= 1900 ? value : DateTime.Now.Year;
                OnPropertyChanged("T");
            }
        }
        /// <summary> Фактическая максимальная высота распространения ЗВТ над нижележащим пластом спустя время t с момента его отработки(определяется опытным путем), м; при отсутствии данных о фактической максимальной высоте распространения ЗВТ над нижележащим разрабатываемым пластом принимается равной H_t; </summary>
        public float Hf
        {
            get => _hf;
            set
            {
                _hf = value;
                OnPropertyChanged("Hf");
            }
        }
        /// <summary> Расстояние в плане между границами остановки работ на 1-м и рассматриваемом пластах, м ( поле ) </summary>
        public float Lp
        {
            get => _lp;
            set
            {
                _lp = value;
                OnPropertyChanged("Lp");
            }
        }
        /// <summary> Ширина предохранительного приразломного целика на уровне данного пласта со стороны лежачего крыла разломной зоны, м </summary>
        public float Bl { get; set; }
        /// <summary> Ширина предохранительного приразломного целика на уровне данного пласта со стороны висячего крыла разломной зоны, м </summary>
        public float Bv { get; set; }
        /// <summary> Список шахтных полей, разрабатываемых на данном пласте </summary>
        public ObservableCollection<MineField> MineFields { get; set; }
        /// <summary> Главное шахтное поле среди списка рассматриваемых </summary>
        public MineField MainMineField
        {
            get => _mainMineField;
            set
            {
                _mainMineField = value;
                OnPropertyChanged("MainMineField");
            }
        }

        #endregion

        #region Constructors

        /// <summary> Статический констуктор для данного класса </summary>
        static Plast() => id = 0;
        /// <summary> Основной констуктор для данного класса </summary>
        public Plast()
        {
            MineFields = new ObservableCollection<MineField>();
            TypeDev = CAMERA_DEV;
            myID = ++id;
            Name = $"Пласт_{myID}";
            S = Sz = Kt = 1;
            Top = Buttom = UNDEFINE_DEV;
        }

        #endregion
    }

    /// <summary> Класс, обслуживающий стуктуру данных Plast </summary>
    public class PlastManager : IMineFieldManage
    {
        #region Private Fields

        /// <summary> Рассматриваемый пласт </summary>
        private readonly Plast _plast;

        #endregion

        #region Public Methods

        /// <summary> Расчет высоты ЗВТ одиночного пласта </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        public float Ht() => MPr() * CalcD();
        /// <summary> Расчет параметра, зависящего от системы разработки для пласта </summary>
        /// <returns> Значение параметра, зависящего от системы разработки </returns>
        public float CalcD() => SelectTypeManager().CalcD();
        /// <summary> Расчет приведенной вынимаемой мощности для пласта </summary>
        /// <returns> Значение приведенной вынимаеомй мощности </returns>
        public float MPr() => SelectTypeManager().MPr();
        /// <summary> Перерасчет коэффициентов К для коллекции шахтных полей </summary>
        public float RecalcK()
        {
            float maxK = 0;
            if (_plast.TypeDev.Equals(CAMERA_DEV))
            {
                maxK = new CameraManager(_plast.MainMineField as Camera).RecalcK();
            }
            else
            {
                for (int i = 0; i < _plast.MineFields.Count - 1; i++)
                    if (_plast.MineFields[i] is Lava lava1 && _plast.MineFields[i + 1] is Lava lava2)
                    {
                        LavaManager manager1 = new LavaManager(lava1), manager2 = new LavaManager(lava2);
                        maxK = Max(maxK, lava1.B.V >= 0.58F * (manager1.Ht() + manager2.Ht()) ? manager1.RecalcK() : manager1.ERecalcK(lava2));
                    }
                        
            }
            return _plast.MainMineField.K = maxK;
        }

        #endregion

        #region Public Static Methods

        /// <summary> Расчет высоты ЗВТ одиночного пласта </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        public static float Ht(Plast plast)
        {
            PlastManager manager = new PlastManager(plast);
            return manager.MPr() * manager.CalcD();
        }
        /// <summary> Расчет параметра, зависящего от системы разработки для пласта </summary>
        /// <returns> Значение параметра, зависящего от системы разработки </returns>
        public static float CalcD(Plast plast) => new PlastManager(plast).SelectTypeManager().CalcD();

        #endregion

        #region Private Help Methods

        /// <summary> Выбор обслуживающего класса для шахтного поля по его системе разработки </summary>
        /// <returns> Обслуживающий класс </returns>
        private IMineFieldManage SelectTypeManager()
        {
            if (_plast.TypeDev.Equals(CAMERA_DEV))
                return new CameraManager((Camera)_plast.MainMineField);
            else
                return new LavaManager((Lava)_plast.MainMineField);
        }

        #endregion

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        /// <param name="plast"> Рассматриваемый пласт </param>
        public PlastManager(Plast plast) => _plast = plast;

        #endregion
    }
}