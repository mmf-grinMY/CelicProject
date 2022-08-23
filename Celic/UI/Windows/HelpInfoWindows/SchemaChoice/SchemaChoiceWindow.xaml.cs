using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Celic
{
	/// <summary> Логика взаимодействия для SchemaChoiceWindow.xaml </summary>
    public partial class SchemaChoiceWindow : Window
    {
        /// <summary> Основной конструктор для данного класса </summary>
        /// <param name="vm"> Вызывающая модель </param>
        public SchemaChoiceWindow(SchemaChoiceViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }
    }
}