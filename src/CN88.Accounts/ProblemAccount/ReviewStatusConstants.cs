namespace CN88.Accounts.ProblemAccount
{
    using System.Collections.Generic;

    public static class ReviewStatusConstants
    {
        public const byte All = 2;
        public const byte Reviewed = 1;
        public const byte NotReviewed = 0;

        public static IDictionary<byte, string> Items { get; } = new Dictionary<byte, string>
        {
            [All] = nameof(All),
            [Reviewed] = nameof(Reviewed),
            [NotReviewed] = nameof(NotReviewed)
        };
    }
}
