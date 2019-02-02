using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SwimmerManagmentUI.Commands
{
    public abstract class BaseCommand : ICommand
    {
        public abstract event EventHandler CanExecuteChanged;

        public abstract bool CanExecute(object parameter);
        public abstract void Execute(object parameter);
    }
}
