using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SwimmerManagmentUI.Commands
{
    public class RelayCommand : BaseCommand
    {
        public override event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        #region Fields and Properties
        private Action _execute;
        private Func<bool> _canExecute;
        #endregion

        #region Constructors
        public RelayCommand() { }
        public RelayCommand(Action action, Func<bool> func)
        {
            _execute = action ?? throw new ArgumentNullException(nameof(action));
            _canExecute = func;
        }
        public RelayCommand(Action action) : this(action, null) { }
        #endregion

        #region Methods
        public override bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute();
        }

        public override void Execute(object parameter)
        {
            _execute();
        }
        #endregion
    }
}
