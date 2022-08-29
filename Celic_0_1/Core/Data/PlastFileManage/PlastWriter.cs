using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Xml;

namespace Celic
{
    /// <summary> Запись данных в файл </summary>
    class PlastWriter
    {
        #region Private Fields

        /// <summary> Абсолютный путь к файлу с данными ( по умолчанию равен "" ) </summary>
        private readonly string _path = "";
        /// <summary> Модель расчета ( по умолчанию null ) </summary>
        private readonly ListPlastViewModel _model = null;

        #endregion

        #region Constructors

        /// <summary> Конструктор класса с расположением файла по умолчанию </summary>
        /// <param name="model"> Модель расчета </param>
        public PlastWriter(ListPlastViewModel model)
        {
            _path = "D:/plast.plast";
            _model = model;
        }
        /// <summary> Конструктор класса с задаваемым расположением файла для записи </summary>
        /// <param name="path"> Абсолютный путь файла для записи </param>
        /// <param name="model"> Модель расчета </param>
        public PlastWriter(string path, ListPlastViewModel model)
        {
            _path = path;
            _model = model;
        }

        #endregion

        #region Public Methods

        /// <summary> Запись данных о системе разрабатываемых пластов в выбранный файл </summary>
        public void Write()
        {
            if(_model.GetType() == typeof(CalculationBViewModel))
                this.WriteSimpleB();
            else if (_model.GetType() == typeof(CalculationCViewModel))
                this.WriteSimpleC();
            else if (_model.GetType() == typeof(CalculationDViewModel))
                this.WriteSimpleD();
        }

        #endregion

        #region Private Methods

        private string ToString(float data) => data == -1 ? "undefine" : data.ToString();

        private XmlElement WriteMineFieldData(XmlDocument doc, Plast plast)
        {
            XmlElement plastNode = doc.CreateElement("plast");
            XmlNode minefields = plastNode.AppendChild(doc.CreateElement("minefields"));
            foreach(MineField mine in plast.MineFields)
            {
                XmlElement root;
                if (mine is Lava lava)
                {
                    root = doc.CreateElement("lava");
                    root.AppendChild(doc.CreateElement("H")).AppendChild(doc.CreateTextNode(ToString(lava.H)));
                    root.AppendChild(doc.CreateElement("Mv")).AppendChild(doc.CreateTextNode(ToString(lava.Mv)));
                    root.AppendChild(doc.CreateElement("Ki")).AppendChild(doc.CreateTextNode(ToString(lava.Ki)));
                    root.AppendChild(doc.CreateElement("K")).AppendChild(doc.CreateTextNode(ToString(lava.K)));
                    root.AppendChild(doc.CreateElement("D")).AppendChild(doc.CreateTextNode(ToString(lava.D)));
                    root.AppendChild(doc.CreateElement("L")).AppendChild(doc.CreateTextNode(ToString(lava.L)));
                    root.AppendChild(doc.CreateElement("B")).AppendChild(doc.CreateTextNode(ToString(lava.B)));
                    root.AppendChild(doc.CreateElement("Sl")).AppendChild(doc.CreateTextNode(ToString(lava.Sl)));
                    root.Attributes.Append(doc.CreateAttribute("status")).AppendChild(doc.CreateTextNode(mine == plast.Main ? "main" : "other"));
                }
                else
                {
                    Camera camera = mine as Camera;
                    root = doc.CreateElement("camera");
                    root.AppendChild(doc.CreateElement("H")).AppendChild(doc.CreateTextNode(ToString(camera.H)));
                    root.AppendChild(doc.CreateElement("Mv")).AppendChild(doc.CreateTextNode(ToString(camera.Mv)));
                    root.AppendChild(doc.CreateElement("Ki")).AppendChild(doc.CreateTextNode(ToString(camera.Ki)));
                    root.AppendChild(doc.CreateElement("K")).AppendChild(doc.CreateTextNode(ToString(camera.K)));
                    root.AppendChild(doc.CreateElement("D")).AppendChild(doc.CreateTextNode(ToString(camera.D)));
                    root.AppendChild(doc.CreateElement("L")).AppendChild(doc.CreateTextNode(ToString(camera.L)));
                    root.AppendChild(doc.CreateElement("Si")).AppendChild(doc.CreateTextNode(ToString(camera.Si)));
                    root.Attributes.Append(doc.CreateAttribute("status")).AppendChild(doc.CreateTextNode(mine == plast.Main ? "main" : "other"));
                }
                minefields.AppendChild(root);
            }
            return plastNode;
        }
        /// <summary> Запись данных о коллекции пластов в выбранный файл </summary>
        /// <param name="plasts"> Записываемая коллекция разрабатываемых пластов </param>
        /// <param name="doc"> Файл для записи </param>
        /// <param name="main"> Главный узел, в который добавляется информация о коллекции</param>
        private void WritePlastCollection(ObservableCollection<Plast> plasts, XmlDocument doc, XmlElement main)
        {
            foreach (Plast plast in plasts)
            {
                XmlNode root = WriteMineFieldData(doc, plast);
                root.AppendChild(doc.CreateElement("S")).AppendChild(doc.CreateTextNode(ToString(plast.S)));
                root.AppendChild(doc.CreateElement("Sz")).AppendChild(doc.CreateTextNode(ToString(plast.Sz)));
                root.AppendChild(doc.CreateElement("Kt")).AppendChild(doc.CreateTextNode(ToString(plast.Kt)));
                root.AppendChild(doc.CreateElement("T")).AppendChild(doc.CreateTextNode(ToString(plast.T)));
                root.AppendChild(doc.CreateElement("Hf")).AppendChild(doc.CreateTextNode(ToString(plast.Hf)));
                root.AppendChild(doc.CreateElement("Lp")).AppendChild(doc.CreateTextNode(ToString(plast.Lp)));
                root.AppendChild(doc.CreateElement("Gorizont")).AppendChild(doc.CreateTextNode(GorizontManager.ToString(plast.Gorizont)));
                root.AppendChild(doc.CreateElement("Id")).AppendChild(doc.CreateTextNode(ToString(plast.Id)));
                root.AppendChild(doc.CreateElement("Top")).AppendChild(doc.CreateTextNode(plast.Top));
                root.AppendChild(doc.CreateElement("Buttom")).AppendChild(doc.CreateTextNode(plast.Buttom));
                root.AppendChild(doc.CreateElement("Name")).AppendChild(doc.CreateTextNode(plast.Name));
                main.AppendChild(root);
            }
        }
        /// <summary> Запись в файл данных о моделе SCalcBViewModel </summary>
        private void WriteSimpleB()
        {
            XmlDocument doc = new XmlDocument();
            if (!File.Exists(_path))
            {
                string text = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<sysDev type=\"B\">\n</sysDev>";
                byte[] arr = Encoding.ASCII.GetBytes(text);
                FileStream file = File.Create(_path);
                file.Write(arr, 0, arr.Length);
                file.Close();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            doc.Load(_path);
            WritePlastCollection((_model as CalculationBViewModel).Plasts, doc, doc.DocumentElement);
            doc.Save(_path);
        }
        /// <summary> Запись в файл данных о моделе SCalcCViewModel </summary>
        private void WriteSimpleC()
        {
            XmlDocument doc = new XmlDocument();
            if (!File.Exists(_path))
            {
                string text = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<sysDev type=\"simpleC\">\n</sysDev>";
                byte[] arr = Encoding.ASCII.GetBytes(text);
                FileStream file = File.Create(_path);
                file.Write(arr, 0, arr.Length);
                file.Close();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            doc.Load(_path);
            XmlElement sysDev = doc.DocumentElement;
            // Запись данных об угле поворота разломной зоны
            XmlElement angleElem = doc.CreateElement("angle");
            XmlText angleText = doc.CreateTextNode((_model as CalculationCViewModel).Alfa.ToString());
            angleElem.AppendChild(angleText);
            sysDev.AppendChild(angleElem);
            // Запись информации о разрабатываемой коллекции пластов
            WritePlastCollection((_model as CalculationCViewModel).Plasts, doc, sysDev);
            doc.Save(_path);
        }
        /// <summary> Запись в файл данных о моделе SCalcDViewModel </summary>
        private void WriteSimpleD()
        {
            XmlDocument doc = new XmlDocument();
            if (!File.Exists(_path))
            {
                string text = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<sysDev type=\"simpleD\">\n</sysDev>";
                byte[] arr = Encoding.ASCII.GetBytes(text);
                FileStream file = File.Create(_path);
                file.Write(arr, 0, arr.Length);
                file.Close();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            doc.Load(_path);
            XmlElement sysDev = doc.DocumentElement;
            XmlElement left = doc.CreateElement("left");
            XmlElement right = doc.CreateElement("right");
            sysDev.AppendChild(left);
            WritePlastCollection((_model as CalculationDViewModel).LeftPlasts, doc, left);
            sysDev.AppendChild(right);
            WritePlastCollection((_model as CalculationDViewModel).RightPlasts, doc, right);
            doc.Save(_path);
        }

        #endregion
    }
}
