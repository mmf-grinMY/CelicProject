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
            if(_model.GetType() == typeof(SCalcBViewModel))
                ReadSimpleB();
            else if (_model.GetType() == typeof(SCalcCViewModel))
                ReadSimpleC();
            else if (_model.GetType() == typeof(SCalcDViewModel))
                ReadSimpleD();
        }

        #endregion

        #region Private Methods

        /// <summary> Чтение данных об одиночном пласте </summary>
        /// <param name="plastNode"> Корневой узел рассматриваемого пласта </param>
        /// <returns> Пласт с данными из рассматриваемого узла </returns>
        private Plast ReadSimplePlast(XmlElement plastNode)
        {
            Plast plast = new Plast();
            foreach (XmlNode childnode in plastNode.ChildNodes)
                if (childnode.Name == "minefield")
                    foreach (XmlNode minefield in childnode.ChildNodes)
                        if (minefield.Name == "h")
                            plast.H = minefield.InnerText;
                        else if (minefield.Name == "mv")
                            plast.Mv = minefield.InnerText;
                        else if (minefield.Name == "ki")
                            plast.Ki = minefield.InnerText;
                        else if (minefield.Name == "typeDev")
                            plast.TypeDev = minefield.InnerText;
                else if (childnode.Name == "S")
                {
                    _ = double.TryParse(childnode.InnerText, out double s);
                    plast.S = s;
                }
                else if (childnode.Name == "S_z")
                {
                    _ = double.TryParse(childnode.InnerText, out double s_z);
                    plast.Sz = s_z;
                }
                else if (childnode.Name == "k_t")
                {
                    _ = double.TryParse(childnode.InnerText, out double k_t);
                    plast.Kt = k_t;
                }
            return plast;
        }
        /// <summary> Чтение данных о модели расчета SCalcBViewModel </summary>
        private void ReadSimpleB()
        {
            XmlDocument doc = new XmlDocument();
            if (File.Exists(_path))
            {
                doc.Load(_path);
                XmlElement sysDev = doc.DocumentElement;
                if (sysDev != null && (sysDev.Attributes.GetNamedItem("type")?.Value == "simpleB")
                    || (sysDev.Attributes.GetNamedItem("type") == null))
                    foreach (XmlElement plastNode in sysDev)
                        (_model as SCalcBViewModel).Plasts.Add(ReadSimplePlast(plastNode));
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
                            (_model as SCalcCViewModel).Alfa = plastNode.InnerText;
                        else
                            (_model as SCalcCViewModel).Plasts.Add(ReadSimplePlast(plastNode));
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
                                (_model as SCalcDViewModel).LeftPlasts.Add(ReadSimplePlast(plastNode));
                        else if (type.Name == "right")
                            foreach (XmlElement plastNode in type)
                                (_model as SCalcDViewModel).RightPlasts.Add(ReadSimplePlast(plastNode));
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
