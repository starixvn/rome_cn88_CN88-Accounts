namespace CN88.Accounts.Shared.Extensions
{
    using System;

    public static class DateTimeExtensions
    {
        private const string ShortDate = "yyyy-MM-dd";

        public static string ToShortDate(this DateTime dateTime)
            => dateTime.ToString(ShortDate);
    }
}
