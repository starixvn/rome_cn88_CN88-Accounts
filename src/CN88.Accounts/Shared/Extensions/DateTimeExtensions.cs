namespace CN88.Accounts.Shared.Extensions
{
    using System;

    public static class DateTimeExtensions
    {
        private const string ShortDateWithHyphen = "yyyy-MM-dd";
        private const string LongDateWithHyphen = "yyyy-MM-dd HH:mm:ss";

        public static string ToShortDateWithHyphen(this DateTime dateTime)
            => dateTime.ToString(ShortDateWithHyphen);

        public static string ToLongDateWithHyphen(this DateTime dateTime)
            => dateTime.ToString(LongDateWithHyphen);
    }
}
