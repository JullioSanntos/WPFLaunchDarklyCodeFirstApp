using System.Diagnostics;
using System.Windows.Input;

namespace TrustsAndTpa.TrustAndTpa12.Infrastructure {
    public class RelayCommand : ICommand {
        #region Fields

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        #endregion // Fields

        #region Constructors

        [DebuggerStepThrough]
        public RelayCommand(Action<object> execute) : this(execute, null) { }

        [DebuggerStepThrough]
        public RelayCommand(Action<object> execute, Predicate<object> canExecute) {
            ArgumentNullException.ThrowIfNull(execute);

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion // Constructors

        #region ICommand Members

        [DebuggerStepThrough]
        public bool CanExecute(object parameter) {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler? CanExecuteChanged;
        //{
        //    add { CommandManager.RequerySuggested += value; }
        //    remove { CommandManager.RequerySuggested -= value; }
        //}

        [DebuggerStepThrough]
        public void Execute(object parameter) {
            _execute(parameter);
        }

        #endregion // ICommand Members
    }
}
