using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace TrustsAndTpa.TrustAndTpa12.Infrastructure {
    public abstract class SetPropertyBase : INotifyPropertyChanged {
        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        [DebuggerStepThrough]
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null) {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        [DebuggerStepThrough]
        public void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string? propertyName = null) {
            if (Equals(value, backingField)) return;

            backingField = value;

            RaisePropertyChanged(propertyName);
        }
        #endregion INotifyPropertyChanged
    }
}
