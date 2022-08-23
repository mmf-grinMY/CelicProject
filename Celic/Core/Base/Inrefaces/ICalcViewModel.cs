/*
 * Created by SharpDevelop.
 * User: Максим
 * Date: 08/20/2022
 * Time: 19:01
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Celic
{
	/// <summary> Общая логика классов "SCalc?ViewModel" </summary>
    public interface ICalcViewModel
    {
        /// <summary> Команда удаления пласта </summary>
        RelayCommand RemoveCommand { get; }
        /// <summary> Команда удаления выделения пласта </summary>
        RelayCommand RemoveSelectionCommand { get; }
        /// <summary> Команда добавления пластов </summary>
        RelayCommand AddCommand { get; }
        /// <summary> Команда расчета с азписью хода вычисления в файл *.docx </summary>
        RelayCommand CalcWithDocxCommand { get; }
        /// <summary> Команда расчета без записи в файл </summary>
        RelayCommand CalcWithoutDocxCommand { get; }
        /// <summary> Команда открытия главного окна </summary>
        RelayCommand OpenMainWindowCommand { get; }
        /// <summary> Команда открытия файла с данными для добавления в систему разрабатываемых пластов </summary>
        RelayCommand OpenReadCommand { get; }
        /// <summary> Команда сохранения системы разрабатываемых пластов без выбора пути сохранения </summary>
        RelayCommand SimpleSaveCommand { get; }
        /// <summary> Команда сохранения системы разрабатываемых пластов с выбором пути сохранения данных </summary>
        RelayCommand SaveWithPathCommand { get; }
    }
}
