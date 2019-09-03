using CN88.Accounts.Site.Tests.Fixtures;
using Xunit;

namespace CN88.Accounts.Site.Tests.Collections
{
    [CollectionDefinition("UserCollection")]
    public class UserCollection : ICollectionFixture<UserFixture>
    {
    }
}
