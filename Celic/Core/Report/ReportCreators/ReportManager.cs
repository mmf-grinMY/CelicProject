using System;
using Word = Microsoft.Office.Interop.Word;
using System.Windows;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using System.IO;

namespace Celic
{
	/// <summary> Управление процессом генерации отчета </summary>
	public static class ReportManager
	{
		public static bool WriteReport(ListPlastViewModel model)
		{
			Word.Application app = null;
			try
            {
                app = new Word.Application();
                app.Documents.Open(GetSavePath());
                app.ActiveDocument.Range().Font.Size = 12;
                app.ActiveDocument.Range().Font.Name = "Times New Roman";
                if(model.GetType() == typeof(SCalcBViewModel))
                {
                	WriteCalculation((model as SCalcBViewModel).Plasts, app);
                }
                app.ActiveDocument.Save();
                app.ActiveDocument.Close();
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                if (app != null)
                    app.Quit();
            }
            return false;
		}
		// Write report Info about calculation B
		/// <summary> Запись проведенных операций вычислений высоты ЗВТ </summary>
        /// <param name="reporter"> Модель, отслеживающая запись процесса расчета в файл </param>
        private static void WriteCalculation(ObservableCollection<Plast> plasts, Word.Application app)
        {
        	List<string> txt = new List<string>();
            new TextManager(app).Write(plasts, CalculatorB.CountLog(plasts, txt));
            TableManager tableManager = new TableManager(app, plasts);
            int rangeLength = tableManager.WriteTable();
            Word.Cell cell = tableManager.Cell;
            BaseCalculator.AddParagraphMath(txt, app);
            app.ActiveDocument.Range(rangeLength - 6).Select();
            app.Selection.Cut();
            cell.TopPadding = 12F;
            cell.Select();
            app.Selection.Paste();
        }
        /// <summary> Выбор пути сохранения файла путем выбора пользователем в открывшемся окне </summary>
        /// <returns> Абсолютный путь сохранения файла </returns>
        private static string GetSavePath()
        {
            SaveFileDialog save = new SaveFileDialog
            {
                AddExtension = true,
                InitialDirectory = @"D:\",
                RestoreDirectory = true,
                Title = "Выбор файла",
                DefaultExt = "docx",
                Filter = "docx files (*.docx)|*.docx|All files (*.*)|*.*",
                FilterIndex = 1,
                CreatePrompt = false
            };
            save.ShowDialog();
            if (File.Exists(save.FileName))
                File.Delete(save.FileName);
            File.Create(save.FileName).Close();
            return save.FileName;
        }
	}
}
