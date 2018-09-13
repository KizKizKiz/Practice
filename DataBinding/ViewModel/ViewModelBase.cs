using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataBinding.ViewModel
{
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// Устанавливает свойству типа <see cref="T"/> новое значение
        /// </summary>        
        /// <param name="field">Текущее значение</param>
        /// <param name="value">Новое значение</param>
        /// <param name="prop">Имя свойства</param>
        /// <returns></returns>
        protected virtual bool SetProperty<T>(ref T field, T value, [CallerMemberName]string prop = "")
        {
            if (EqualityComparer<T>.Default.Equals(value, field)) {
                return false;
            }
            field = value;
            OnPropertyChanged(prop);
            return true;            
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName]string prop=null)
        {
            PropertyChanged?.Invoke(null, new PropertyChangedEventArgs(prop));
        }
    }
}
