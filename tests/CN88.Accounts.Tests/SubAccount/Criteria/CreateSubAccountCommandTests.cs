namespace CN88.Accounts.Tests.SubAccount.Criteria
{
    using CN88.Accounts.SubAccount.Criteria;
    using Xunit;

    public class CreateSubAccountCommandTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsCorrectValue()
        {
            //// Arrange
            var command = new CreateSubAccountCommand(1, "name", "username", "password", "[A],[B]", "E", "127.0.0.1", 1);

            //// Act & Assert
            Assert.Equal("CS_CustSubIns", command.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            //// Arrange
            var command = new CreateSubAccountCommand(custId, "name", "username", "password", "[A],[B]", "E", "127.0.0.1", 1);

            //// Act & Assert
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidUserId_ReturnsFalse(int userId)
        {
            //// Arrange
            var command = new CreateSubAccountCommand(1, "name", "username", "password", "[A],[B]", "E", "127.0.0.1", userId);

            //// Act & Assert
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void IsValid_InvalidUsername_ReturnsFalse(string username)
        {
            //// Arrange
            var command = new CreateSubAccountCommand(1, "name", username, "password", "[A],[B]", "E", "127.0.0.1", 1);

            //// Act & Assert
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void IsValid_InvalidUserPwd_ReturnsFalse(string userPwd)
        {
            //// Arrange
            var command = new CreateSubAccountCommand(1, "name", "username", userPwd, "[A],[B]", "E", "127.0.0.1", 1);

            //// Act & Assert
            Assert.False(command.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            //// Arrange
            var command = new CreateSubAccountCommand(1, "name", "username", "password", "[A],[B]", "E", "127.0.0.1", 1);

            //// Act & Assert
            Assert.True(command.IsValid());
        }
    }
}