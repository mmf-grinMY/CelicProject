namespace Celic
{
    /// <summary> Логика работы окна выбора схемы расположения </summary>
    public class SchemaChoiceViewModel
    {
        #region Private Fields

        /// <summary> Окно выбора схемы </summary>
        private SchemaChoiceWindow _window = null;

        #endregion

        #region Constructors 

        /// <summary> Основной конструктор для данного класса </summary>
        public SchemaChoiceViewModel(int[] schema)
        {
            ChoiceCommand = new RelayCommand(obj =>
            {
                schema[0] = int.Parse(obj.ToString());
                _window.Close();
            });
            _window = new SchemaChoiceWindow(this);
            _window.ShowDialog();
        }

        #endregion

        #region Commands

        /// <summary> Команда выбора схемы </summary>
        public RelayCommand ChoiceCommand { private set; get; }

        #endregion
    }
}
