namespace CN88.Accounts.Tests.SubAccount.Models
{
    using CN88.Accounts.SubAccount.Models;
    using Xunit;

    public class ManagedAccountTests
    {
        [Fact]
        public void Constructor_Always_ReturnsCorrectValues()
        {
            // Act
            var manangeAccount = new ManagedAccount(1, "username", "firstname", true);

            // Assert
            Assert.True(manangeAccount.CustId == 1
                && manangeAccount.Username == "username"
                && manangeAccount.FirstName == "firstname"
                && manangeAccount.Assigned);
        }
    }
}