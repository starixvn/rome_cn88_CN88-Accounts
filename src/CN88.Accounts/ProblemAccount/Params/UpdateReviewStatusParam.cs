namespace CN88.Accounts.ProblemAccount.Params
{
    using CN88.Accounts.Shared.Params;

    public class UpdateReviewStatusParam : LogParam
    {
        public UpdateReviewStatusParam(
            int userId,
            int subAccId,
            byte roleId,
            bool isAllDownline,
            bool isAffectedAllDownlines,
            string selectedDownlineIds,
            string languageKey,
            string loginIP)
            : base(userId, languageKey, loginIP)
        {
            SubAccId = subAccId;
            RoleId = roleId;
            IsAllDownline = isAllDownline;
            IsAffectedAllDownlines = isAffectedAllDownlines;
            SelectedDownlineIds = selectedDownlineIds;
        }

        public int SubAccId { get; }

        public byte RoleId { get; }

        public bool IsAllDownline { get; }

        public bool IsAffectedAllDownlines { get; }

        public string SelectedDownlineIds { get; }
    }
}