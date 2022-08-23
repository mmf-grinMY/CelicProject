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
    /// Логика взаимодействия для SimpleCalcDWindow.xaml
    /// </summary>
    public partial class SCalcDWindow : Window
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainWindow"></param>
        public SCalcDWindow(MainWindow mainWindow)
        {
            InitializeComponent();
            DataContext = new SCalcDViewModel(mainWindow, this);
            Closing += (DataContext as SCalcDViewModel).Close;
        }
    }
}