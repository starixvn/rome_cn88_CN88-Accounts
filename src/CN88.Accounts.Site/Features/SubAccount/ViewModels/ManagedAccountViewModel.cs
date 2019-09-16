namespace CN88.Accounts.Site.Features.SubAccount.ViewModels
{
    using System.Collections.Generic;
    using CN88.Accounts.SubAccount.Models;

    public class ManagedAccountViewModel
    {
        public ManagedAccountViewModel(
            bool isAllDownline,
            IEnumerable<ManagedAccount> managedAccounts)
        {
            IsAllDownline = isAllDownline;
            ManagedAccounts = managedAccounts;
        }

        public bool IsAllDownline { get; }

        public IEnumerable<ManagedAccount> ManagedAccounts { get; }
    }
}