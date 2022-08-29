using System.Xml;
using System.IO;
using System.Windows;

namespace Celic
{
    /// <summary> Чтение данных о системе разработки из файла </summary>
    class PlastReader
    {
        #region Private Fields

        /// <summary> Абсолютный путь к файлу с данными ( по умолчанию равен "" ) </summary>
        private readonly string _path = "";
        /// <summary> Модель расчета ( по умолчанию null ) </summary>
        private readonly ListPlastViewModel _model = null;

        #endregion

        #region Constructors

        /// <summary> Главный конструктор </summary>
        /// <param name="path"> Абсолютный путь к файлу с данными </param>
        /// <param name="model"> Модель проводимого расчета </param>
        public PlastReader(string path, ListPlastViewModel model)
        {
            _path = path;
            _model = model;
        }

        #endregion

        #region Public Methods

        /// <summary> Чтение данных из файла с добавлением данных в модель расчета </summary>
        public void Read()
        {
            if(_model.GetType() == typeof(CalculationBViewModel))
                ReadDataB();
            else if (_model.GetType() == typeof(CalculationCViewModel))
                ReadSimpleC();
            else if (_model.GetType() == typeof(CalculationDViewModel))
                ReadSimpleD();
        }

        #endregion

        #region Private Methods

        private float Parse(string data) => data.Equals("undefine") ? -1 : float.Parse(data);

        private void ReadData(XmlNode minefields, Plast plast)
        {
            foreach (XmlNode minefield in minefields.ChildNodes)
            {
                if (minefield.Name == "lava")
                {
                    Lava lava = new Lava();
                    foreach (XmlNode coef in minefield.ChildNodes)
                        switch (coef.Name)
                        {
                            case "H": lava.H = Parse(coef.InnerText); break;
                            case "Mv": lava.Mv = Parse(coef.InnerText); break;
                            case "Ki": lava.Ki = Parse(coef.InnerText); break;
                            case "K": lava.K = Parse(coef.InnerText); break;
                            case "D": lava.D = Parse(coef.InnerText); break;
                            case "L": lava.L = Parse(coef.InnerText); break;
                            case "B": lava.B = Parse(coef.InnerText); break;
                            case "Sl": lava.Sl = Parse(coef.InnerText); break;
                        }
                    if (minefield.Attributes.GetNamedItem("status")?.Value == "main")
                        plast.Main = lava;
                    plast.MineFields.Add(lava);
                }
                else
                {
                    Camera camera = new Camera();
                    foreach (XmlNode coef in minefield.ChildNodes)
                        switch (coef.Name)
                        {
                            case "H": camera.H = Parse(coef.InnerText); break;
                            case "Mv": camera.Mv = Parse(coef.InnerText); break;
                            case "Ki": camera.Ki = Parse(coef.InnerText); break;
                            case "K": camera.K = Parse(coef.InnerText); break;
                            case "D": camera.D = Parse(coef.InnerText); break;
                            case "L": camera.L = Parse(coef.InnerText); break;
                            case "Si": camera.Si = Parse(coef.InnerText); break;
                        }
                    if (minefield.Attributes.GetNamedItem("status")?.Value == "main")
                        plast.Main = camera;
                    plast.MineFields.Add(camera);
                }
            }
        }

        /// <summary> Чтение данных об одиночном пласте </summary>
        /// <param name="plastNode"> Корневой узел рассматриваемого пласта </param>
        /// <returns> Пласт с данными из рассматриваемого узла </returns>
        private Plast ReadPlastInfo(XmlElement plastNode)
        {
            Plast plast = new Plast();
            foreach (XmlNode plastCoef in plastNode.ChildNodes)
                switch (plastCoef.Name)
                {
                    case "minefields": ReadData(plastCoef, plast); break;
                    case "S": plast.S = Parse(plastCoef.InnerText); break;
                    case "Sz": plast.Sz = Parse(plastCoef.InnerText); break;
                    case "Kt": plast.Kt = Parse(plastCoef.InnerText); break;
                    case "T": plast.T = (int)Parse(plastCoef.InnerText); break;
                    case "Hf": plast.Hf = Parse(plastCoef.InnerText); break;
                    case "Lp": plast.Lp = Parse(plastCoef.InnerText); break;
                    case "Gorizont": plast.Gorizont = GorizontManager.ToGorizont(plastCoef.InnerText); break;
                    case "Id": plast.Id = (int)Parse(plastCoef.InnerText); break;
                    case "Top": plast.Top = plastCoef.InnerText; break;
                    case "Buttom": plast.Buttom = plastCoef.InnerText; break;
                    case "Name": plast.Name = plastCoef.InnerText; break;
                }
            return plast;
        }
        /// <summary> Чтение данных о модели расчета SCalcBViewModel </summary>
        private void ReadDataB()
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists(_path))
            {
                doc.Load(_path);
                XmlElement sysDev = doc.DocumentElement;
                if (sysDev != null && (sysDev.Attributes.GetNamedItem("type")?.Value == "simpleB")
                    || (sysDev.Attributes.GetNamedItem("type") == null))
                    foreach (XmlElement plastNode in sysDev)
                        (_model as CalculationBViewModel).Plasts.Add(ReadPlastInfo(plastNode));
                else
                    MessageBox.Show("Данные в файле некорректны для данного типа расчета");
            }
        }
        /// <summary> Чтение данных о модели расчета SCalcCViewModel </summary>
        private void ReadSimpleC()
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists(_path))
            {
                doc.Load(_path);
                XmlElement sysDev = doc.DocumentElement;
                if (sysDev != null && (sysDev.Attributes.GetNamedItem("type")?.Value == "simpleC"))
                    foreach (XmlElement plastNode in sysDev)
                        if (plastNode.Name == "angle")
                            (_model as CalculationCViewModel).Alfa = float.Parse(plastNode.InnerText);
                        else
                            (_model as CalculationCViewModel).Plasts.Add(ReadPlastInfo(plastNode));
                else
                    MessageBox.Show("Данные в файле некорректны для данного типа расчета");
            }
        }
        /// <summary> Чтение данных о модели расчета SCalcDViewModel </summary>
        private void ReadSimpleD()
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists(_path))
            {
                doc.Load(_path);
                XmlElement sysDev = doc.DocumentElement;
                if (sysDev != null && (sysDev.Attributes.GetNamedItem("type")?.Value == "simpleD"))
                    foreach(XmlElement type in sysDev.ChildNodes)
                        if(type.Name == "left")
                            foreach (XmlElement plastNode in type)
                                (_model as CalculationDViewModel).LeftPlasts.Add(ReadPlastInfo(plastNode));
                        else if (type.Name == "right")
                            foreach (XmlElement plastNode in type)
                                (_model as CalculationDViewModel).RightPlasts.Add(ReadPlastInfo(plastNode));
                        else
                            ReadFailed();
                else
                    MessageBox.Show("Данные в файле некорректны для данного типа расчета");
            }
        }
        /// <summary> Уведомление пользователя о некоррекности данных в файле и невозможности произвести операцию чтения данных из файла </summary>
        public static void ReadFailed() => MessageBox.Show("К сожалению, программе не удалось прочитать данные из файла." +
            " Возможно они повреждены, или некорректно заданы!");

        #endregion
    }
}
