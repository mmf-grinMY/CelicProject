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
    /// Логика взаимодействия для CalcBWindow.xaml
    /// </summary>
    public partial class SCalcBWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainWindow"></param>
        public SCalcBWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = new SCalcBViewModel(mainWindow, this);
            this.Closing += (DataContext as SCalcBViewModel).Close;
        }
    }
}