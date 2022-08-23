using System;
using System.Collections.ObjectModel;

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
        private MineDev _typeDev;
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
        private MineField _main;

        #endregion

        #region Public Properties

        /// <summary> Имя пласта </summary>
        public string Name
        {
            get 
            {
            	return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }
        /// <summary> Система разработки шахтного поля </summary>
        public string TypeDev
        {
            get 
            {
            	return _typeDev.ToString();
            }
            set
            {
            	MineDev newValue = MineDevManager.ToMineDev(value);
                if(_typeDev != newValue)
                {
                	_typeDev = newValue;
                	switch(_typeDev)
                	{
                		case MineDev.camera: CreateMineFieldsWithCamera(); break;
                		case MineDev.lava: CreateMineFiledsWithLava(); break;
                	}
                    OnPropertyChanged("TypeDev");
                }
            }
        }
        
        /// <summary> Калийный горизонт </summary>
        public string Gorizont
        {
        	get 
        	{
        		return _gorizont;
        	}
            set
            {
                _gorizont = value;
                OnPropertyChanged("Gorizont");
            }
        }
        /// <summary> Сближенный к данному пласт, находящийся сверху </summary>
        public string Top
        {
        	get 
        	{
        		return _top;
        	}
            set
            {
                _top = value;
                OnPropertyChanged("Top");
            }
        }
        /// <summary> Сближенный к данному пласт, находящийся снизу </summary>
        public string Buttom
        {
        	get 
        	{
        		return _buttom;
        	}
            set
            {
                _buttom = value;
                OnPropertyChanged("Buttom");
            }
        }
        /// <summary> Коэффициент, учитывающий степень влияния выемки разрабатываемых пластов на развитие техногенных водопроводящих трещин в зависимости от взаимного положения ( смещения в плане ) границ остановки работ </summary>
        public float S
        {
            get 
            {
            	return _s;
            }
            set
            {
                _s = value;
                OnPropertyChanged("S");
            }
        }
        /// <summary> Коэффициент, учитывающий характер деформирования массива пород в краевой части мульды сдвижения над угловыми участками выработанного пространства, определяемые согласно отдельным рекомендациям специализированной организации( 0 ≤ S_z ≤ 1 ); при отсутствии таких рекомендаций значение принимается равным 1 </summary>
        public float Sz 
        {
        	get 
        	{
        		return _sz;
        	}
            set
            {
                _sz = value;
                OnPropertyChanged("Sz");
            }
        }
        /// <summary> Коэффициент, учитывающий порядок разработки пластов и интервал времени между их отработкой </summary>
        public float Kt 
        {
            get 
            {
            	return _kt;
            }
            set
            {
                _kt = value;
                OnPropertyChanged("Kt");
            }
        }
        /// <summary> Год отработки рассматриваемого пласта, год. </summary>
        public int T
        {
            get 
            {
            	return _t;
            }
            set
            {
                _t = value <= DateTime.Now.Year && value >= 1900 ? value : DateTime.Now.Year;
                OnPropertyChanged("T");
            }
        }
        /// <summary> Фактическая максимальная высота распространения ЗВТ над нижележащим пластом спустя время t с момента его отработки(определяется опытным путем), м; при отсутствии данных о фактической максимальной высоте распространения ЗВТ над нижележащим разрабатываемым пластом принимается равной H_t; </summary>
        public float Hf
        {
            get 
            {
            	return _hf;
            }
            set
            {
                _hf = value;
                OnPropertyChanged("Hf");
            }
        }
        /// <summary> Расстояние в плане между границами остановки работ на 1-м и рассматриваемом пластах, м ( поле ) </summary>
        public float Lp
        {
            get 
            {
            	return _lp;
            }
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
        public MineField Main
        {
            get 
            {
            	return _main;
            }
            set
            {
                _main = value;
                OnPropertyChanged("Main");
            }
        }

        #endregion

        #region Constructors

        /// <summary> Статический констуктор для данного класса </summary>
        static Plast() 
        {
        	id = 0;
        }
        /// <summary> Основной констуктор для данного класса </summary>
        public Plast()
        {
            MineFields = new ObservableCollection<MineField>();
            TypeDev = HelpManager.CAMERA_DEV;
            myID = ++id;
            Name = "Пласт_" + myID;
            S = Sz = Kt = 1;
            Top = Buttom = HelpManager.UNDEFINE_DEV;
        }

        #endregion
        
        #region Help Private Methods
        
        private void CreateMineFieldsWithCamera()
        {
        	if(MineFields != null)
            	MineFields.Clear();
            MineFields.Add(new Camera());
        }
        private void CreateMineFiledsWithLava()
        {
        	if(MineFields != null)
           	    MineFields.Clear();
            MineFields.Add(new Lava());
        }
        
        #endregion
    }
}
