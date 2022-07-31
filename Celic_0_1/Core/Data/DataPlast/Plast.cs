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
            TypeDev = "столбовая";
            Mv = "2,5";
            Ki = "1";
            H = "500";
            myID = ++id;
            Name = $"Пласт_{myID}";
            K = Ki = PD = "";
            Top = Buttom = "отсутствует";
        }

        #endregion

        #region Private Fields

        /// <summary> Вынимаемая мощность пласта(высота камер), м ( поле ) </summary>
        private string _mv;
        /// <summary> Глубина ведения горных работ в пределах от 350 до 1000 м ( поле )</summary>
        private string _h;
        /// <summary> Коэффициент извлечения рудной массы в пределах вынимаемой мощности ( поле ) </summary>
        private string _ki;
        /// <summary> Калийный горизонт ( поле ) </summary>
        private string _gorizont;
        /// <summary> Система разработки шахтного поля ( поле ) </summary>
        private string _typeDev;
        /// <summary> Коэффициент, учитывающий размер выработанного пространства ( степень подработанности породного массива ) ( поле ) </summary>
        private string _k;
        /// <summary> Шахтное поле с камерной системой разработки </summary>
        private Camera _camera;
        /// <summary> Шахтное поле со столбовой системой разработки </summary>
        private Lava _lava;
        /// <summary> Ширина выработанного пространства ( поле ) </summary>
        private string _d;
        /// <summary> Имя пласта ( поле)  </summary>
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
        private string _s;
        /// <summary> Коэффициент деформирования массива ( поле ) </summary>
        private string _sz;
        /// <summary> Коэффициент порядка разработки пластов ( поле ) </summary>
        private string _kt;

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
                if ((_typeDev = value) == "столбовая")
                {
                    _camera = null;
                    _lava = new Lava();
                }
                else
                {
                    _camera = new Camera();
                    _lava = null;
                }
                OnPropertyChanged(nameof(TypeDev));
            }
            get => _typeDev;
        }
        /// <summary> Глубина ведения горных работ в пределах от 350 до 1000 м </summary>
        public string H
        {
            set
            {
                _h = ValidateStringRange(value, 300, 1000);
                OnPropertyChanged(nameof(H));
            }
            get => _h;
        }
        /// <summary> Вынимаемая мощность пласта(высота камер), м </summary>
        public string Mv
        {
            set
            {
                _mv = ValidateStringRange(value, 0, 3);
                OnPropertyChanged(nameof(Mv));
            }
            get => _mv;
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
        // Параметры нахождения коэффициента извлечения
        //----------------------------------------------
        /// <summary> Коэффициент извлечения рудной массы в пределах вынимаемой мощности </summary>
        public string Ki
        {
            set
            {
                _ki = ValidateStringRange(value);
                Si = L = Ll = Sl = string.Empty;
                OnPropertyChanged(nameof(Ki));
            }
            get => _ki;
        }       
        /// <summary> Площадь поперечного сечения лавы ( только для шахтного поля со столбовой системой разработки ) </summary>
        public string Sl
        {
            get => _lava != null ? _lava.Sl : string.Empty;
            set
            {
                if (_lava != null)
                    _lava.Sl = ValidateString(value);
                OnPropertyChanged(nameof(Sl));
            }
        }
        /// <summary> Длина лавы ( только для шахтного поля со столбовой системой разработки ) </summary>
        public string Ll
        {
            get => _lava != null ? _lava.L : string.Empty;
            set
            {
                if (_lava != null)
                    _lava.L = ValidateString(value);
                OnPropertyChanged(nameof(Ll));
            }
        }
        /// <summary> Расстояние между соседними осями междукамерных целиков ( только для шахтного поля с камерной системой разработки ) </summary>
        public string L
        {
            get => _camera != null ? _camera.L : string.Empty;
            set
            {
                if (_camera != null)
                    _camera.L = ValidateString(value);
                OnPropertyChanged(nameof(L));
            }
        }
        /// <summary> Сумма поперечных сечений выработок, составляющих очистную камеру ( только для шахтного поля с камерной системой разработки ) </summary>
        public string Si
        {
            get => _camera != null ? _camera.Si : string.Empty;
            set
            {
                if (_camera != null)
                    _camera.Si = ValidateString(value);
                OnPropertyChanged(nameof(Si));
            }
        }
        //--------------------------------------------------------------
        // Параметры находжения коэффициента выработанного пространства
        //--------------------------------------------------------------
        /// <summary> Коэффициент, учитывающий размер выработанного пространства ( степень подработанности породного массива ) </summary>
        public string K
        {
            get => _k;
            set
            {
                _k = ValidateStringRange(value);
                B = PD = string.Empty;
                OnPropertyChanged(nameof(K));
            } 
        }
        /// <summary> Ширина выработанного пространства </summary>
        public string PD
        {
            get => _d;
            set
            {
                _d = ValidateString(value);
                OnPropertyChanged(nameof(PD));
            }
        }
        /// <summary> Расстояние между штреками ( только для шахтного поля со столбовой системой разработки ) </summary>
        public string B
        {
            get => _lava != null ? _lava.B : string.Empty;
            set
            {
                if (_lava != null)
                    _lava.B = ValidateString(value);
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
        public string S 
        {
            get => _s;
            set
            {
                _s = ValidateString(value);
                OnPropertyChanged(nameof(S));
            }
        }
        /// <summary> Коэффициент, учитывающий характер деформирования массива пород в краевой части мульды сдвижения над
        /// угловыми участками выработанного пространства, определяемые согласно отдельным рекомендациям
        /// специализированной организации( 0 ≤ S_z ≤ 1 ); при отсутствии таких рекомендаций значение принимается равным 1 </summary>
        public string Sz 
        {
            get => _sz;
            set
            {
                _sz = ValidateStringRange(value);
                OnPropertyChanged(nameof(Sz));
            }
        }
        /// <summary> Коэффициент, учитывающий порядок разработки пластов и интервал времени между их отработкой </summary>
        public string Kt 
        {
            get => _kt;
            set
            {
                _kt = ValidateStringRange(value);
                OnPropertyChanged(nameof(Kt));
            }
        }
        //----------------------------------------------------------------------
        // Дополнительные парметры пласта
        //----------------------------------------------------------------------
        /// <summary> Глубина ведения горных работ в пределах от 350 до 1000 м ( число ) </summary>
        public float Height { get => TryParse(_h, out float h) ? h : 0; }
        /// <summary> Ширина предохранительного приразломного целика на уровне данного пласта со стороны лежачего крыла разломной зоны, м </summary>
        public float Bl { get; set; }
        /// <summary> Ширина предохранительного приразломного целика на уровне данного пласта со стороны висячего крыла разломной зоны, м </summary>
        public float Bv { get; set; }

        #endregion

        #region Private Methods

        /// <summary> Проверка строки на схожесть с вещественным числом </summary>
        /// <param name="str"> Исходная строка </param>
        /// <returns> Исходная строка, если она схожа с числом, "" в противном случае </returns>
        private string ValidateString(string str) =>
            str != string.Empty ? TryParse((str = StringIsNumber(str)) + (str[str.Length - 1] == ',' ? "0" : ""), out float _) ? str : "" : "";

        #endregion

        #region Public Methods

        /// <summary> Расчет высоты ЗВТ одиночного пласта </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        public float Ht()
        {
            _ = TryParse(H, out float h);
            _ = TryParse(Mv, out float mv);
            _ = TryParse(Ki, out float ki);
            _ = TryParse(K, out float k);
            return mv * ki * k * (-0.01F * h + (TypeDev == "камерная" ? 26 : 46));
        }
        /// <summary> Расчет коэффициента d, зависящего от системы разраьотки для пласта </summary>
        /// <returns> Значение коэффициента d</returns>
        public float D()
        {
            _ = TryParse(H, out float h);
            return -0.01F * h + (TypeDev == "камерная" ? 26 : 46);
        }
        /// <summary> Расчет приведенной вынимаемой мощности для пласта </summary>
        /// <returns> Значение приведенной вынимаеомй мощности </returns>
        public float MPr()
        {
            _ = TryParse(Mv, out float mv);
            _ = TryParse(Ki, out float ki);
            _ = TryParse(K, out float k);
            return mv * ki * k;
        }

        #endregion
    }
}
