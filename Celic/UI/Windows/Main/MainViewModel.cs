﻿using System;

namespace Celic
{
	/// <summary> Главный класс обработки системы разработки </summary>
    public class MainViewModel : BaseViewModel
    {
        #region Private Fields

        /// <summary> Главное окно приложения </summary>
        private readonly MainWindow _mainWindow;

        #endregion

        #region Constructors

        /// <summary> Основной конструктор для данного класса </summary>
        /// <param name="mainWindow"> Главное окно приложения </param>
        public MainViewModel(MainWindow mainWindow) : base()
        {
            SimpleCalcBCommand = new RelayCommand(obj =>
            {
                _mainWindow.Hide();
                new SCalcBWindow(_mainWindow).ShowDialog();
            });
            SimpleCalcCCommand = new RelayCommand(obj =>
            {
                _mainWindow.Hide();
                new SCalcCWindow(_mainWindow).ShowDialog();
            });
            SimpleCalcDCommand = new RelayCommand(obj =>
            {
                _mainWindow.Hide();
                new SCalcDWindow(_mainWindow).ShowDialog();
            });
            _mainWindow = mainWindow;
        }

        #endregion

        #region Commands

        /// <summary> Команда открытия окна расчета высоты ЗВТ </summary>
        public RelayCommand SimpleCalcBCommand { private set; get; }
        /// <summary> Команда открытия окна расчета приразломных целиков </summary>
        public RelayCommand SimpleCalcCCommand { private set; get; }
        /// <summary> Команда открытия окна расчета ширины междубарьерных целиков </summary>
        public RelayCommand SimpleCalcDCommand { private set; get; }

        #endregion
        
        #region Test
        
        private float _value;
        public float Value
        {
        	get
        	{
        		return _value;
        	}
        	set
        	{
        		_value = value;
        		OnPropertyChanged("Value");
        	}
        }
        
        #endregion
    }
}
