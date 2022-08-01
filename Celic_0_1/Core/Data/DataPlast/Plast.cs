using System;
using System.Collections.ObjectModel;
using static System.Char;
using static System.Single;
using static Celic.HelpManager;

namespace Celic
{
    /// <summary> Хранение данных о пласте </summary>
    public class Plast : BaseViewModel
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
            K = new EFloat(0);
            Ki = PD = new EFloat();
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

        // Общие параметры пласта
        //-------------------------------------
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
        //-------------------------------------------------
        // Параметры нахождения коэффициента извлечения ( при камерной системе разработки )
        //----------------------------------------------
        /// <summary> Коэффициент извлечения рудной массы в пределах вынимаемой мощности </summary>
        public EFloat Ki
        {
            set
            {
                if (_camera != null)
                {
                    _camera.Ki = value;
                    OnPropertyChanged(nameof(Ki));
                }
            }
            get => _camera != null ? _camera.Ki : new EFloat();
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
        //-------------------------------------------
        // Параметры для нахождения приведенной вынимаемой мощности ( при столбовой системе разработки )
        //-------------------------------------------
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
        //--------------------------------------------------------------
        // Параметры находжения коэффициента выработанного пространства
        //--------------------------------------------------------------
        /// <summary> Коэффициент, учитывающий размер выработанного пространства ( степень подработанности породного массива ) </summary>
        public EFloat K
        {
            get => _mineFields[0].K;
            set
            {
                // _mineFields[0].K = ValidateStringRange(value);
                _mineFields[0].K = value;
                B = PD = new EFloat();
                OnPropertyChanged(nameof(K));
            } 
        }
        /// <summary> Ширина выработанного пространства </summary>
        public EFloat PD
        {
            get => _mineFields[0].D;
            set
            {
                _mineFields[0].D = value;
                OnPropertyChanged(nameof(PD));
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
        //--------------------------------------------------------------
        // Параметры сближенных пластов
        //--------------------------------------------------------------
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
        //----------------------------------------------------------
        // Параметры для расчета высоты ЗВТ для нескольких пластов
        //----------------------------------------------------------
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
        //----------------------------------------------------------------------
        // Дополнительные парaметры пласта
        //----------------------------------------------------------------------
        /// <summary> Ширина предохранительного приразломного целика на уровне данного пласта со стороны лежачего крыла разломной зоны, м </summary>
        public float Bl { get; set; }
        /// <summary> Ширина предохранительного приразломного целика на уровне данного пласта со стороны висячего крыла разломной зоны, м </summary>
        public float Bv { get; set; }

        #endregion

        #region Private Methods
        
        /// <summary> Расчет значения коэффицента K </summary>
        /// <returns> Значение коэффициента </returns>
        private float CalcK()
        {
            if (TypeDev.Equals(CAMERA_DEV))
            {
                float d0 = D() * Mv.V * Ki.V;
                return PD.V >= 1.4F * d0 ? 1 : (float)Math.Sqrt(0.8 * (PD.V / (d0 - 0.2)));
            }
            else
            {
                float d0 = 1.4F * D() * Mv.V;
                return PD.V >= d0 ? 1 : (d0 != 0 ? (float)Math.Sqrt(PD.V / d0) : 0);
            }
        }
        /// <summary> Расчет значения коэффициента Ki </summary>
        /// <returns> Значение коэффициента </returns>
        private float CalcKi() => TypeDev.Equals(CAMERA_DEV) ? L.V != 0 && Mv.V != 0 ? Si.V / (L.V * Mv.V) : 1 : 1;
        /// <summary> Метод перерасчета коэффициента k для столбовой системы разработки </summary>
        private void CalcLavaArrayK()
        {
            ObservableCollection<Lava> arr = new ObservableCollection<Lava>(); 
            if (TypeDev.Equals(LAVA_DEV))
            {
                bool ef = false, bf = false;
                int value = 0;
                for (int i = 0; i < arr.Count - 1; i++)
                {
                    if (arr[i] is Lava lava1 && arr[i + 1] is Lava lava2)
                    {
                        _ = float.TryParse(lava1.B.ToString(), out float b1);
                        if (b1 < 0.58 * (lava1.Ht() + lava2.Ht()))
                        {
                            if (bf == false)
                            {
                                value = i;
                            }
                            else
                            {
                                if (ef == false && bf == true)
                                {
                                    float d0, d, d1, maxD, b, k = 0;
                                    for (int j = value; j < i - 1; j++)
                                    {
                                        if (arr[j] is Lava temp1 && arr[j + 1] is Lava temp2)
                                        {
                                            d = temp1.Mv.V;
                                            d0 = 1.4F * temp1.CalcD() * d;
                                            d1 = temp2.Mv.V;
                                            maxD = Math.Max(d, d1);
                                            b = temp1.B.V;
                                            k = Math.Max(k, maxD >= d0 ? 1 :
                                                (maxD + b >= d0 ? (float)Math.Sqrt(maxD / d0) : (d + d1) / (d + 2 * b + d1)));
                                        }
                                    }
                                    for (int j = value; j < i; j++)
                                    {
                                        if (arr[j] is Lava tmp)
                                        {
                                            tmp.K = new EFloat(k);
                                        }
                                    }
                                    ef = bf = false;
                                    value = 0;
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region Public Methods

        /// <summary> Расчет высоты ЗВТ одиночного пласта </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        public float Ht() => MPr() * D();
        /// <summary> Расчет коэффициента d, зависящего от системы разраьотки для пласта </summary>
        /// <returns> Значение коэффициента d</returns>
        public float D() => -0.01F * H.V + (TypeDev == "камерная" ? 26 : 46);
        /// <summary> Расчет приведенной вынимаемой мощности для пласта </summary>
        /// <returns> Значение приведенной вынимаеомй мощности </returns>
        public float MPr() => TypeDev.Equals(CAMERA_DEV) ? Mv.V * Ki.V * K.V : L.V != 0 ? Sl.V / L.V * K.V : 0;

        #endregion
    }
}
