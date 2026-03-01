namespace TrustsAndTpa.TrustsAndTpa03.ViewModels;

public class MainViewModel : ViewModelBase
{
    #region CurrentTpaViewModel
    public TrustViewModel CurrentTrustViewModel {
        get {
            if (field == null) { CurrentTrustViewModel = new TrustViewModel(); }
            return field!;
        }
        set => SetProperty(ref field, value);
    }
    #endregion CurrentTpaViewModel

    #region CurrentTpaViewModel
    public TpaViewModel CurrentTpaViewModel {
        get {
            if (field == null) { CurrentTpaViewModel = new TpaViewModel(); }
            return field!;
        }
        set => SetProperty(ref field, value);
    }
    #endregion CurrentTpaViewModel
}
