namespace CN88.Accounts.ProblemAccount.Params
{
    public class UpdateReviewStatusParam
    {
        public UpdateReviewStatusParam(
            int userId,
            int subAccId,
            byte roleId,
            byte targetRoleId,
            bool isAllDownline,
            bool isAffectedAllDownlines,
            string selectedDownlineIds,
            string language,
            string loginIP)
        {
            UserId = userId;
            SubAccId = subAccId;
            RoleId = roleId;
            TargetRoleId = targetRoleId;
            IsAllDownline = isAllDownline;
            IsAffectedAllDownlines = isAffectedAllDownlines;
            SelectedDownlineIds = selectedDownlineIds;
            Language = language;
            LoginIP = loginIP;
        }

        public int UserId { get; }

        public int SubAccId { get; }

        public byte RoleId { get; }

        public byte TargetRoleId { get; }

        public bool IsAllDownline { get; }

        public bool IsAffectedAllDownlines { get; }

        public string SelectedDownlineIds { get; }

        public string Language { get; }

        public string LoginIP { get; }
    }
}