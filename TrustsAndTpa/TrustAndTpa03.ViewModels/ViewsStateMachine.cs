using TrustsAndTpa.TrustAndTpa12.Infrastructure;

namespace TrustsAndTpa.TrustAndTpa03.ViewModels;

public class ViewsStateMachine : ViewModelBase {

    #region CurrentTpaTrustState
    private TpaTrustState _currentTpaTrustState = TpaTrustState.Unbound;
    public TpaTrustState CurrentTpaTrustState {
        get => _currentTpaTrustState;
        set => SetProperty(ref _currentTpaTrustState, value);
    }
    #endregion CurrentTpaTrustState

    #region VisiblityControls
    public string UnboundVisibility => CurrentTpaTrustState == TpaTrustState.Unbound ? nameof(Visibility.Visible) : nameof(Visibility.Hidden);
    public string EditingVisibility => CurrentTpaTrustState is TpaTrustState.Editing or TpaTrustState.Searching
        ? nameof(Visibility.Visible) : nameof(Visibility.Collapsed);
    public string EditingVisibilityHeader => EditingVisibility == nameof(Visibility.Visible) ? nameof(Visibility.Visible) : nameof(Visibility.Hidden);
    public string SearchingVisibility => CurrentTpaTrustState == TpaTrustState.Searching ? nameof(Visibility.Visible) : nameof(Visibility.Collapsed);
    public string NameVisibility => CurrentTpaTrustState == TpaTrustState.Validating ? nameof(Visibility.Visible) : nameof(Visibility.Hidden);
    public string BoundedVisibility => CurrentTpaTrustState == TpaTrustState.Bounded && AccountFound == true
        ? nameof(Visibility.Visible) : nameof(Visibility.Hidden);
    public string AccountBoxVisibility => CurrentTpaTrustState != TpaTrustState.Unbound ? nameof(Visibility.Visible) : nameof(Visibility.Hidden);
    public string RemoveAccountVisibility => CurrentTpaTrustState == TpaTrustState.Validating && !string.IsNullOrWhiteSpace(AccountNumber)
        ? nameof(Visibility.Visible) : nameof(Visibility.Hidden);
    public string AccountNotFoundMessageVisibilty =>
        CurrentTpaTrustState == TpaTrustState.Searching && AccountFound == false
        ? nameof(Visibility.Visible) : nameof(Visibility.Collapsed);

    #endregion VisiblityControls

    #region properties
    #region AccountNumber
    private string? _accountNumber;
    public string? AccountNumber {
        get => _accountNumber;
        set => SetProperty(ref _accountNumber, value);
    }
    #endregion AccountNumber

    #region AccountName
    public string? AccountName {
        get;
        set => SetProperty(ref field, value);
    }
    #endregion AccountName

    #region CanSearch
    public bool CanSearch => !string.IsNullOrWhiteSpace(AccountNumber);
    #endregion CanSearch

    #region AccountFound
    public bool? AccountFound { get; set => SetProperty(ref field, value); }
    #endregion AccountFound

    #endregion properties

    #region constructor
    public ViewsStateMachine() {
        this.PropertyChanged += ViewsStateMachine_PropertyChanged;
    }

    private void ViewsStateMachine_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
        switch (e.PropertyName) {
            case nameof(CurrentTpaTrustState):
                RaisePropertyChanged(nameof(UnboundVisibility));
                RaisePropertyChanged(nameof(EditingVisibility));
                RaisePropertyChanged(nameof(EditingVisibilityHeader));
                RaisePropertyChanged(nameof(SearchingVisibility));
                RaisePropertyChanged(nameof(NameVisibility));
                RaisePropertyChanged(nameof(BoundedVisibility));
                RaisePropertyChanged(nameof(AccountBoxVisibility));
                RaisePropertyChanged(nameof(RemoveAccountVisibility));
                break;
            case nameof(AccountNumber):
                RaisePropertyChanged(nameof(RemoveAccountVisibility));
                RaisePropertyChanged(nameof(CanSearch));
                break;
            case nameof(AccountFound):
                RaisePropertyChanged(nameof(AccountNotFoundMessageVisibilty));
                break;
        }
    }
    #endregion constructor

}

