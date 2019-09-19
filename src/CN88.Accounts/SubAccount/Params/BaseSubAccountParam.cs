using CN88.Accounts.Shared.Params;

namespace CN88.Accounts.SubAccount.Params
{
    public class BaseSubAccountParam : LogParam
    {
        protected BaseSubAccountParam(
            string name,
            string pageAccess,
            string languageKey,
            string loginIP,
            int userId,
            byte roleId,
            bool isAllDownline,
            string downlineId)
            : base(userId, languageKey, loginIP)
        {
            Name = name;
            PageAccess = pageAccess;
            RoleId = roleId;
            IsAllDownline = isAllDownline;
            DownlineId = downlineId;
        }

        public string Name { get; }

        public string PageAccess { get; set; }

        public byte RoleId { get; }

        public bool IsAllDownline { get; }

        public string DownlineId { get; }
    }
}