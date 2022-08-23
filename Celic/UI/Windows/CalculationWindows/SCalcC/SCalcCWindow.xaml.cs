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
    /// Логика взаимодействия для SimpleCalcCWindow.xaml
    /// </summary>
    public partial class SCalcCWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainWindow"></param>
        public SCalcCWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = new SCalcCViewModel(mainWindow, this);
            Closing += (DataContext as SCalcCViewModel).Close;
        }
    }
}