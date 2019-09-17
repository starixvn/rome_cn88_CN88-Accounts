namespace CN88.Accounts.Shared.Params
{
    public class LogParam
    {
        public LogParam(
            int userId,
            string languageKey,
            string loginIP)
        {
            UserId = userId;
            LanguageKey = languageKey;
            LoginIP = loginIP;
        }

        public int UserId { get; }

        public string LanguageKey { get; }

        public string LoginIP { get; }
    }
}