namespace CN88.Accounts.SubAccount.Criteria
{
    public class CreateSubAccountCommand : BaseSubAccountCommand
    {
        public CreateSubAccountCommand(
            int custId,
            string name,
            string username,
            string password,
            string pageAccess,
            string languageKey,
            string loginIP,
            int userId)
            : base(custId, name, pageAccess, languageKey, loginIP, userId)
        {
            UserName = username;
            UserPwd = password;
        }

        public string UserName { get; }

        public string UserPwd { get; }

        public int SubAccId { get; set; }

        public override string GetSettingKey()
            => "CS_CustSubIns";

        public override bool IsValid()
            => base.IsValid()
            && !string.IsNullOrWhiteSpace(UserName)
            && !string.IsNullOrWhiteSpace(UserPwd);
    }
}