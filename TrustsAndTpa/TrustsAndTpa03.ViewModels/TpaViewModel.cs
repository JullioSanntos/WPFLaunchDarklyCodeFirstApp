using System.ComponentModel;
using TrustsAndTpa.TrustsAndTpa12.Infrastructure;

namespace TrustsAndTpa.TrustsAndTpa03.ViewModels {
    public class TpaViewModel : ViewModelBase {
        #region CurrentTpaTrustState
        private TpaTrustState _currentTpaTrustState = TpaTrustState.Unbound;
        public TpaTrustState CurrentTpaTrustState {
            get => _currentTpaTrustState;
            set => SetProperty(ref _currentTpaTrustState, value);
        }
        #endregion CurrentTpaTrustState

        #region Commands
        #region SetEditingState
        public RelayCommand SetEditingStateCommand => new RelayCommand(async (o) => { EditingState(); });
        private void EditingState() {
            CurrentTpaTrustState = TpaTrustState.Editing;
        }
        #endregion SetSearchingStateCommand

        #region SetSearchingState
        public RelayCommand SetSearchingStateCommand => new RelayCommand(async (o) => { await SearchingState(); });
        private async Task SearchingState() {
            CurrentTpaTrustState = TpaTrustState.Searching;
            if (string.IsNullOrWhiteSpace(TrustAccountNumber)) { return; }
            var isValid = await ValidateTrustAccountAsync(TrustAccountNumber);
            TrustName = GetTrustName(TrustAccountNumber);
            CurrentTpaTrustState = TpaTrustState.Validating;
        }
        #endregion SetSearchingState

        #region SetValidatingState
        public RelayCommand SetValidatingTrustAccountStateCommand => new RelayCommand(async (o) => { await ValidatingTrustAccountState(); });
        private async Task ValidatingTrustAccountState() {
            if (string.IsNullOrWhiteSpace(TrustAccountNumber)) { return; }
            await ValidateTrustAccountAsync(TrustAccountNumber);
        }
        #endregion SetValidatingState

        #region RemoveBindingCommand
        public RelayCommand RemoveBindingCommand => new RelayCommand(async (o) => { await RemoveBinding(); });
        private async Task RemoveBinding() {
            // mimicking delayed response by service call
            var hash = Math.Abs(TrustName!.GetHashCode());
            var delayResponse = Math.DivRem(hash, 10).Remainder;
            await Task.Delay(delayResponse * 100);

            TrustName = null;
            CurrentTpaTrustState = TpaTrustState.Unbound;
        }
        #endregion RemoveBindingCommand
        #endregion Commands

        #region VisiblityControls
        public string UnboundVisibility => CurrentTpaTrustState == TpaTrustState.Unbound ? nameof(Visibility.Visible) : nameof(Visibility.Hidden);
        public string EditingVisibility => CurrentTpaTrustState is TpaTrustState.Editing or TpaTrustState.Searching
            ? nameof(Visibility.Visible) : nameof(Visibility.Collapsed);
        public string EditingVisibilityHeader => EditingVisibility == nameof(Visibility.Visible) ? nameof(Visibility.Visible) : nameof(Visibility.Hidden);
        public string SearchingVisibility => CurrentTpaTrustState == TpaTrustState.Searching ? nameof(Visibility.Visible) : nameof(Visibility.Collapsed);
        public string TrustNameVisibility => CurrentTpaTrustState == TpaTrustState.Validating ? nameof(Visibility.Visible) : nameof(Visibility.Hidden);
        public string BoundedVisibility => CurrentTpaTrustState == TpaTrustState.Bounded ? nameof(Visibility.Visible) : nameof(Visibility.Hidden);
        public string TrustAccountBoxVisibility => CurrentTpaTrustState != TpaTrustState.Unbound ? nameof(Visibility.Visible) : nameof(Visibility.Hidden);
        public string RemoveAccountVisibility => CurrentTpaTrustState == TpaTrustState.Validating && !string.IsNullOrWhiteSpace(TrustAccountNumber)
            ? nameof(Visibility.Visible) : nameof(Visibility.Hidden);

        #endregion VisiblityControls

        #region Properties
        #region TrustAccountNumber
        private string? _trustAccountNumber;
        public string? TrustAccountNumber {
            get => _trustAccountNumber;
            set => SetProperty(ref _trustAccountNumber, value);
        }
        #endregion TrustAccountNumber

        #region TrustName
        private string? _trustName;
        public string? TrustName {
            get => _trustName;
            set => SetProperty(ref _trustName, value);
        }
        #endregion TrustName

        #region TrustAccountNumberIsValid
        private string? _trustAccountNumberIsValid;
        public string? TrustAccountNumberIsValid {
            get => _trustAccountNumberIsValid;
            set => SetProperty(ref _trustAccountNumberIsValid, value);
        }
        #endregion TrustAccountNumberIsValid

        #region TpaAccountNumber
        private string? _tpaAccountNumber;
        public string? TpaAccountNumber {
            get => _tpaAccountNumber;
            set => SetProperty(ref _tpaAccountNumber, value);
        }
        #endregion TpaAccountNumber

        #region CurrentTpaViewModel
        public TpaViewModel CurrentTpaViewModel {
            get {
                if (field == null) { CurrentTpaViewModel = new TpaViewModel(); }
                return field!;
            }
            set => SetProperty(ref field, value);
        }
        #endregion CurrentTpaViewModel

        #endregion Properties

        #region constructors
        public TpaViewModel() {
            this.PropertyChanged += MainViewModel_PropertyChanged;
        }

        private void MainViewModel_PropertyChanged(object? sender, PropertyChangedEventArgs e) {
            switch (e.PropertyName) {
                case nameof(CurrentTpaTrustState):
                    RaisePropertyChanged(nameof(UnboundVisibility));
                    RaisePropertyChanged(nameof(EditingVisibility));
                    RaisePropertyChanged(nameof(EditingVisibilityHeader));
                    RaisePropertyChanged(nameof(SearchingVisibility));
                    RaisePropertyChanged(nameof(TrustNameVisibility));
                    RaisePropertyChanged(nameof(BoundedVisibility));
                    RaisePropertyChanged(nameof(TrustAccountBoxVisibility));
                    RaisePropertyChanged(nameof(RemoveAccountVisibility));
                    break;
                case nameof(TrustAccountNumber):
                    RaisePropertyChanged(nameof(RemoveAccountVisibility));
                    break;
            }

            ;
        }
        #endregion constructors

        #region Methods
        public bool ValidateTpaAccount() {
            var isValid = true;
            return isValid;
        }
        public async Task<bool?> ValidateTrustAccountAsync(string? account) {
            if (string.IsNullOrWhiteSpace(account)) { return null; }

            // mimicking delayed response by service call
            var hash = Math.Abs(account.GetHashCode());
            var delayResponse = Math.DivRem(hash, 10).Remainder;
            await Task.Delay(delayResponse * 100);

            // randomly determine valid vs invalid account by using the hash code of the input string to return a consistent result for the same input.
            var isValid = account.GetHashCode() % 2 == 0;
            return isValid;
        }

        public string? GetTrustName(string? trustAccountName) {
            string? trustName = null;
            trustName = "Northern Company/DTC" + Environment.NewLine + "Trust Number 2669";
            return trustName;
        }
        #endregion Methods
    }
}
