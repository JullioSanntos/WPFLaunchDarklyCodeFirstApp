using System;
using System.Collections.Generic;
using System.Text;
using TrustsAndTpa.TrustAndTpa12.Infrastructure;

namespace TrustsAndTpa.TrustAndTpa06.Models; 
public class MainModel : SetPropertyBase {

    #region properties
    #region OutgoingAccount
    public string? OutgoingAccount {
        get;
        set => SetProperty(ref field, value);
    }
    #endregion OutgoingAccount

    #region InboundAccount
    public string? InboundAccount {
        get;
        set => SetProperty(ref field, value);
    }
    #endregion InboundAccount
    #endregion properties

    #region methods
    public async Task<string?> GetAccountAsync(string accountNumber) { await Task.Delay(0); return null; }
    public async Task<bool> AddAccount(string accountNumber) { await Task.Delay(0); return false; }
    public async Task<bool> RemoveAccount(string accountNumber) { await Task.Delay(0); return false; }
    public async Task<bool> ValidateAccountAsync(string accountNumber) { await Task.Delay(0); return false; }
    public async Task<bool> UpdateAccounTask(string accountNumber) { await Task.Delay(0); return false; }
    #endregion methods

}
