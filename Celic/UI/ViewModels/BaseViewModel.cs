using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Celic
{
	/// <summary>
	/// Description of BaseViewModel.
	/// </summary>
	public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Событие при изменении свойства компонента
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Обработчик для события Propertychanged
        /// </summary>
        /// <param name="prop"></param>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
        	if(PropertyChanged != null)
            	PropertyChanged.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
