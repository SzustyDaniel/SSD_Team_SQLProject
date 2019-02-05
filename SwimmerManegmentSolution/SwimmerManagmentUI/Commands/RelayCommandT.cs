using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SwimmerManagmentUI.Commands
{
    public class RelayCommand<T>: BaseCommand
    {
        public override event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        #region Fields and Properties
        private Action<T> _execute;
        private Func<T, bool> _canExecute;
        #endregion

        #region Constructors
        public RelayCommand() { }
        public RelayCommand(Action<T> action, Func<T, bool> func)
        {
            _execute = action ?? throw new ArgumentNullException(nameof(action));
            _canExecute = func;
        }
        public RelayCommand(Action<T> action) : this(action, null) { }
        #endregion

        #region Methods
        public override bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((T)parameter);
        }

        public override void Execute(object parameter)
        {
            if(parameter is T)
                _execute((T)parameter);
        }
        #endregion
    }
}
