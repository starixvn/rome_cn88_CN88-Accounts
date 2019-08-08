namespace CN88.Accounts.Shared.Constants
{
    using System.Collections.Generic;

    public static class CustomerStatusConstants
    {
        public const byte All = 0;
        public const byte Active = 4;
        public const byte ViewOnly = 3;
        public const byte Suspended = 2;
        public const byte Inactive = 1;

        public static IDictionary<byte, string> Items { get; } = new Dictionary<byte, string>
        {
            [All] = nameof(All),
            [Active] = nameof(Active),
            [ViewOnly] = nameof(ViewOnly),
            [Suspended] = nameof(Suspended),
            [Inactive] = nameof(Inactive)
        };
    }
}
