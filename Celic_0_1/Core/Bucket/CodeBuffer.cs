namespace Celic
{
    class CodeBuffer
    {
        #region To Do List

        #endregion

        #region SCalcCPlast*
        /* Описание семейства файлов SCalcCPlast*
           Группа файлов предназначалась для одновременного ввода двух списков пластов ( по 3 максимум в каждом за каждую итерацию ввода )
           Заменен на группу файлов AddPlast* */
        /*SCalcCPlastViewModel
        class SimpleCalcCPlastViewModel
        {
        SimpleCalcCPlastWindow plastWindow;
        private Plast inputPlast1;
        public Plast InputPlast1
        {
            set
            {
                inputPlast1 = value;
                OnPropertyChanged("InputPlast1");
            }
            get
            {
                return inputPlast1;
            }
        }
        private Plast inputPlast2;
        public Plast InputPlast2
        {
            set
            {
                inputPlast2 = value;
                OnPropertyChanged("InputPlast2");
            }
            get
            {
                return inputPlast2;
            }
        }
        private Plast inputPlast3;
        public Plast InputPlast3
        {
            set
            {
                inputPlast3 = value;
                OnPropertyChanged("InputPlast3");
            }
            get
            {
                return inputPlast3;
            }
        }
        private Plast inputPlast4;
        public Plast InputPlast4
        {
            set
            {
                inputPlast4 = value;
                OnPropertyChanged("InputPlast4");
            }
            get
            {
                return inputPlast4;
            }
        }
        private Plast inputPlast5;
        public Plast InputPlast5
        {
            set
            {
                inputPlast5 = value;
                OnPropertyChanged("InputPlast5");
            }
            get
            {
                return inputPlast5;
            }
        }
        private Plast inputPlast6;
        public Plast InputPlast6
        {
            set
            {
                inputPlast6 = value;
                OnPropertyChanged("InputPlast6");
            }
            get
            {
                return inputPlast6;
            }
        }
        ObservableCollection<Plast> Plasts;
        public SimpleCalcCPlastViewModel(ObservableCollection<Plast> plasts, SimpleCalcCPlastWindow plastWindow)
        {
            Plasts = plasts;
            this.plastWindow = plastWindow;
            Plasts.Add(inputPlast1 = new Plast());
            Plasts.Add(inputPlast2 = new Plast());
        }
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Обработчик для события Propertychanged
        /// </summary>
        /// <param name="prop"></param>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        private RelayCommand exitWithSaveCommand;
        public RelayCommand ExitWithSaveCommand
        {
            get
            {
                return exitWithSaveCommand ?? 
                (exitWithSaveCommand = new RelayCommand(obj =>
                {
                    plastWindow.Close();
                }));
            }
        }
        private RelayCommand exitWithoutSaveCommand;
        public RelayCommand ExitWithoutSaveCommand
        {
            get
            {
                return exitWithoutSaveCommand ??
                (exitWithoutSaveCommand = new RelayCommand(obj =>
                {
                    Plasts.Remove(inputPlast1);
                    Plasts.Remove(inputPlast2);
                    Plasts.Remove(inputPlast3);
                    Plasts.Remove(inputPlast4);
                    Plasts.Remove(inputPlast5);
                    Plasts.Remove(inputPlast6);
                    plastWindow.Close();
                }));
            }
        }
        private RelayCommand addPlastCommand;
        public RelayCommand AddPlastCommand
        {
            get
            {
                return addPlastCommand ??
                (addPlastCommand = new RelayCommand(obj =>
                {
                    plastWindow.Plast.IsEnabled = true;
                    Plast plast1 = new Plast();
                    Plasts.Add(plast1);
                    InputPlast3 = plast1;
                    Plast plast2 = new Plast();
                    Plasts.Add(plast2);
                    InputPlast3 = plast2;
                    Console.WriteLine(inputPlast3 == Plasts[Plasts.Count - 1]);
                    Plasts.Add(inputPlast4 = new Plast());
                    Console.WriteLine(inputPlast4 == Plasts[Plasts.Count - 1]);
                    inputPlast3.IsContiguos = InputPlast1.GetHashCode();
                    inputPlast4.IsContiguos = InputPlast2.GetHashCode();
                }));
            }
        }
        private RelayCommand addPlastCommand1;
        public RelayCommand AddPlastCommand1
        {
            get
            {
                return addPlastCommand1 ??
                (addPlastCommand1 = new RelayCommand(obj =>
                {
                    plastWindow.Plast1.IsEnabled = true;
                    Plasts.Add(InputPlast5 = new Plast());
                    Plasts.Add(InputPlast6 = new Plast());
                }));
            }
        }
    }*/
        /* SCalcCPlastWindow.xaml
         Title="SimpleCalcCPlastWindow" Height="410" Width="620">
    <Window.Resources>
        <Style TargetType="TextBox">
            <Setter Property="Width" Value="100"/>
            <Setter Property="Margin" Value="10,2"/>
        </Style>
        <Style TargetType="Label">
            <Setter Property="Width" Value="185"/>
        </Style>
        <Style TargetType="ComboBox">
            <Setter Property="Width" Value="100"/>
            <Setter Property="Margin" Value="10,2"/>
        </Style>
        <Style TargetType="TextBlock">
            <Setter Property="Width" Value="180"/>
            <Setter Property="Margin" Value="5,0,0,0"/>
        </Style>
    </Window.Resources>
    <Grid>
        <Grid.ColumnDefinitions>
            <ColumnDefinition />
            <ColumnDefinition />
        </Grid.ColumnDefinitions>
        <StackPanel Grid.ColumnSpan="2">
            <StackPanel Orientation="Horizontal">
            <Label Content="Для правильности расчета пласты стоит вводить парами !" Style="{x:Null}" FontSize="14" Width="400"/>
            <Button Content="Сохранить" Margin="10,5" Command="{Binding ExitWithSaveCommand}" Width="Auto"/>
            <Button Content="Отменить" Margin="10,5" Command="{Binding ExitWithoutSaveCommand}" Width="Auto"/>
        </StackPanel>
            <StackPanel Orientation="Horizontal">
            <StackPanel DataContext="{Binding InputPlast1}">
            <StackPanel Orientation="Horizontal">
                <Label Content="Система разработки"/>
                <ComboBox Text="{Binding TypeDev}">
                    <ComboBoxItem Content="столбовая"/>
                    <ComboBoxItem Content="камерная"/>
                </ComboBox>
            </StackPanel>
            <StackPanel Orientation="Horizontal">
                <Label Content="Глубина ведения горных работ"/>
                <TextBox Text="{Binding H, UpdateSourceTrigger=PropertyChanged}"/>
            </StackPanel>
            <StackPanel Orientation="Horizontal">
                <Label Content="Вынимаемая мощность"/>
                <TextBox Text="{Binding Mv, UpdateSourceTrigger=PropertyChanged}"/>
            </StackPanel>
            <StackPanel Orientation="Horizontal">
                <Label Content="Коэффициент извлечения "/>
                <TextBox Text="{Binding Ki, UpdateSourceTrigger=PropertyChanged}"/>
            </StackPanel>
        </StackPanel>
            <StackPanel Grid.Column="1" DataContext="{Binding InputPlast2}">
                <StackPanel Orientation="Horizontal">
                    <Label Content="Система разработки"/>
                    <ComboBox Text="{Binding TypeDev}">
                        <ComboBoxItem Content="столбовая"/>
                        <ComboBoxItem Content="камерная"/>
                    </ComboBox>
                </StackPanel>
                <StackPanel Orientation="Horizontal">
                    <Label Content="Глубина ведения горных работ"/>
                    <TextBox Text="{Binding H, UpdateSourceTrigger=PropertyChanged}"/>
                </StackPanel>
                <StackPanel Orientation="Horizontal">
                    <Label Content="Вынимаемая мощность"/>
                    <TextBox Text="{Binding Mv, UpdateSourceTrigger=PropertyChanged}"/>
                </StackPanel>
                <StackPanel Orientation="Horizontal">
                    <Label Content="Коэффициент извлечения "/>
                    <TextBox Text="{Binding Ki, UpdateSourceTrigger=PropertyChanged}"/>
                </StackPanel>
            </StackPanel>
        </StackPanel>
            <StackPanel>
                <CheckBox Content="Сближенный слой 1" Command="{Binding AddPlastCommand}"/>
                <StackPanel Orientation="Horizontal" IsEnabled="False" x:Name="Plast">
                    <StackPanel DataContext="{Binding InputPlast3}">
                        <StackPanel Orientation="Horizontal">
                            <Label Content="Система разработки"/>
                            <ComboBox Text="{Binding TypeDev}">
                                <ComboBoxItem Content="столбовая"/>
                                <ComboBoxItem Content="камерная"/>
                            </ComboBox>
                        </StackPanel>
                        <StackPanel Orientation="Horizontal">
                            <Label Content="Глубина ведения горных работ"/>
                            <TextBox Text="{Binding H, UpdateSourceTrigger=PropertyChanged}"/>
                        </StackPanel>
                        <StackPanel Orientation="Horizontal">
                            <Label Content="Вынимаемая мощность"/>
                            <TextBox Text="{Binding Mv, UpdateSourceTrigger=PropertyChanged}"/>
                        </StackPanel>
                        <StackPanel Orientation="Horizontal">
                            <Label Content="Коэффициент извлечения "/>
                            <TextBox Text="{Binding Ki, UpdateSourceTrigger=PropertyChanged}"/>
                        </StackPanel>
                    </StackPanel>
                    <StackPanel DataContext="{Binding InputPlast3}" Grid.Column="1">
                    <StackPanel Orientation="Horizontal">
                        <Label Content="Система разработки"/>
                        <ComboBox IsEditable="False" Text="{Binding TypeDev}">
                            <ComboBoxItem Content="столбовая"/>
                            <ComboBoxItem Content="камерная"/>
                        </ComboBox>
                    </StackPanel>
                    <StackPanel Orientation="Horizontal">
                        <Label Content="Глубина ведения горных работ"/>
                        <TextBox Text="{Binding H, UpdateSourceTrigger=PropertyChanged}"/>
                    </StackPanel>
                    <StackPanel Orientation="Horizontal">
                        <Label Content="Вынимаемая мощность"/>
                        <TextBox Text="{Binding Path=Mv, UpdateSourceTrigger=PropertyChanged}"/>
                    </StackPanel>
                    <StackPanel Orientation="Horizontal">
                        <Label Content="Коэффициент извлечения"/>
                        <TextBox Text="{Binding Path=Ki, UpdateSourceTrigger=PropertyChanged}"/>
                    </StackPanel>
                </StackPanel>
                </StackPanel>
            </StackPanel>
            <StackPanel>
            <CheckBox Content="Сближенный слой 2" Command="{Binding AddPlastCommand1}"/>
            <StackPanel Grid.ColumnSpan="2" Orientation="Horizontal" IsEnabled="False" x:Name="Plast1">
                <StackPanel DataContext="{Binding InputPlast5}">
                <StackPanel Orientation="Horizontal">
                    <Label Content="Система разработки"/>
                    <ComboBox IsEditable="False" Text="{Binding TypeDev}">
                        <ComboBoxItem Content="столбовая"/>
                        <ComboBoxItem Content="камерная"/>
                    </ComboBox>
                </StackPanel>
                <StackPanel Orientation="Horizontal">
                    <Label Content="Глубина ведения горных работ"/>
                    <TextBox Text="{Binding Path=H, UpdateSourceTrigger=PropertyChanged}"/>
                </StackPanel>
                <StackPanel Orientation="Horizontal">
                    <Label Content="Вынимаемая мощность"/>
                    <TextBox Text="{Binding Path=Mv, UpdateSourceTrigger=PropertyChanged}"/>
                </StackPanel>
                <StackPanel Orientation="Horizontal">
                    <Label Content="Коэффициент извлечения"/>
                    <TextBox Text="{Binding Path=Ki, UpdateSourceTrigger=PropertyChanged}"/>
                </StackPanel>
                </StackPanel>
                <StackPanel DataContext="{Binding InputPlast6}">
                    <StackPanel Orientation="Horizontal">
                        <Label Content="Система разработки"/>
                        <ComboBox IsEditable="False" Text="{Binding TypeDev}">
                            <ComboBoxItem Content="столбовая"/>
                            <ComboBoxItem Content="камерная"/>
                        </ComboBox>
                    </StackPanel>
                    <StackPanel Orientation="Horizontal">
                        <Label Content="Глубина ведения горных работ"/>
                        <TextBox Text="{Binding Path=H, UpdateSourceTrigger=PropertyChanged}"/>
                    </StackPanel>
                    <StackPanel Orientation="Horizontal">
                        <Label Content="Вынимаемая мощность"/>
                        <TextBox Text="{Binding Path=Mv, UpdateSourceTrigger=PropertyChanged}"/>
                    </StackPanel>
                    <StackPanel Orientation="Horizontal">
                        <Label Content="Коэффициент извлечения"/>
                        <TextBox Text="{Binding Path=Ki, UpdateSourceTrigger=PropertyChanged}"/>
                    </StackPanel>
                </StackPanel>
            </StackPanel>
        </StackPanel>
        </StackPanel>
    </Grid>*/
        /*SCalcCPlastWindow.xaml.cs
         Логика взаимодействия для SimpleCalcCPlastWindow.xaml
         public partial class SimpleCalcCPlastWindow : Window
        {
            public SimpleCalcCPlastWindow(ObservableCollection<Plast> plasts)
            {
                InitializeComponent();
                DataContext = new SimpleCalcCPlastViewModel(plasts, this);
            }
        }*/
        #endregion

        #region UserControls
        /*----------MyUserControl---------
         <Border BorderBrush="Black"
                    BorderThickness="0.2" Margin="0,2,0,0">
                <StackPanel Margin="4"
                            IsEnabled="False"
                            x:Name="Plast1"
                            DataContext="{Binding ContiguosPlast1}">
                    <StackPanel Orientation="Horizontal">
                    <Label Content="Система разработки"/>
                    <ComboBox IsEditable="False"
                              Text="{Binding TypeDev}">
                        <ComboBoxItem Content="столбовая"/>
                        <ComboBoxItem Content="камерная"/>
                    </ComboBox>
                </StackPanel>
                    <StackPanel Orientation="Horizontal">
                    <Label Content="Глубина ведения горных работ"/>
                    <TextBox Text="{Binding Path=H, UpdateSourceTrigger=PropertyChanged}"/>
                </StackPanel>
                    <StackPanel Orientation="Horizontal">
                    <Label Content="Вынимаемая мощность"/>
                    <TextBox Text="{Binding Path=Mv, UpdateSourceTrigger=PropertyChanged}"/>
                </StackPanel>
                    <StackPanel Orientation="Horizontal">
                    <Label Content="Коэффициент извлечения"/>
                    <TextBox Text="{Binding Path=Ki, UpdateSourceTrigger=PropertyChanged}"/>
                </StackPanel>
                </StackPanel>
            </Border>*/
        /*UserControl example 
         * from internet*/
        /*UserControl.xaml.cs
         public partial class MyUserControl : UserControl
        {
            UserControlViewModel _vm;

            public MyUserControl()
            {
                InitializeComponent();

                //internal viewModel set to the first child of MyUserControl
                rootDock.DataContext = new UserControlViewModel();

                _vm = (UserControlViewModel)rootDock.DataContext;

                //sets control to be able to use the viewmodel elements

            }

            #region Dependency properties 
            public string textSetFromApplication
            {
                get { return (string)GetValue(textSetFromApplicationProperty); }
                set { SetValue(textSetFromApplicationProperty, value); }
            }

            public static readonly DependencyProperty textSetFromApplicationProperty = DependencyProperty.Register("textSetFromApplication", typeof(string), typeof(MyUserControl), new PropertyMetadata(null, OnDependencyPropertyChanged));

            private static void OnDependencyPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((MyUserControl)d)._vm.SomethingInMyUserControlViewModel =
                     e.NewValue as string;
            }
            #endregion
        }*/
        /*UserControl.xaml
         <UserControl x:Class="Six_Barca_Main_Interface.MyUserControl"
             xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:local="clr-namespace:Six_Barca_Main_Interface"
             xmlns:System="clr-namespace:System;assembly=mscorlib" 
             mc:Ignorable="d" 
             d:DesignHeight="900" d:DesignWidth="900">

    <DockPanel  x:Name="rootDock" >
        <TextBlock>{Binding SomethingInMyUserControlViewModel}</TabControl>
    </DockPanel>
</UserControl>
        */
        #endregion

        #region Old Logic SCalculatorC
        /* Метод для расчета ширины приразломных предохранительных целиков 
        со стороны лежачего крыла разломной зоны для нижележащих пластов ( _bottomPlast )
        private void Bl()
        {
            if (_bottomPlasts.Count == 2)
            {
                if (!HelpManager.IsContiguous(_bottomPlasts))
                {
                    _bottomPlasts[0].Bl = CalcBl(_bottomPlasts[0].Ht());
                    _topPlasts[0].Bl = CalcBl(_topPlasts[0].Ht());
                }
                else
                {
                    SCalculatorB calc = new SCalculatorB(_bottomPlasts);
                    if (_alfa > _delta0)
                    {
                        _bottomPlasts[0].Bl = CalcBl(calc.Count());
                        _bottomPlasts[1].Bl = CalcBl(_bottomPlasts[1].Ht());
                    }
                    else
                    {
                        _bottomPlasts[1].Bl = CalcBl(_bottomPlasts[1].Ht());
                        _bottomPlasts[0].Bl = CalcBl(calc.Count());
                    }
                }
            }
            else if (_bottomPlasts.Count == 3)
            {
                if (HelpManager.IsContiguous(_bottomPlasts))
                {
                    if (_alfa > _delta0)
                    {
                        double[] h_t = new double[3];
                        h_t[2] = _bottomPlasts[2].Ht();
                        h_t[1] = h_t[2] < _bottomPlasts[2].H_() - _bottomPlasts[1].H_() ? _bottomPlasts[1].Ht() : HelpManager.SimpleCalcHt(1, 2, _bottomPlasts);
                        if (h_t[1] < _bottomPlasts[1].H_() - _bottomPlasts[0].H_())
                        {
                            h_t[0] = _bottomPlasts[0].Ht();
                        }
                        else
                        {
                            int endIndex = h_t[1] == _bottomPlasts[1].Ht() ? 1 : 2;
                            h_t[0] = HelpManager.SimpleCalcHt(0, endIndex, _bottomPlasts);
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            _bottomPlasts[i].Bl = CalcBl(h_t[i]);
                        }
                    }
                    else
                    {
                        double[] h_t = new double[3];
                        h_t[2] = _bottomPlasts[2].Ht();
                        h_t[1] = h_t[2] < _bottomPlasts[2].H_() - _bottomPlasts[1].H_() ? _bottomPlasts[1].Ht() : HelpManager.SimpleCalcHt(1, 2, _bottomPlasts);
                        if (h_t[1] < _bottomPlasts[1].H_() - _bottomPlasts[0].H_())
                        {
                            h_t[0] = _bottomPlasts[0].Ht();
                        }
                        else
                        {
                            int endIndex = h_t[1] == _bottomPlasts[1].Ht() ? 1 : 2;
                            h_t[0] = HelpManager.SimpleCalcHt(0, endIndex, _bottomPlasts);
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            _bottomPlasts[i].Bl = CalcBl(h_t[i]);
                        }
                    }
                }
                else
                {
                    HelpManager.CallOrg();
                }
            }
            else
            {
                HelpManager.CallOrg();
            }
        }*/
        /* Метод для расчета ширины приразломных предохранительных целиков 
         со стороны висячего крыла разломной зоны для вышележащих пластов ( _topPlast )
         private void Bv()
        {
            if (_topPlasts.Count == 2)
            {
                if (!HelpManager.IsContiguous(_topPlasts))
                {
                    if (_alfa > _delta0)
                    {
                        for (int i = 0; i < _topPlasts.Count; i++)
                        {
                            _topPlasts[i].Bv = CalcBv(_topPlasts[i].Ht());
                        }
                    }
                    else
                    {
                        _topPlasts[0].Bv = _topPlasts[1].Bv = 50;
                    }
                }
                else
                {
                    SCalculatorB calc = new SCalculatorB(_topPlasts);
                    if (_alfa > _delta0)
                    {
                        _topPlasts[0].Bv = CalcBv(calc.Count());
                        _topPlasts[1].Bv = CalcBv(_topPlasts[1].Ht());
                    }
                    else
                    {
                        _topPlasts[0].Bv = _topPlasts[1].Bv = 50;
                    }
                }
            }
            else if (_topPlasts.Count == 3)
            {
                if (HelpManager.IsContiguous(_topPlasts))
                {
                    if (_alfa > _delta0)
                    {
                        double[] h_t = new double[3];
                        h_t[2] = _topPlasts[2].Ht();
                        h_t[1] = h_t[2] < _topPlasts[2].H_() - _topPlasts[1].H_() ? _topPlasts[1].Ht() : HelpManager.SimpleCalcHt(1, 2, _topPlasts);
                        if (h_t[1] < _topPlasts[1].H_() - _topPlasts[0].H_())
                        {
                            h_t[0] = _topPlasts[0].Ht();
                        }
                        else
                        {
                            int endIndex = h_t[1] == _topPlasts[1].Ht() ? 1 : 2;
                            h_t[0] = HelpManager.SimpleCalcHt(0, endIndex, _topPlasts);
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            _topPlasts[i].Bl = CalcBl(h_t[i]);
                        }
                    }
                    else
                    {
                        double[] h_t = new double[3];
                        h_t[2] = _topPlasts[2].Ht();
                        h_t[1] = h_t[2] < _topPlasts[2].H_() - _topPlasts[1].H_() ? _topPlasts[1].Ht() : HelpManager.SimpleCalcHt(1, 2, _topPlasts);
                        if (h_t[1] < _topPlasts[1].H_() - _topPlasts[0].H_())
                        {
                            h_t[0] = _topPlasts[0].Ht();
                        }
                        else
                        {
                            int endIndex = h_t[1] == _topPlasts[1].Ht() ? 1 : 2;
                            h_t[0] = HelpManager.SimpleCalcHt(0, endIndex, _topPlasts);
                        }
                        for (int i = 0; i < 3; i++)
                        {
                            _topPlasts[i].Bv = 50;
                        }
                    }
                }
                else
                {
                    HelpManager.CallOrg();
                }
            }
            else
            {
                HelpManager.CallOrg();
            }
        }*/
        // From OldCelicVersion
        /*using System;
using System.Collections.ObjectModel;

namespace OldCelic
{
    /// <summary>
    /// Методика расчета приразломных целиков
    /// </summary>
    class CalculatorC
    {
        private static readonly double delta0 = Math.PI / 3;
        /// <summary>
        /// Угол падения плоскости сместителя (разломной зоны),
        /// определяемый согласно графическим приложениям к настоящим «Правилам…»
        /// </summary>
        private readonly double alfa;
        /// <summary>
        /// Список разрабатываемых пластов
        /// </summary>
        private readonly ObservableCollection<Plast> plasts = new ObservableCollection<Plast>();
        /// <summary>
        /// Конструктор с добавлением начальных данных для расчета
        /// </summary>
        /// <param name="arr">Коллекция разрабатываемых пластов</param>
        /// <param name="alfa">Угол падения плоскости сместители разломной зоны</param>
        public CalculatorC(ObservableCollection<Plast> arr, double alfa)
        {
            foreach (Plast plast in arr)
            {
                this.plasts.Add(plast);
            }
            this.alfa = alfa;
        }
        /// <summary>
        /// Метод подсчета B_l в стандартной ситуации 
        /// </summary>
        /// <param name="h_t">ЗВТ</param>
        /// <returns>Значение B_l</returns>
        private double CalcBL(double h_t) => 50 + (h_t * (1 / Math.Tan(delta0) + 1 / Math.Tan(alfa)));
        /// <summary>
        /// Метод расчета параметром B_l и B_v для заданного пласта 
        /// </summary>
        /// <param name="h_t">ЗВТ для рассматриваемого пласта</param>
        /// <param name="index">Индекс пласта в коллекции</param>
        private void CalcB(double h_t, int index)
        {
            plasts[index].B_l = 50 + h_t * (1 / Math.Tan(delta0) + 1 / Math.Tan(alfa));
            plasts[index].B_v = 50 + h_t * (1 / Math.Tan(delta0) - 1 / Math.Tan(alfa));
        }    
        /// <summary>
        /// Метод для рассчета ширина предохранительного приразломного
        /// целика на уровне разрабатываемого пласта со стороны лежачего
        /// крыла разломной зоны, м;
        /// </summary>
        public void B()
        {
            double delta0 = Math.PI / 3;
            if (plasts.Count == 2)
            {
                if (!HelpManager.isContiguous(plasts))
                {
                    if(alfa > delta0)
                    {
                        for (int i = 0; i < plasts.Count; i++)
                        {
                            CalcB(plasts[i].H_t(), i);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < plasts.Count; i++)
                        {
                            plasts[i].B_l = CalcBL(plasts[i].H_t());
                            plasts[i].B_v = 50;
                        }
                    }
                }
                else
                {
                    CalculatorB calc = new CalculatorB(plasts);
                    CalcB(calc.H_t(), 0);
                    CalcB(plasts[1].H_t(), 1);
                }
            }
            else if (plasts.Count >= 3)
            {
                if (HelpManager.isContiguous(plasts))
                {
                    double[] h_t = new double[3];
                    h_t[2] = plasts[2].H_t();
                    h_t[1] = h_t[2] < plasts[2].H - plasts[1].H ? plasts[1].H_t() : HelpManager.calcH_t(1, 2, plasts);
                    if(h_t[1] < plasts[1].H - plasts[0].H)
                    {
                        h_t[0] = plasts[0].H_t();
                    }
                    else
                    {
                        int endIndex = h_t[1] == plasts[1].H_t() ? 1 : 2;
                        h_t[0] = HelpManager.calcH_t(0, endIndex, plasts);
                    }
                    for(int i = 0; i < 3; i++)
                    {
                        CalcB(h_t[i], i);
                    }
                }
                else
                {
                    HelpManager.callOrg();
                }
            }
            else
            {
                HelpManager.callOrg();
            }
        }
    }
}
*/
        #endregion

        #region Old Logic SCalculatorD
        /* Метод для расчета минимально необходимой ширины междубарьерного целика для нескольких пластов
           Return: Список значений ширины междубарьерного целика для каждой пары пластов
         public List<double> BMin()
        {
            List<double> _bMin = new List<double>();
            if (_leftPlasts.Count == 1 && _rightPlasts.Count == 1)
            {
                if (!IsContiguous(_rightPlasts[0], _leftPlasts[0]))
                {
                    _bMin.Add(BMin(_rightPlasts[0].Ht(), _leftPlasts[0].Ht()));
                }
                else
                {
                    ObservableCollection<Plast> _plasts = new ObservableCollection<Plast>();
                    SCalculatorB calculatorB = new SCalculatorB(_plasts);
                    if (_leftPlasts[0].H_() > _rightPlasts[0].H_())
                    {
                        _plasts.Add(_rightPlasts[0]);
                        _plasts.Add(_leftPlasts[0]);
                        _bMin.Add(BMin(_leftPlasts[0].Ht(), calculatorB.Count()));
                    }
                    else
                    {
                        _plasts.Add(_leftPlasts[0]);
                        _plasts.Add(_rightPlasts[0]);
                        _bMin.Add(BMin(_rightPlasts[0].Ht(), calculatorB.Count()));
                    }
                }
            }
            else if (_leftPlasts.Count >= 2 && _rightPlasts.Count >= 2)
            {
                if (IsContiguous(_leftPlasts) || IsContiguous(_rightPlasts))
                {
                    double leftHt3 = _leftPlasts[2].Ht(), rightHt3 = _rightPlasts[2].Ht();
                    _bMin.Add(BMin(leftHt3, rightHt3));
                    double leftdH23 = _leftPlasts[2].H_() - _leftPlasts[1].H_(); //расстояние между 3 и 2 пластами слева
                    double rightdH23 = _rightPlasts[2].H_() - _rightPlasts[1].H_(); // расстояние между 3 и 2 пластами справа
                    double leftdH12 = _leftPlasts[1].H_() - _leftPlasts[0].H_(); //расстояние между 2 и 1 пластами слева
                    double rightdH12 = _rightPlasts[1].H_() - _rightPlasts[0].H_(); // расстояние между 2 и 1 пластами справа
                    double leftHt2 = 0, rightHt2 = 0;
                    if (leftHt3 < leftdH23 && rightHt3 < rightdH23)
                    {
                        leftHt2 = _leftPlasts[1].Ht();
                        rightHt2 = _rightPlasts[1].Ht();
                    }
                    else if (leftHt3 >= leftdH23 && rightHt3 >= rightdH23)
                    {
                        leftHt2 = SimpleCalcHt(1, 2, _leftPlasts);
                        rightHt2 = SimpleCalcHt(1, 2, _rightPlasts);
                    }
                    else
                    {
                        CallOrg();
                    }
                    _bMin.Add(BMin(leftHt2, rightHt2));
                    double leftHt1 = 0, rightHt1 = 0;
                    if (leftHt2 < leftdH12 && rightHt2 < rightdH12)
                    {
                        leftHt1 = _leftPlasts[0].Ht();
                        rightHt1 = _rightPlasts[0].Ht();
                    }
                    else if (leftHt2 >= leftdH12 && rightHt2 >= rightdH12)
                    {
                        if (leftHt2 == _leftPlasts[1].Ht() && rightHt2 == _rightPlasts[1].Ht())
                        {
                            leftHt1 = SimpleCalcHt(0, 1, _leftPlasts);
                            rightHt1 = SimpleCalcHt(0, 1, _rightPlasts);
                        }
                        else
                        {
                            if (leftHt2 == SimpleCalcHt(1, 2, _leftPlasts) && rightHt2 == SimpleCalcHt(1, 2, _rightPlasts))
                            {
                                leftHt1 = SimpleCalcHt(0, 2, _leftPlasts);
                                rightHt1 = SimpleCalcHt(0, 2, _rightPlasts);
                            }
                            else
                            {
                                CallOrg();
                            }
                        }
                    }
                    else
                    {
                        CallOrg();
                    }
                    _bMin[0] = BMin(leftHt1, rightHt1);
                }
                else
                {
                    CallOrg();
                }
            }
            else
            {
                CallOrg();
            }
            return _bMin;
        }*/
        // Logic CalculatroD from OldCelicVersion
        /*private double bMin(double H_t1, double H_t2)
        {
            return 0.18 * (H_t1 + H_t2) + 66;
        }
        public List<double> B_min()
        {
            List<double> b_min = new List<double>();
            if (plasts.Count == 2)
            {
                b_min.Add(bMin(!HelpManager.isContiguous(plasts) ? plasts[0].H_t() : HelpManager.calcH_t(0, 1, plasts), plasts[1].H_t()));
            }
            else if (plasts.Count >= 3)
            {
                if (HelpManager.isContiguous(plasts))
                {
                    double H_t31 = plasts[2].H_t(), H_t32 = plasts[2].H_t();
                    b_min.Add(bMin(plasts[2].H_t(), plasts[2].H_t()));
                    double dH23 = plasts[2].H - plasts[1].H; //расстояние между 3 и 2 пластами
                    double dH12 = plasts[1].H - plasts[0].H; //расстояние между 2 и 1 пластами
                    double H_t21 = 0, H_t22 = 0;
                    if (H_t31 < dH23 && H_t32 < dH23)
                    {
                        H_t21 = plasts[1].H_t();
                        H_t22 = plasts[1].H_t();
                    }
                    else if (H_t31 >= dH23 && H_t32 >= dH23)
                    {
                        H_t21 = H_t22 = HelpManager.calcH_t(1, 2, plasts);   
                    }
                    else
                    {
                        HelpManager.callOrg();
                    }
                    b_min.Add(bMin(H_t21, H_t22));
                    double H_t11 = 0, H_t12 = 0;
                    if (H_t21 < dH12 && H_t22 < dH12)
                    {
                        H_t11 = plasts[0].H_t();
                        H_t12 = plasts[0].H_t();
                    }
                    else if (H_t21 >= dH12 && H_t22 >= dH12)
                    {
                        if (H_t21 == plasts[1].H_t() && H_t22 == plasts[1].H_t())
                        {
                            H_t11 = H_t12 = HelpManager.calcH_t(0, 1, plasts);
                        }
                        else
                        {
                            H_t11 = H_t12 = H_t21 == HelpManager.calcH_t(1, 2, plasts) && H_t22 == HelpManager.calcH_t(1, 2, plasts) ? HelpManager.calcH_t(0, 2, plasts) : HelpManager.callOrg();
                        }
                    }
                    else
                    {
                        HelpManager.callOrg();
                    }
                    b_min[0] = 0.18 * (H_t11 + H_t12) + 66;
                }
                else
                {
                    HelpManager.callOrg();
                }
            }
            else
            {
                HelpManager.callOrg();
            }
            return b_min;
        }*/
        #endregion

        #region Old Logic TextManager
        /*
        #region Constructors
        
        /// <summary>
        /// 
        /// </summary>
        static BaseTextManager()
        {
            _app = new Word.Application();
        }

        #endregion
        
        #region Public Static Methods

        /// <summary>
        /// Метод для записи математических формул и их корректного форматирования
        /// </summary>
        public static void AddParagraphMath(List<string> _txt)
        {
            for (int i = 0; i < _txt.Count; i++)
            {
                Word.Paragraph par = _app.ActiveDocument.Paragraphs.Add();
                par.Range.Text = _txt[i];
                par.Range.Font.Size = 9;
                _app.ActiveDocument.OMaths.Add(par.Range);
                par.Range.InsertParagraphAfter();
            }
            _app.ActiveDocument.OMaths.BuildUp();
            _txt.Clear();
        }
        /// <summary>
        /// Метод для записи параграфа
        /// </summary>
        /// <param name="text">Записываемый текст</param>
        public static void AddParagraph(string text)
        {
            Word.Paragraph par = _app.ActiveDocument.Paragraphs.Add();
            par.Range.Text = text;
            par.Range.InsertParagraphAfter();
        }

        #endregion

        #region Private Static Methods

        /// <summary>
        /// Метод для преобразования радиан в гардусы
        /// </summary>
        /// <param name="angle">Угол в радианах</param>
        /// <returns>Значение угал в градусах</returns>
        private static double ToGrad(double angle) => angle / Math.PI * 180;
        /// <summary>
        /// Метод для корректного форматирования специального двухбуквенного выражения
        /// </summary>
        /// <param name="start">Строковое представление выражения</param>
        /// <param name="_par">Исходный параграф</param>
        private static void SelectTwo(string start, Word.Paragraph _par)
        {
            int displacement = 0, length = 2, _rangeLength = _app.ActiveDocument.Range().Text.Length;
            _app.ActiveDocument.Range(_par?.Range.Text.IndexOf(start) + _rangeLength + displacement,
                _par?.Range.Text.IndexOf(start) + _rangeLength + length + displacement).Select();
            _app.Selection.ItalicRun();
            length = displacement = 1;
            _app.ActiveDocument.Range(_par?.Range.Text.IndexOf(start) + _rangeLength + displacement,
                _par?.Range.Text.IndexOf(start) + _rangeLength + start.Length + displacement).Select();
            _app.Selection.Font.Subscript = 1;
        }
        /// <summary>
        /// Метод для корректного форматирования специальных параметров
        /// </summary>
        /// <param name="start">Строковое представление параметра</param>
        /// <param name="_par">Исходный параграф с указанным параметром</param>
        private static void SelectThree(string start, Word.Paragraph _par)
        {
            int displacement = 0, length = 3, _rangeLength = _app.ActiveDocument.Range().Text.Length;
            _app.ActiveDocument.Range(_par?.Range.Text.IndexOf(start) + _rangeLength + displacement,
                _par?.Range.Text.IndexOf(start) + _rangeLength + length + displacement).Select();
            _app.Selection.ItalicRun();
            length = displacement = 1;
            _app.ActiveDocument.Range(_par?.Range.Text.IndexOf(start) + _rangeLength + displacement,
                _par?.Range.Text.IndexOf(start) + _rangeLength + start.Length + displacement).Select();
            _app.Selection.Font.Subscript = 1;
            displacement = 2;
            _app.ActiveDocument.Range(_par?.Range.Text.IndexOf(start) + _rangeLength + displacement,
               _par?.Range.Text.IndexOf(start) + _rangeLength + start.Length + displacement).Select();
            _app.Selection.Font.Subscript = 2;
        }

        #endregion

        /// <summary>
        /// Метод для записи расчета высоты ЗВТ
        /// </summary>
        /// <param name="i">Индекс пласта в списке</param>
        /// <param name="ht">Массив высот ЗВТ</param>
        /// <param name="_plasts">Коллекция разрабатываемых пластов</param>
        public static int AddParagraphCalcHt(int i, double[] ht, ObservableCollection<Plast> _plasts)
        {
            Word.Paragraph par = _app.ActiveDocument.Paragraphs.Add();
            par.Range.Text = $"HT = d ∙ mпр = {_plasts[i].D()} ∙ {_plasts[i].MPr()} = {ht[i] = _plasts[i].Ht()} м;";
            SelectTwo("HT", par);
            Select("d ∙ mпр", 7, 0, par).ItalicRun();
            Select("mпр", 2, 1, par).Font.Subscript = 1;
            par.Range.InsertParagraphAfter();
            return _app.ActiveDocument.Range().Text.Length;
        }
        /// <summary>
        /// Метод для записи параграфа с информацией о подсчете параметра ВЛ
        /// </summary>
        /// <param name="i">Индекс пласта в списке</param>
        /// <param name="ht">Массив высот ЗВТ</param>
        private void AddParagraphCalcBl(int i, double[] ht)
        {
            Word.Paragraph par = _app.ActiveDocument.Paragraphs.Add();
            par.Range.Text = $"ВЛ = 50 + HT ∙ (ctgδ0 + ctgα) = 50 + {ht[i]} ∙ (ctg{ToGrad(_delta0)} + ctg{ToGrad(_alfa)})" +
                $" = {_plasts[i].Bl = CalcBl(ht[i])};";
            SelectTwo("ВЛ", par);
            SelectTwo("HT", par);
            SelectTwo("δ0", par);
            Select("α", 1, 1, par).ItalicRun();
            par.Range.InsertParagraphAfter();
            _rangeLength = _app.ActiveDocument.Range().Text.Length;
        }
        /// <summary>
        /// Метод для записи параграфа с информацией о подсчете параметра ВВ
        /// </summary>
        /// <param name="i">Индекс пласта в списке</param>
        /// <param name="ht">Массив высот ЗВТ</param>
        private void AddParagraphCalcBv(int i, double[] ht)
        {
            Word.Paragraph par = _app.ActiveDocument.Paragraphs.Add();
            par.Range.Text = $"ВВ = 50 + HT ∙ (ctgδ0 - ctgα) = 50 + {ht[i]} ∙ (ctg{ToGrad(_delta0)} - ctg{ToGrad(_alfa)})" +
                $" = {_plasts[i].Bv = CalcBv(ht[i])};";
            SelectTwo("ВЛ", par);
            SelectTwo("HT", par);
            SelectTwo("δ0", par);
            Select("α", 1, 1, par).ItalicRun();
            par.Range.InsertParagraphAfter();
            _rangeLength = _app.ActiveDocument.Range().Text.Length;
        }
        /// <summary>
        /// Метод для выделения текста из указанного параграфа
        /// </summary>
        /// <param name="start">Выделяемый текст</param>
        /// <param name="length">Длина выделяемого текста</param>
        /// <param name="displacement">Количетсво симолов, на которые необходимо сместиться</param>
        /// <param name="_par">Исходный параграф, содержащий выделяемый текст</param>
        /// <returns>Выделенную область</returns>
        private Word.Selection Select(string start, int length = 1, int displacement = 0, Word.Paragraph _par = null)
        {
            _app.ActiveDocument.Range(_par?.Range.Text.IndexOf(start) + _rangeLength + displacement,
                _par?.Range.Text.IndexOf(start) + _rangeLength + length + displacement).Select();
            return _app.Selection;
        }
        /// <summary>
        /// Метод для записи параграфа с информацией о сравнении углов при расчете ВВ
        /// </summary>
        /// <param name="text">Записываемый текст</param>
        private void AddParagraphAngle(string text)
        {
            Word.Paragraph par = _app.ActiveDocument.Paragraphs.Add();
            par.Range.Text = text;
            Select("α", 1, 0, par).ItalicRun();
            SelectTwo("δ0", par);
            SelectTwo("ВВ", par);
            par.Range.InsertParagraphAfter();
            _rangeLength = _app.ActiveDocument.Range().Text.Length;
        }
        /// <summary>
        /// Метод для записи параграфа с двухбуквенным специальным куском текста
        /// </summary>
        /// <param name="text">Записываемый текст</param>
        /// <param name="selectedTwo">Специальный кусок</param>
        private void AddParagraphWithSelectedTwo(string text, string selectedTwo)
        {
            Word.Paragraph par = _app.ActiveDocument.Paragraphs.Add();
            par.Range.Text = text;
            SelectTwo(selectedTwo, par);
            par.Range.InsertParagraphAfter();
            _rangeLength = _app.ActiveDocument.Range().Text.Length;
        }
        /// <summary>
        /// Метод для записи параграфа с двумя трехбуквенными специальными кусками текста
        /// </summary>
        /// <param name="text">Записываемый текст</param>
        /// <param name="selected1">Первый специальный кусок текста</param>
        /// <param name="selected2">Второй специальный кусок текста</param>
        private void AddParagraphWithSelectedThree(string text, string selected1, string selected2)
        {
            Word.Paragraph par = _app.ActiveDocument.Paragraphs.Add();
            par.Range.Text = text;
            SelectThree(selected1, par);
            SelectThree(selected2, par);
            par.Range.InsertParagraphAfter();
            _rangeLength = _app.ActiveDocument.Range().Text.Length;
        }
        /// <summary>
        /// Метод для записи параграфа со специальным текстом
        /// </summary>
        /// <param name="text">Записываемый текст</param>
        /// <param name="selected1">Первый специальный кусок текста</param>
        /// <param name="selected2">Второй специальный кусок текста</param>
        /// <param name="selected3">Третий специальный кусок текста</param>
        private void AddParagraphSpecial(string text, string selected1, string selected2, string selected3)
        {
            Word.Paragraph par = _app.ActiveDocument.Paragraphs.Add();
            par.Range.Text = text;
            SelectThree(selected1, par);
            SelectThree(selected2, par);
            Select(selected3, 5, 0, par).ItalicRun();
            Select(selected3, 3, 2, par).Font.Subscript = 1;
            par.Range.InsertParagraphAfter();
            _rangeLength = _app.ActiveDocument.Range().Text.Length;
        }*/
        #endregion

        #region Functions from OldCelicVersion

        /*public static double calck(Lava one, Lava two, double B)
        {
            double k = 0;
            if (B < 0.58 * (one.H_t() + two.H_t()))
            {
                if (300 >= Math.Max(one.D, two.D) && Math.Max(one.D, two.D) >= one.D_0)
                {
                    k = 1;
                }
                else if (Math.Max(one.D, two.D) < one.D_0)
                {
                    if (Math.Max(one.D, two.D) + B >= one.D_0)
                    {
                        k = Math.Sqrt(Math.Max(one.D, two.D) / one.D_0);
                    }
                    else
                    {
                        k = (one.D + two.D) / (one.D + 2 * B + two.D);
                    }
                }
            }
            return k;
        }*/
        // Old Logic Plast.RecalcKLava
        /*if (lava1.B.V < 0.58F * (lava1.Ht() + lava2.Ht()))
                        {
                            if (bf == false)
                            {
                                value = i;
                            }
                            else
                            {
                                if (ef == false && bf == true)
                                {
                                    float d0, d, d1, maxD, b, k = 0;
                                    for (int j = value; j < i - 1; j++)
                                    {
                                        if (_mineFields[j] is Lava temp1 && _mineFields[j + 1] is Lava temp2)
                                        {
                                            d = temp1.Mv.V;
                                            d0 = 1.4F * temp1.CalcD() * d;
                                            d1 = temp2.Mv.V;
                                            maxD = Max(d, d1);
                                            b = temp1.B.V;
                                            k = Max(k, maxD >= d0 ? 1 :
                                                (maxD + b >= d0 ? (float)Sqrt(maxD / d0) : (d + d1) / (d + 2 * b + d1)));
                                        }
                                    }
                                    for (int j = value; j < i; j++)
                                    {
                                        if (_mineFields[j] is Lava tmp)
                                        {
                                            tmp.K = new EFloat(k);
                                        }
                                    }
                                    ef = bf = false;
                                    value = 0;
                                }
                            }
                        }*/

        #endregion
    }
}
