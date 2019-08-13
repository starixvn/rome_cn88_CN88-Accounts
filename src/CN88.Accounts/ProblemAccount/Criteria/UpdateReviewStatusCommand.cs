namespace CN88.Accounts.ProblemAccount.Criteria
{
    using Fanex.Data.Repository;

    public class UpdateReviewStatusCommand : NonQueryCommand
    {
        public UpdateReviewStatusCommand(
            int userId,
            int subAccId,
            byte roleId,
            bool isAllDownline,
            string downlineList,
            string language,
            string loginIP)
        {
            UserId = userId;
            SubAccId = subAccId;
            RoleId = roleId;
            IsAllDownline = isAllDownline;
            DownlineList = downlineList;
            Language = language;
            LoginIP = loginIP;
        }

        public int UserId { get; }

        public int SubAccId { get; }

        public byte RoleId { get; }

        public bool IsAllDownline { get; }

        public string DownlineList { get; }

        public string Language { get; }

        public string LoginIP { get; }

        public int ErrCode { get; set; }

        public string ErrMsg { get; set; }

        public override string GetSettingKey() => "CS_ProblemAccount_ReviewStatusUpd";

        public override bool IsValid()
            => UserId > 0 && SubAccId > 0 && !string.IsNullOrWhiteSpace(DownlineList);
    }
}
