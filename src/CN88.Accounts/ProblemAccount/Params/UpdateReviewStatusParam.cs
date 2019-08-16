namespace CN88.Accounts.ProblemAccount.Params
{
    public class UpdateReviewStatusParam
    {
        public UpdateReviewStatusParam(
            int userId,
            int subAccId,
            byte roleId,
            bool isAllDownline,
            bool isAffectedAllDownlines,
            string downlineList,
            string language,
            string loginIP)
        {
            UserId = userId;
            SubAccId = subAccId;
            RoleId = roleId;
            IsAllDownline = isAllDownline;
            IsAffectedAllDownlines = isAffectedAllDownlines;
            DownlineList = downlineList;
            Language = language;
            LoginIP = loginIP;
        }

        public int UserId { get; }

        public int SubAccId { get; }

        public byte RoleId { get; }

        public bool IsAllDownline { get; }

        public bool IsAffectedAllDownlines { get; }

        public string DownlineList { get; }

        public string Language { get; }

        public string LoginIP { get; }
    }
}
