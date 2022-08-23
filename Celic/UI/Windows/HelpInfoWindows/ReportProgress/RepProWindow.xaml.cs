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
	/// <summary>
    /// Логика взаимодействия для RepProWindow.xaml
    /// </summary>
    partial class RepProWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        public RepProWindow(ListPlastViewModel model)
        {
            InitializeComponent();
            DataContext = new RepProViewModel(this, model);
        }
    }
}