using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Celic
{
    /// <summary> Вспомогательный класс, содержащий часто используемые общие методы </summary>
    public class HelpManager
    {
        /// <summary> Удаление нулей в начале строки </summary>
        /// <param name="value"> Строка для преобразования </param>
        /// <returns> Преобразованная строка без лишних нулей в начале</returns>
        public static string RmZero(string value)
        {
            /*if (value != "")
            {
                bool isBegin = true;
                int i;
                for (i = 0; i < value.Length && isBegin; i++)
                    if (value[i] != '0')
                    {
                        if (value[i] == ',')
                            i--;
                        isBegin = false;
                    }
                return value.Remove(0, i - 1);
            }
            return value;*/
            int length = 0;
            bool run = true;
            for (int i = 0; i < value.Length - 1 && run; i++)
            {
                if (value[i] == '0')
                {
                    if (value[i + 1] == ',')
                        run = false;
                    else if (value[i + 1] == '0')
                        length++;
                    else if (char.IsDigit(value[i + 1]))
                    {
                        length++;
                        run = false;
                    }
                }
            }
            return value.Remove(0, length);
        }
        /// <summary> Вычисление высот ЗВТ над несколькими пластами </summary>
        /// <param name="begin"> Номер первого элемента, входящего в расчет </param>
        /// <param name="end"> Номер последнего элемента, входящего в расчет </param>
        /// <param name="plasts"> Коллекция рассматриваемых плаcтов </param>
        /// <param name="txt"> Информация о проведении подсчета </param>
        /// <returns> Значение высоты ЗВТ </returns>
        public static float SimpleCalcHt(int begin = 0, int end = 1, ObservableCollection<Plast> plasts = null, List<string> txt = null)
        {
            ObservableCollection<Plast> help = new ObservableCollection<Plast>();
            for (int i = begin; i <= end; i++)
                help.Add(plasts[i]);
            SCalculatorB calc = new SCalculatorB(help);
            txt?.AddRange(calc.TextCount);
            float ht = calc.Count();
            for (int i = 0; i < end - begin + 1; i++)
            {
                help.RemoveAt(help.Count - 1);
            }
            return ht;
        }
        /// <summary> Проверка двух пластов на сближенность по условиям водопроводимости </summary>
        /// <param name="plast1"> Первый пласт </param>
        /// <param name="plast2"> Второй пласт </param>
        /// <returns> true, если пласты являются сближенными; false в протичном случае </returns>
        public static bool IsContiguous(Plast plast1, Plast plast2)
        {
            if(plast1.Height > plast2.Height)
                return plast1.Buttom.Equals(plast2.Name) && plast2.Top.Equals(plast1.Name);
            else
                return plast2.Buttom.Equals(plast1.Name) && plast1.Top.Equals(plast2.Name);
        }
        /// <summary> Проверка коллекции пластов на наличие сближенных по условиям водопроводимости </summary>
        /// <param name="plasts"> Коллекция рассматриваемых пластов </param>
        /// <returns> true, если есть сближенные; false, если нет ни одной пары сближенных пластов </returns>
        public static bool IsContiguous(ObservableCollection<Plast> plasts)
        {
            foreach (Plast plast1 in plasts)
                foreach (Plast plast2 in plasts)
                    if (plast1 != plast2 && IsContiguous(plast1, plast2))
                        return true;
            return false;
        }
        /// <summary> Сортировка коллекции пластов по возрастанию величины параметра H </summary>
        /// <param name="plasts"> Коллекция разрабатываемых пластов</param>
        /// <returns> Отсортированная коллекция пластов </returns>
        public static ObservableCollection<Plast> Sort(ObservableCollection<Plast> plasts)
        {
            for (int i = 1; i < plasts.Count; i++)
                for (int j = i; j > 0 && plasts[j - 1].Height > plasts[j].Height; j--)
                    plasts.Move(j - 1, j);
            return plasts;
        }
        /// <summary> Выбор коэффициента параметра d при генерации отчета </summary>
        /// <param name="plast"> Рассматриваемый пласт </param>
        /// <returns> Значение коэффициента в виде строки </returns>
        public static string GetD(Plast plast) => plast.TypeDev == "столбовая" ? "46" : "26";
    }
}
