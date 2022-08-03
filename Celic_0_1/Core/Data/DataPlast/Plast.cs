using System;
using System.Collections.ObjectModel;
using static Celic.HelpManager;

namespace Celic
{
    // Если TypeDev.Equals(LAVA_DEV) -> Mv <= 3;
    /// <summary> Хранение данных о пласте </summary>
    public class Plast : BaseViewModel, IMineFieldManage
    {
        #region Constructors

        /// <summary> Статический констуктор для данного класса </summary>
        static Plast() => id = 0;
        /// <summary> Основной констуктор для данного класса </summary>
        public Plast()
        {
            _mineFields = new ObservableCollection<MineField> { (_lava = new Lava()) };
            myID = ++id;
            Name = $"Пласт_{myID}";
            Ki = D = new EFloat(-1);
            S = Sz = Kt = new EFloat(-1);
            Top = Buttom = UNDEFINE_DEV;
        }

        #endregion

        #region Private Fields

        /// <summary> Коллекция рассматриваемых шахтных полей, расположенных на данном пласте </summary>
        private readonly ObservableCollection<MineField> _mineFields;
        /// <summary> Калийный горизонт ( поле ) </summary>
        private string _gorizont;
        /// <summary> Шахтное поле с камерной системой разработки </summary>
        private Camera _camera;
        /// <summary> Шахтное поле со столбовой системой разработки </summary>
        private Lava _lava;
        /// <summary> Имя пласта ( поле) </summary>
        private string _name;
        /// <summary> Уникальный идентификатор пласта ( поле ) </summary>
        private readonly int myID;
        /// <summary> Количество созданных пластов </summary>
        private static int id;
        /// <summary> Сближенный к данному пласт, находящийся сверху ( поле ) </summary>
        private string _top;
        /// <summary> Сближенный к данному пласт, находящийся снизу ( поле ) </summary>
        private string _buttom;
        /// <summary> Коэффициент степени влияния выемки ( поле ) </summary>
        private EFloat _s;
        /// <summary> Коэффициент деформирования массива ( поле ) </summary>
        private EFloat _sz;
        /// <summary> Коэффициент порядка разработки пластов ( поле ) </summary>
        private EFloat _kt;
        /// <summary> Год отработки рассматриваемого пласта, год ( поле ) </summary>
        private int _t;
        /// <summary> Фактическая высота ЗВТ ( поле ) </summary>
        private EFloat _hf;
        /// <summary> Расстояние в плане между границами остановки работ на 1-м и рассматриваемом пластах, м ( поле ) </summary>
        private EFloat _lp;

        #endregion

        #region Public Properties

        /// <summary> Имя пласта </summary>
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        /// <summary> Система разработки шахтного поля </summary>
        public string TypeDev
        {
            set
            {
                if(TypeDev != value)
                { 
                    if (value.Equals(LAVA_DEV))
                    {
                        _mineFields.Clear();
                        _camera = null;
                        _mineFields.Add(_lava = new Lava());
                    }
                    else
                    {
                        _mineFields.Clear();
                        _lava = null;
                        _mineFields.Add(_camera = new Camera());
                    }
                    OnPropertyChanged(nameof(TypeDev));
                }
            }
            get => _mineFields[0].TypeDev;
        }
        /// <summary> Глубина ведения горных работ в пределах от 350 до 1000 м </summary>
        public EFloat H
        {
            set
            {
                // _mineFields[0].H = ValidateStringRange(value, 300, 1000);
                _mineFields[0].H = value;
                OnPropertyChanged(nameof(H));
            }
            get => _mineFields[0].H;
        }
        /// <summary> Вынимаемая мощность пласта(высота камер), м </summary>
        public EFloat Mv
        {
            set
            {
                // _mineFields[0].Mv = ValidateStringRange(value, 0, 3);
                _mineFields[0].Mv = value;
                OnPropertyChanged(nameof(Mv));
            }
            get => _mineFields[0].Mv;
        }
        /// <summary> Калийный горизонт </summary>
        public string Gorizont
        {
            get => _gorizont;
            set
            {
                _gorizont = value;
                OnPropertyChanged(nameof(Gorizont));
            }
        }
        /// <summary> Коэффициент извлечения рудной массы в пределах вынимаемой мощности </summary>
        public EFloat Ki
        {
            set
            {
                if (_camera != null)
                {
                    if ((_camera.Ki = value).IsClear())
                        _camera.CalcKi();
                    OnPropertyChanged(nameof(Ki));
                }
            }
            get => _camera != null ? _camera.Ki : new EFloat(-1);
        }
        /// <summary> Расстояние между осями соседних междукамерных целиков ( только для шахтного поля с камерной системой разработки ) </summary>
        public EFloat L
        {
            get => _camera != null ? _camera.L : new EFloat();
            set
            {
                if (_camera != null)
                    _camera.L = value;
                OnPropertyChanged(nameof(L));
            }
        }
        /// <summary> Сумма поперечных сечений выработок, составляющих очистную камеру ( только для шахтного поля с камерной системой разработки ) </summary>
        public EFloat Si
        {
            get => _camera != null ? _camera.Si : new EFloat();
            set
            {
                if (_camera != null)
                    _camera.Si = value;
                OnPropertyChanged(nameof(Si));
            }
        }
        /// <summary> Площадь поперечного сечения лавы ( только для шахтного поля со столбовой системой разработки ) </summary>
        public EFloat Sl
        {
            get => _lava != null ? _lava.Sl : new EFloat();
            set
            {
                if (_lava != null)
                    _lava.Sl = value;
                OnPropertyChanged(nameof(Sl));
            }
        }
        /// <summary> Длина лавы ( только для шахтного поля со столбовой системой разработки ) </summary>
        public EFloat Ll
        {
            get => _lava != null ? _lava.L : new EFloat();
            set
            {
                if (_lava != null)
                    _lava.L = value;
                OnPropertyChanged(nameof(Ll));
            }
        }
        /// <summary> Коэффициент, учитывающий размер выработанного пространства ( степень подработанности породного массива ) </summary>
        public EFloat K
        {
            get => _mineFields[0].K;
            set
            {
                if ((_mineFields[0].K = value).IsClear())
                    CalcSimpleK();
                OnPropertyChanged(nameof(K));
            } 
        }
        /// <summary> Ширина выработанного пространства </summary>
        public EFloat D
        {
            get => _mineFields[0].D;
            set
            {
                _mineFields[0].D = value;
                OnPropertyChanged(nameof(D));
            }
        }
        /// <summary> Расстояние между штреками ( только для шахтного поля со столбовой системой разработки ) </summary>
        public EFloat B
        {
            get => _lava != null ? _lava.B : new EFloat();
            set
            {
                if (_lava != null)
                    _lava.B = value;
                OnPropertyChanged(nameof(B));
            }
        }
        /// <summary> Сближенный к данному пласт, находящийся сверху </summary>
        public string Top
        {
            get => _top;
            set
            {
                _top = value;
                OnPropertyChanged(nameof(Top));
            }
        }
        /// <summary> Сближенный к данному пласт, находящийся снизу </summary>
        public string Buttom
        {
            get => _buttom;
            set
            {
                _buttom = value;
                OnPropertyChanged(nameof(Buttom));
            }
        }
        /// <summary> Коэффициент, учитывающий степень влияния выемки разрабатываемых пластов на развитие техногенных 
        /// водопроводящих трещин в зависимости от взаимного положения ( смещения в плане ) границ остановки работ </summary>
        public EFloat S
        {
            get => _s;
            set
            {
                _s = value;
                OnPropertyChanged(nameof(S));
            }
        }
        /// <summary> Коэффициент, учитывающий характер деформирования массива пород в краевой части мульды сдвижения над
        /// угловыми участками выработанного пространства, определяемые согласно отдельным рекомендациям
        /// специализированной организации( 0 ≤ S_z ≤ 1 ); при отсутствии таких рекомендаций значение принимается равным 1 </summary>
        public EFloat Sz 
        {
            get => _sz;
            set
            {
                _sz = value;
                OnPropertyChanged(nameof(Sz));
            }
        }
        /// <summary> Коэффициент, учитывающий порядок разработки пластов и интервал времени между их отработкой </summary>
        public EFloat Kt 
        {
            get => _kt;
            set
            {
                _kt = value;
                OnPropertyChanged(nameof(Kt));
            }
        }
        /// <summary> Год отработки рассматриваемого пласта, год. </summary>
        public int T
        {
            get => _t;
            set
            {
                _t = value <= DateTime.Now.Year && value >= 1900 ? value : DateTime.Now.Year;
                OnPropertyChanged(nameof(T));
            }
        }
        /// <summary>
        /// Фактическая максимальная высота распространения ЗВТ над нижележащим пластом 
        /// спустя время t с момента его отработки(определяется опытным путем), м;
        /// при отсутствии данных о фактической максимальной высоте распространения ЗВТ над
        /// нижележащим разрабатываемым пластом принимается равной H_t;
        /// </summary>
        public EFloat Hf
        {
            get => _hf;
            set
            {
                _hf = value;
                OnPropertyChanged(nameof(Hf));
            }
        }
        /// <summary> Расстояние в плане между границами остановки работ на 1-м и рассматриваемом пластах, м ( поле ) </summary>
        public EFloat Lp
        {
            get => _lp;
            set
            {
                _lp = value;
                OnPropertyChanged(nameof(_lp));
            }
        }
        /// <summary> Ширина предохранительного приразломного целика на уровне данного пласта со стороны лежачего крыла разломной зоны, м </summary>
        public float Bl { get; set; }
        /// <summary> Ширина предохранительного приразломного целика на уровне данного пласта со стороны висячего крыла разломной зоны, м </summary>
        public float Bv { get; set; }
        /// <summary> Шахтное поле под индексом № index </summary>
        /// <param name="index"> Номер индекса </param>
        /// <returns> Шахтное поле </returns>
        public MineField this[int index] { get => _mineFields[index]; }
        /// <summary> Количество шахтных полей на данном пласте </summary>
        public int Count { get => _mineFields.Count; }

        #endregion

        #region Public Methods

        /// <summary> Расчет высоты ЗВТ одиночного пласта </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        public float Ht() => MPr() * CalcD();
        /// <summary> Расчет коэффициента d, зависящего от системы разраьотки для пласта </summary>
        /// <returns> Значение коэффициента d</returns>
        public float CalcD() => _mineFields[0].CalcD();
        /// <summary> Расчет приведенной вынимаемой мощности для пласта </summary>
        /// <returns> Значение приведенной вынимаеомй мощности </returns>
        public float MPr() => _mineFields[0].MPr();

        #endregion

        #region Private Methods

        /// <summary> Простой расчет коэффициентов К для шахтных полей </summary>
        private void CalcSimpleK()
        {
            if (TypeDev.Equals(CAMERA_DEV))
                _mineFields[0].K = new EFloat((_mineFields[0] as Camera).CalcK());
            else
                RecalcKLava();
        }
        /// <summary> Расчет коэфициентов K для шахтных полей со столбовой системой разработки </summary>
        private void RecalcKLava()
        {
            if (TypeDev.Equals(LAVA_DEV))
            {
                float maxK = 0;
                for (int i = 0; i < _mineFields.Count - 1; i++)
                {
                    if (_mineFields[i] is Lava lava1 && _mineFields[i + 1] is Lava lava2)
                    {
                        if(lava1.B.V >= 0.58F * (lava1.Ht() + lava2.Ht()))
                        {
                            lava1.RecalcK();
                            maxK = Max(maxK, lava1.K.V);
                        }
                        else
                        {
                            float d0 = 1.4F * lava1.CalcD() * lava1.Mv.V;
                            float d1 = lava1.D.V;
                            float d = lava2.D.V;
                            float b = lava1.B.V;
                            if (300 >= Max(d, d1) && Max(d, d1) >= d0)
                                lava1.K = new EFloat(maxK = Max(maxK,1));
                            else if (Max(d, d1) < d0)
                                lava1.K = new EFloat(maxK = Max(maxK, Max(d, d1) + b >= d0 ? (float)Sqrt(d0 != 0 ? Max(d, d1) / d0 : 0) :
                                    (d + 2 * b + d1) != 0 ? (d + d1) / (d + 2 * b + d1) : 0));
                        }
                    }
                }
                _mineFields[0].K = new EFloat(maxK);
            }
        }

        #endregion
    }
}