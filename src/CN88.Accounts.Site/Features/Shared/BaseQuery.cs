namespace CN88.Accounts.Site.Features.Shared
{
    using CN88.Accounts.Shared.Models;
    using CN88.Web.Models;

    public class BaseQuery
    {
        public BaseQuery(IUser viewer)
        {
            ViewerId = viewer.Id;
            ViewerName = viewer.Name;
            NickName = viewer.NickName;
            ViewerLevelId = viewer.LevelId;
            SubAccountId = viewer.SubAccountId;
            IsSubAccount = viewer.IsSubAccount;
            IsInternal = viewer.IsInternal;
            IP = viewer.IP;
            LanguageKey = viewer.LanguageKey;
            Language = viewer.Language;
            AccountManagementEditView = viewer.AccountManagementEditView;
        }

        public int CustId { get; set; }

        public string CustName { get; set; }

        public byte CustLevelId { get; set; }

        public virtual UserLevel CustLevel => Enumeration.FromValue<UserLevel>(CustLevelId);

        public int ViewerId { get; }

        public string ViewerName { get; }

        public string NickName { get; }

        public byte ViewerLevelId { get; }

        public virtual UserLevel ViewerLevel => Enumeration.FromValue<UserLevel>(ViewerLevelId);

        public int SubAccountId { get; }

        public bool IsSubAccount { get; }

        public bool AccountManagementEditView { get; }

        public bool IsInternal { get; }

        public string IP { get; }

        public string Language { get; }

        public string LanguageKey { get; }

        public virtual bool IsValid()
            => ViewerId > 0
            && !string.IsNullOrWhiteSpace(ViewerName)
            && !string.IsNullOrWhiteSpace(Language);

        public virtual string ErrorMessage { get; set; }
    }
}
