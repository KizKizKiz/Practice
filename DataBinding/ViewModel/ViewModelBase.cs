using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
            VerifyPropertyName(prop);
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
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        public void VerifyPropertyName(string propertyName)
        {            
            if (TypeDescriptor.GetProperties(this)[propertyName] == null) {
                string msg = "Invalid property name: " + propertyName;                
                throw new Exception(msg);                
            }
        }
    }
}
