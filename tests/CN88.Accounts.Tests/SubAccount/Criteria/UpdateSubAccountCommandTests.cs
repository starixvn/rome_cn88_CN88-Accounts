namespace CN88.Accounts.Tests.SubAccount.Criteria
{
    using CN88.Accounts.SubAccount.Criteria;
    using Xunit;

    public class UpdateSubAccountCommandTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsCorrectValue()
        {
            //// Arrange
            var command = new UpdateSubAccountCommand(1, 1, "name", "[A],[B]", "E", "127.0.0.1", 1);

            //// Act & Assert
            Assert.Equal("CS_CustSubUpd", command.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            //// Arrange
            var command = new UpdateSubAccountCommand(custId, 1, "name", "[A],[B]", "E", "127.0.0.1", 1);

            //// Act & Assert
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidSubAccId_ReturnsFalse(int subAccId)
        {
            //// Arrange
            var command = new UpdateSubAccountCommand(1, subAccId, "name", "[A],[B]", "E", "127.0.0.1", 1);

            //// Act & Assert
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidUserId_ReturnsFalse(int userId)
        {
            //// Arrange
            var command = new UpdateSubAccountCommand(1, 1, "name", "[A],[B]", "E", "127.0.0.1", userId);

            //// Act & Assert
            Assert.False(command.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            //// Arrange
            var command = new UpdateSubAccountCommand(1, 1, "name", "[A],[B]", "E", "127.0.0.1", 1);

            //// Act & Assert
            Assert.True(command.IsValid());
        }
    }
}