using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Celic
{
    /// <summary>
    /// Логика взаимодействия для PlastListBox.xaml
    /// </summary>
    public partial class PlastListBox : UserControl
    {
        public PlastListBox()
        {
            InitializeComponent();
        }
        public readonly static DependencyProperty ItemSourceProperty =
            DependencyProperty.Register("ItemSource", typeof(ObservableCollection<Plast>), typeof(PlastListBox),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnItemSourceChanged)));
        public ObservableCollection<Plast> ItemSource
        {
            get { return (ObservableCollection<Plast>)GetValue(ItemSourceProperty); }
            set { SetValue(ItemSourceProperty, value); }
        }
        private static void OnItemSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PlastListBox box)
                box.adding.CommandParameter = box.ItemSource;
        }
        public readonly static DependencyProperty SelectedItemProperty =
            DependencyProperty.Register("SelectedItem", typeof(Plast), typeof(PlastListBox));
        public Plast SelectedItem
        {
            get { return (Plast)GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
    }
}
