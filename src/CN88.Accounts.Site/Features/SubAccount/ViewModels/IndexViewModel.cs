namespace CN88.Accounts.Site.Features.SubAccount.ViewModels
{
    using System.Collections.Generic;
    using CN88.Accounts.SubAccount.Models;
    using CN88.Web.Helpers;
    using Shared.Helpers;

    public class IndexViewModel
    {
        private readonly BrowserHelper browserHelper;

        public IndexViewModel(
            IEnumerable<SubAccountItem> subAccounts,
            bool showActionButtons,
            bool showAccountManagementPermission,
            BrowserHelper browserHelper)
        {
            this.browserHelper = browserHelper;

            SubAccounts = subAccounts;
            ShowActionButtons = showActionButtons;
            ShowAccountManagementPermission = showAccountManagementPermission;
        }

        public IEnumerable<SubAccountItem> SubAccounts { get; }

        public bool ShowActionButtons { get; }

        public bool ShowAccountManagementPermission { get; }

        public int TotalColumns
        {
            get
            {
                var totalColumns = ShowActionButtons ? 9 : 8;

                return ShowAccountManagementPermission ? totalColumns : (totalColumns - 2);
            }
        }

        public bool IsDevice => browserHelper.IsMobile();

        public string NewSubAccountLink => UrlHelper.MapAccountsPath("~/SubAccount/New");

        public string EditSubAccountLink => UrlHelper.MapAccountsPath("~/SubAccount/Edit");

        public string UnlockSubAccountLink => UrlHelper.MapAccountsPath("~/SubAccount/Unlock");

        public string DeleteSubAccountLink => UrlHelper.MapAccountsPath("~/SubAccount/Delete");

        public string ResetPasswordLink => UrlHelper.MapMainPath("~/Password/ResetSubAccountPassword");

        public string ResetSecurityCodeLink => UrlHelper.MapMainPath("~/SecurityCode/ResetSubAccountSecurityCode");

        public string VerifySecurityCodeLink => UrlHelper.MapMainPath("~/SecurityCode/VerifySecurityCode");
    }
}