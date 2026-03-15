using TrustsAndTpa.TrustAndTpa12.Infrastructure;

namespace TrustsAndTpa.TrustAndTpa03.ViewModels;

public class MainViewModel : ViewModelBase {
    #region properties
    #region CurrentTrustViewModel
    public TrustViewModel CurrentTrustViewModel {
        get {
            if (field == null) { CurrentTrustViewModel = new TrustViewModel(); }
            return field!;
        }
        set => SetProperty(ref field, value);
    }
    #endregion CurrentTrustViewModel

    #region CurrentTpaViewModel
    public TpaViewModel CurrentTpaViewModel {
        get {
            if (field == null) { CurrentTpaViewModel = new TpaViewModel(); }
            return field!;
        }
        set => SetProperty(ref field, value);
    }
    #endregion CurrentTpaViewModel

    public bool CancelButtonIsEnable { get; set => SetProperty(ref field, value); } = false;
    public bool SaveButtonIsEnable { get; set => SetProperty(ref field, value); } = false;
    #endregion properties

    #region commands
    public RelayCommand CancelBindingCommand => new RelayCommand(async (o) => { CancelBinding(); });
    public RelayCommand SaveBindingCommand => new RelayCommand(async (o) => { SaveBinding(); });

    private void CancelBinding() {
        CurrentTrustViewModel.StMch.AccountName = CurrentTpaViewModel.StMch.AccountName = null;
        CurrentTrustViewModel.StMch.CurrentTpaTrustState = CurrentTpaViewModel.StMch.CurrentTpaTrustState = TpaTrustState.Unbound;
    }
    private void SaveBinding() {
        CurrentTrustViewModel.StMch.AccountName = CurrentTpaViewModel.StMch.AccountName = null;
        CurrentTrustViewModel.StMch.AccountNumber = CurrentTpaViewModel.StMch.AccountNumber = null;
        CurrentTrustViewModel.StMch.CurrentTpaTrustState = CurrentTpaViewModel.StMch.CurrentTpaTrustState = TpaTrustState.Unbound;
    }
    #endregion commands

    #region constructors
    public MainViewModel() {
        CurrentTrustViewModel!.StMch!.PropertyChanged += CurrentTrustTpaViewModel_PropertyChanged;
        CurrentTpaViewModel!.StMch!.PropertyChanged += CurrentTrustTpaViewModel_PropertyChanged;
    }
    private void CurrentTrustTpaViewModel_PropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
        if (e.PropertyName is (nameof(CurrentTrustViewModel.StMch.AccountName)) or (nameof(CurrentTpaViewModel.StMch.AccountName))) {
            if (CurrentTrustViewModel.StMch.AccountName != null || CurrentTpaViewModel.StMch.AccountName != null) {
                CancelButtonIsEnable = SaveButtonIsEnable = true;
            }
            else {
                CancelButtonIsEnable = SaveButtonIsEnable = false;
            }
        }
    }
    #endregion constructors



}

