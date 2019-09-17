namespace CN88.Accounts.SubAccount.Params
{
    using CN88.Accounts.Shared.Params;

    public class UnlockSubAccountParam : LogParam
    {
        public UnlockSubAccountParam(
            int userId,
            int subAccId,
            string loginIP,
            string languageKey)
            : base(userId, languageKey, loginIP)
        {
            SubAccId = subAccId;
        }

        public int SubAccId { get; }
    }
}