namespace CN88.Accounts.ProblemAccount
{
    using System.Collections.Generic;

    public static class ReviewStatusConstants
    {
        public const int All = 0;
        public const int Reviewed = 1;
        public const int NotReviewed = 2;

        public static IDictionary<int, string> Items { get; } = new Dictionary<int, string>
        {
            [All] = nameof(All),
            [Reviewed] = nameof(Reviewed),
            [NotReviewed] = nameof(NotReviewed)
        };
    }
}
