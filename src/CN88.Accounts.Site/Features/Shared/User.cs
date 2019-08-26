namespace CN88.Accounts.Site.Features.Shared
{
    using CN88.Accounts.Shared.Models;
    using Sunplus.Agent.Model.Entities;

    public class User : IUser
    {
        private readonly IUserSession userSession;

        public User(IUserSession userSession)
        {
            this.userSession = userSession;
        }

        public int Id => userSession.CustId;

        public string Name => userSession.UserName;

        public string NickName => userSession.NickName;

        public string Language => userSession.Language;

        public string LanguageKey => userSession.LangKey;

        public byte LevelId => userSession.RoleId;

        public bool IsInternal => userSession.IsInternal;

        public int SubAccountId => userSession.SubAccId;

        public bool IsSubAccount => userSession.IsSubAcc;

        public bool IsAllDownline => userSession.GetProperty<bool>("ISALLDOWNLINE");

        public string IP => userSession.UserLoginIp;

        public bool AccountManagementEditView => userSession.SubPageAccess.UpdateAccount;

        public bool AccountManagementViewOnly => userSession.SubPageAccess.ViewMemberInfo;
    }
}
