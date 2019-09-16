namespace CN88.Accounts.SubAccount.Criteria
{
    public class UpdateSubAccountCommand : BaseSubAccountCommand
    {
        public UpdateSubAccountCommand(
            int custId,
            int subAccId,
            string name,
            string pageAccess,
            string languageKey,
            string loginIP,
            int userId)
            : base(custId, name, pageAccess, languageKey, loginIP, userId)
        {
            SubAccId = subAccId;
        }

        public int SubAccId { get; }

        public override string GetSettingKey()
            => "CS_CustSubUpd";

        public override bool IsValid()
            => base.IsValid() && SubAccId > 0;
    }
}