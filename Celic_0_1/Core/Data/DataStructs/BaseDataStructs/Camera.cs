using static System.Math;

namespace Celic
{
    /// <summary> Шахтное поле с камерной системой разработки </summary>
    public class Camera : MineField
    {
        #region Private Fields

        /// <summary> Сумма поперечных сечений выработок, составляющих очистную камеру ( поле ) </summary>
        private float _si;
        /// <summary> Расстояние между соседними осями междукамерных целиков ( поле ) </summary>
        private float _l;
        /// <summary> Коэффициент извлечения рудной массы в пределах вынимаемой мощности ( поле ) </summary>
        private float _ki;

        #endregion

        #region Public Properties

        /// <summary> Сумма поперечных сечений выработок, составляющих очистную камеру </summary>
        public float Si
        {
            get => _si;
            set
            {
                _si = value;
                OnPropertyChanged("Si");
            }
        }
        /// <summary> Расстояние между соседними осями междукамерных целиков </summary>
        public float L
        {
            get => _l;
            set
            {
                _l = value;
                OnPropertyChanged("L");
            }
        }
        /// <summary> Коэффициент извлечения рудной массы в пределах вынимаемой мощности </summary>
        public float Ki
        {
            get => _ki;
            set
            {
                _ki = value;
                OnPropertyChanged("Ki");
            }
        }

        #endregion

        #region Contructros

        /// <summary> Основной конструктор дял данного класса </summary>
        public Camera() : base() => Si = Ki = L = 0;

        #endregion
    }

    /// <summary> Класс, обслуживающий стуктуру данных Camera </summary>
    class CameraManager : IMineFieldManage
    {
        #region Private Fields

        /// <summary> Шахтное поле с камерной системой разработки ( поле ) </summary>
        private readonly Camera _camera;

        #endregion

        #region Public Methods

        /// <summary> Расчет высоты зоны водопроводящих целиков </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        public float Ht() => MPr() * CalcD();
        /// <summary> Расчет значения коэффицента K при камерной системе разработки </summary>
        /// <returns> Значение коэффицента K при камерной системе разработки </returns>
        public float RecalcK()
        {
            float d0 = (26 - 0.01F * _camera.H) * _camera.Mv * _camera.Ki;
            return _camera.K = _camera.D >= 1.4F * d0 ? 1 : (float)Sqrt(0.8 * (_camera.D / (d0 - 0.2)));
        }
        /// <summary> Расчет значения коэффициента Ki </summary>
        /// <returns> Значение коэффициента Ki </returns>
        public float CalcKi() => _camera.Ki = _camera.L != 0 && _camera.Mv != 0 ? _camera.Si / (_camera.L * _camera.Mv) : 0;
        /// <summary> Расчет параметра, зависящего от системы разработки при камерной системы разработки </summary>
        /// <returns> Значение параметра, зависящего от системы разработки </returns>
        public float CalcD() => 26 - 0.01F * _camera.H;
        /// <summary> Расчет приведенной вынимаемой мощности при камерной системе разработки </summary>
        /// <returns> Значение приведенной вынимаемой мощности </returns>
        public float MPr() => _camera.Mv * _camera.Ki * _camera.K;

        #endregion

        #region Public Static Methods

        /// <summary> Расчет высоты зоны водопроводящих целиков </summary>
        /// <returns> Значение высоты ЗВТ </returns>
        public static float Ht(Camera camera)
        {
            CameraManager manager = new CameraManager(camera);
            return manager.MPr() * manager.CalcD();
        }

        #endregion

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        /// <param name="camera"> Рассматриваемое шахтное поле с камерной системой разработки </param>
        public CameraManager(Camera camera) => _camera = camera; 

        #endregion
    }
}