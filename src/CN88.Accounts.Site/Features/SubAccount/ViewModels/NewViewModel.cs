namespace CN88.Accounts.Site.Features.SubAccount.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using CN88.Accounts.Site.Features.SubAccount.Requests;
    using CN88.Accounts.SubAccount.Constants;
    using CN88.Accounts.SubAccount.Models;

    public class NewViewModel
    {
        public NewViewModel(
            NewQuery query,
            string hashKey,
            Tuple<bool, IEnumerable<ManagedAccount>> managedAccounts)
        {
            HashKey = hashKey;
            SubAccountName = query.ViewerName + SubAccountConstants.SubAccountPadding;
            ShowAccountManagementPermission = query.ShowAccountManagementPermission;
            NumberOfDownline = managedAccounts.Item2.Count();
            ManagedAccountViewModel = new ManagedAccountViewModel(managedAccounts.Item1, managedAccounts.Item2);
        }

        public string SubAccountName { get; }

        public string HashKey { get; }

        public bool ShowAccountManagementPermission { get; }

        public int NumberOfDownline { get; }

        public ManagedAccountViewModel ManagedAccountViewModel { get; }

        public IEnumerable<SelectListItem> Suffix1
            => new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" }
            .Select(item => new SelectListItem { Value = item, Text = item, Selected = item == "0" });

        public IEnumerable<SelectListItem> Suffix2
            => new[] { "1", "2", "3", "4", "5", "6", "7", "8", "9", "0" }
            .Select(item => new SelectListItem { Value = item, Text = item, Selected = item == "1" });
    }
}