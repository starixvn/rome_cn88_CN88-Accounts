namespace CN88.Accounts.Site.Tests.Features.Shared
{
    using CN88.Accounts.Site.Features.Shared;
    using NSubstitute;
    using Sunplus.Agent.Model.Entities;
    using Xunit;

    public class UserTests
    {
        [Fact]
        public void Constructor_Always_GetUserDataFromSession()
        {
            // Arrange
            var userSession = Substitute.For<IUserSession>();
            userSession.CustId.Returns(1);
            userSession.UserName.Returns("username");
            userSession.NickName.Returns("nickname");
            userSession.RoleId.Returns((byte)2);
            userSession.Language.Returns("en-US");
            userSession.LangKey.Returns("en");
            userSession.IsInternal.Returns(false);
            userSession.SubAccId.Returns(3);
            userSession.IsSubAcc.Returns(true);
            userSession.UserLoginIp.Returns("127.0.0.1");
            userSession.GetProperty<bool>("ISALLDOWNLINE").Returns(true);
            userSession.GetProperty<byte>("CUSTOMERSTATUS").Returns((byte)1);

            var pageAccess = new PageAccess() { UpdateAccount = true };
            userSession.SubPageAccess.Returns(pageAccess);

            // Act
            var user = new User(userSession);

            // Assert
            Assert.True(
                user.Id == 1
                && user.Name == "username"
                && user.NickName == "nickname"
                && user.LevelId == 2
                && user.Language == "en-US"
                && user.LanguageKey == "en"
                && !user.IsInternal
                && user.SubAccountId == 3
                && user.IsSubAccount
                && user.IP == "127.0.0.1"
                && user.IsAllDownline
                && user.StatusId == 1
                && user.AccountManagementEditView);
        }
    }
}
