using CN88.Accounts.Shared.Models;
using NSubstitute;

namespace CN88.Accounts.Site.Tests.Fixtures
{
    public class UserFixture
    {
        public UserFixture()
        {
            User = Substitute.For<IUser>();
            User.LevelId.Returns((byte)4);
            User.Id.Returns(10);
            User.Name.Returns("Name");
            User.Language.Returns("en-US");
        }

        public IUser User { get; }
    }
}
