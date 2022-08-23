/*
 * Created by SharpDevelop.
 * User: Максим
 * Date: 08/20/2022
 * Time: 20:32
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
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
            if(_model.GetType() == typeof(SCalcBViewModel))
                this.WriteSimpleB();
            else if (_model.GetType() == typeof(SCalcCViewModel))
                this.WriteSimpleC();
            else if (_model.GetType() == typeof(SCalcDViewModel))
                this.WriteSimpleD();
        }

        #endregion

        #region Private Methods

        private XmlElement WriteMineFieldData(XmlDocument doc, Plast plast)
        {
            // Создание главного узла шахтного поля ( <minefield></minefield> )
            XmlElement minefiledElem = doc.CreateElement("minefield");
            // Создание тегов-полей для шахтного поля
            XmlElement hElem = doc.CreateElement("h");
            XmlElement mvElem = doc.CreateElement("mv");
            XmlElement kiElem = doc.CreateElement("ki");
            XmlElement typeDevElem = doc.CreateElement("typeDev");
            // Создание данных для тегов-полей шахтного поля
            XmlText hText = doc.CreateTextNode(plast.Main.H.ToString());
            XmlText mvText = doc.CreateTextNode(plast.Main.Mv.ToString());
            XmlText kiText = doc.CreateTextNode((plast.Main as Camera).Ki.ToString());
            XmlText typeDevText = doc.CreateTextNode(plast.TypeDev);
            // Запись данных в поля-теги шахтного поля
            hElem.AppendChild(hText);
            mvElem.AppendChild(mvText);
            kiElem.AppendChild(kiText);
            typeDevElem.AppendChild(typeDevText);
            // Добавление тегов-полей в шахтное поле
            minefiledElem.AppendChild(hElem);
            minefiledElem.AppendChild(mvElem);
            minefiledElem.AppendChild(kiElem);
            minefiledElem.AppendChild(typeDevElem);
            return minefiledElem;
        }
        /// <summary> Запись данных о коллекции пластов в выбранный файл </summary>
        /// <param name="plasts"> Записываемая коллекция разрабатываемых пластов </param>
        /// <param name="doc"> Файл для записи </param>
        /// <param name="main"> Главный узел, в который добавляется информация о коллекции</param>
        private void WritePlastCollection(ObservableCollection<Plast> plasts, XmlDocument doc, XmlElement main)
        {
            foreach (Plast plast in plasts)
            {
                // Создание главного узла пласта ( <plast></plast> )
                XmlElement plastElem = doc.CreateElement("plast");
                // Создание дополнительный тегов-полей для пласта
                XmlElement SElem = doc.CreateElement("S");
                XmlElement SDzettaElem = doc.CreateElement("S_z");
                XmlElement KTElem = doc.CreateElement("k_t");
                // Создание данных для дополнительных тегов полей пласта
                XmlText SText = doc.CreateTextNode(plast.S.ToString());
                XmlText SDzettaText = doc.CreateTextNode(plast.Sz.ToString());
                XmlText KTText = doc.CreateTextNode(plast.Kt.ToString());
                // Запись данных в дополнительные теги-поля пласта 
                SElem.AppendChild(SText);
                SDzettaElem.AppendChild(SDzettaText);
                KTElem.AppendChild(KTText);
                // Добавление тегов-полей в пласт
                plastElem.AppendChild(WriteMineFieldData(doc, plast));
                plastElem.AppendChild(SElem);
                plastElem.AppendChild(SDzettaElem);
                plastElem.AppendChild(KTElem);
                //Добавление пласта в корневой узел
                main.AppendChild(plastElem);
            }
        }
        /// <summary> Запись в файл данных о моделе SCalcBViewModel </summary>
        private void WriteSimpleB()
        {
            XmlDocument doc = new XmlDocument();
            if (!File.Exists(_path))
            {
                string text = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\n<sysDev type=\"simpleB\">\n</sysDev>";
                byte[] arr = Encoding.ASCII.GetBytes(text);
                FileStream file = File.Create(_path);
                file.Write(arr, 0, arr.Length);
                file.Close();
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
            doc.Load(_path);
            WritePlastCollection((_model as SCalcBViewModel).Plasts, doc, doc.DocumentElement);
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
            XmlText angleText = doc.CreateTextNode((_model as SCalcCViewModel).Alfa.ToString());
            angleElem.AppendChild(angleText);
            sysDev.AppendChild(angleElem);
            // Запись информации о разрабатываемой коллекции пластов
            WritePlastCollection((_model as SCalcCViewModel).Plasts, doc, sysDev);
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
            WritePlastCollection((_model as SCalcDViewModel).LeftPlasts, doc, left);
            sysDev.AppendChild(right);
            WritePlastCollection((_model as SCalcDViewModel).RightPlasts, doc, right);
            doc.Save(_path);
        }

        #endregion
    }
}
