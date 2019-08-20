namespace CN88.Accounts.Site.Features.Shared
{
    using CN88.Accounts.Shared.Models;
    using CN88.Common.Models;

    public class BaseQuery
    {
        private readonly IUser viewer;

        public BaseQuery(IUser viewer)
        {
            this.viewer = viewer;
        }

        public int CustId { get; set; }

        public string CustName { get; set; }

        public byte CustLevelId { get; set; }

        public virtual UserLevel CustLevel => Enumeration.FromValue<UserLevel>(CustLevelId);

        public int ViewerId => viewer.Id;

        public string ViewerName => viewer.Name;

        public string NickName => viewer.NickName;

        public byte ViewerLevelId => viewer.LevelId;

        public virtual UserLevel ViewerLevel => Enumeration.FromValue<UserLevel>(ViewerLevelId);

        public int SubAccountId => viewer.SubAccountId;

        public bool IsSubAccount => viewer.IsSubAccount;

        public bool IsAllDownline => viewer.IsAllDownline;

        public bool AccountManagementEditView => viewer.AccountManagementEditView;

        public bool IsInternal => viewer.IsInternal;

        public string IP => viewer.IP;

        public string Language => viewer.Language;

        public string LanguageKey => viewer.LanguageKey;

        public virtual bool IsValid()
            => ViewerId > 0
            && !string.IsNullOrWhiteSpace(ViewerName)
            && !string.IsNullOrWhiteSpace(Language);
    }
}
