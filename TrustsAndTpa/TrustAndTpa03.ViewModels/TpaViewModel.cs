using TrustAndTpa.TrustAndTpa03.ViewModels;
using TrustsAndTpa.TrustAndTpa12.Infrastructure;

namespace TrustsAndTpa.TrustAndTpa03.ViewModels {
    public class TpaViewModel : ViewModelBase {
        #region StatesModel
        public ViewsStateMachine StMch {
            get {
                if (field == null) { StMch = new ViewsStateMachine(); }
                return field!;
            }
            set => SetProperty(ref field, value);
        }
        #endregion StatesModel

        #region Commands
        #region SetEditingState
        public RelayCommand SetEditingStateCommand => new(async (o) => { EditingState(); });
        private void EditingState() {
            StMch.CurrentTpaTrustState = TpaTrustState.Editing;
        }
        #endregion SetEditingState

        #region SetSearchingState
        public RelayCommand SetSearchingStateCommand => new(async (o) => { await SearchingState(); });
        private async Task SearchingState() {
            StMch.CurrentTpaTrustState = TpaTrustState.Searching;
            if (string.IsNullOrWhiteSpace(StMch.AccountNumber)) { return; }
            var isValid = await ValidateTpaAccountAsync(StMch.AccountNumber);
            StMch.AccountName = GetAccountName(StMch.AccountNumber);
            StMch.CurrentTpaTrustState = TpaTrustState.Validating;
        }
        #endregion SetSearchingState

        #region SetValidatingState
        public RelayCommand SetValidatingTrustAccountStateCommand => new RelayCommand(async (o) => { await ValidatingTrustAccountState(); });
        private async Task ValidatingTrustAccountState() {
            if (string.IsNullOrWhiteSpace(StMch.AccountNumber)) { return; }
            await ValidateTpaAccountAsync(StMch.AccountNumber);
        }
        #endregion SetValidatingState

        #region RemoveBindingCommand
        public RelayCommand RemoveBindingCommand => new(async (o) => { await RemoveBinding(); });
        private async Task RemoveBinding() {
            // mimicking delayed response by service call
            var hash = Math.Abs(StMch.AccountName!.GetHashCode());
            var delayResponse = Math.DivRem(hash, 10).Remainder;
            await Task.Delay(delayResponse * 100);

            StMch.AccountName = null;
            StMch.CurrentTpaTrustState = TpaTrustState.Unbound;
        }
        #endregion RemoveBindingCommand
        #endregion Commands

        #region Properties

        #region IsBusy
        public bool IsBusy {
            get;
            set => SetProperty(ref field, value);
        }
        #endregion IsBusy
        #endregion Properties

        #region Methods
        public async Task<bool?> ValidateTpaAccountAsync(string? account) {
            if (string.IsNullOrWhiteSpace(account)) { return null; }

            // mimicking delayed response by service call
            try {
                IsBusy = true;
                var hash = Math.Abs(account.GetHashCode());
                var delayResponse = Math.DivRem(hash, 30).Remainder;
                await Task.Delay(delayResponse * 100);
            }
            catch (Exception) { throw; }
            finally { IsBusy = false; }

            // randomly determine valid vs invalid account by using the hash code of the input string to return a consistent result for the same input.
            var isValid = account.GetHashCode() % 2 == 0;
            return isValid;
        }

        public string? GetAccountName(string? trustAccountName) {
            string? tpaName = null;
            tpaName = "Northern Company/DTC" + Environment.NewLine + "Trust Number 2669";
            return tpaName;
        }
        #endregion Methods
    }
}
