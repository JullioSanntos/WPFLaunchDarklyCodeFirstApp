using TrustsAndTpa.TrustAndTpa12.Infrastructure;

namespace TrustsAndTpa.TrustAndTpa03.ViewModels; 
public class TrustViewModel : ViewModelBase {
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
    public RelayCommand SetEditingStateCommand => new RelayCommand(async (o) => { EditingState(); });
    private void EditingState() {
        StMch.CurrentTpaTrustState = TpaTrustState.Editing;
    }
    #endregion SetSearchingStateCommand

    #region SetSearchingState
    public RelayCommand SetSearchingStateCommand => new RelayCommand(async (o) => { await SearchingState(); });
    private async Task SearchingState() {
        StMch.CurrentTpaTrustState = TpaTrustState.Searching;

        if (string.IsNullOrWhiteSpace(StMch.AccountNumber)) { return; }
        StMch.AccountFound = await ValidateTrustAccountAsync(StMch.AccountNumber);
        if (StMch.AccountFound == true) {
            StMch.AccountName = GetTrustName(StMch.AccountNumber);
            StMch.CurrentTpaTrustState = TpaTrustState.Validating;
        }
        else { StMch.AccountName = null; StMch.CurrentTpaTrustState = TpaTrustState.Editing; }

    }
    #endregion SetSearchingState

    #region SetValidatingState
    public RelayCommand SetValidatingTrustAccountStateCommand => new RelayCommand(async (o) => { await ValidatingTrustAccountState(); });
    private async Task ValidatingTrustAccountState() {
        if (string.IsNullOrWhiteSpace(StMch.AccountNumber)) { return; }
        await ValidateTrustAccountAsync(StMch.AccountNumber);
    }
    #endregion SetValidatingState

    #region RemoveBindingCommand
    public RelayCommand RemoveBindingCommand => new RelayCommand(async (o) => { await RemoveBinding(); });
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

    #region TrustAccountNumberIsValid
    public string? TrustAccountNumberIsValid {
        get;
        set => SetProperty(ref field, value);
    }
    #endregion TrustAccountNumberIsValid

    #region CanSearch
    public bool CanSearch => !string.IsNullOrWhiteSpace(StMch.AccountNumber);

    #endregion CanSearch

    #region IsBusy
    public bool IsBusy {
        get;
        set => SetProperty(ref field, value);
    }
    #endregion IsBusy

    #endregion Properties

    #region Methods
    public bool ValidateTpaAccount() {
        var isValid = true;
        return isValid;
    }
    public async Task<bool?> ValidateTrustAccountAsync(string? account) {
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

    public string? GetTrustName(string? trustAccountName) {
        string? trustName = null;
        trustName = "Northern Company/DTC" + Environment.NewLine + "Trust Number 2669";
        return trustName;
    }
    #endregion Methods
}
