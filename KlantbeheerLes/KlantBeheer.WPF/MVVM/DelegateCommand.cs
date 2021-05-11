using System;
using System.Windows.Input;

namespace KlantBeheer.WPF.MMVM
{
    public class DelegateCommand : ICommand
    {
        // These delegates store methods to be called that contains the Execute and CanExecute methods
        // for each particular instance of DelegateCommand.
        private readonly Predicate<object> _canExecute;
        private readonly Action<object> _Execute;

        public DelegateCommand()
        {

        }

        //Two Constructors, for convenience and clean code - often you won't need CanExecute
        public DelegateCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _canExecute = canExecute;
            _Execute = execute;
        }

        public DelegateCommand(Action<object> execute)
          : this(execute, null)
        {
        }

        //CanExecute and Execute come from ICommand
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            if (!CanExecute(parameter))
                return;

            _Execute(parameter);
        }

        /// <summary>
        /// Not a part of ICommand, but commonly added so you can trigger a manual refresh on the result of CanExecute.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}