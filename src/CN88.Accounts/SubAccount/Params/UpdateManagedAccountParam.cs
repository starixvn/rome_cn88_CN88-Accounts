namespace CN88.Accounts.SubAccount.Params
{
    using CN88.Accounts.Shared.Params;

    public class UpdateManagedAccountParam : LogParam
    {
        public UpdateManagedAccountParam(
            int custId,
            byte roleId,
            int subAccId,
            bool isAllDownline,
            string downlineId,
            string languageKey,
            string loginIP,
            int userId)
            : base(userId, languageKey, loginIP)
        {
            CustId = custId;
            RoleId = roleId;
            SubAccId = subAccId;
            IsAllDownline = isAllDownline;
            DownlineId = downlineId;
        }

        public int CustId { get; }

        public byte RoleId { get; }

        public int SubAccId { get; }

        public bool IsAllDownline { get; }

        public string DownlineId { get; }
    }
}