using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DataBinding.ViewModel
{
    class RelayCommand : ICommand
    {
        private Action<object> _action;
        private Func<object, bool> _canExecute;
        private string _name;
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }
        public void Execute(object parameter)
        {
            _action?.Invoke(parameter);
        }
        public RelayCommand(string name, Action<object> action, Func<object, bool> canExecute)
        {
            _name = name;
            _action = action;
            _canExecute = canExecute;
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public override string ToString()
        {
            if (string.IsNullOrWhiteSpace(_name)) {
                return "Command";
            }
            return _name;
        }
    }
}
