using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace Celic.WPF.UserControls
{
    /// <summary> Логика взаимодействия для EPlastDataBox.xaml </summary>
    public partial class PlastDataBox : UserControl
    {
        #region Contructors

        /// <summary> Основной конструктор для данного класса </summary>
        public PlastDataBox()
        {
            InitializeComponent();
        }

        #endregion

        #region EventHandlers

        /// <summary> Обработчик события "Изменение системы разработки пласта" </summary>
        /// <param name="sender"> Вызываемый ComboBox </param>
        /// <param name="e"> Аргументы вызова </param>
        private void ComboBoxTypeDev_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox && DataContext != null)
            {

            }
        }

        #endregion

    }
}