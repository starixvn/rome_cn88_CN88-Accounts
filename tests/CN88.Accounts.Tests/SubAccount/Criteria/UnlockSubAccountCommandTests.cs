namespace CN88.Accounts.Tests.SubAccount.Criteria
{
    using CN88.Accounts.SubAccount.Criteria;
    using Xunit;

    public class UnlockSubAccountCommandTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsCorrectValue()
        {
            //// Arrange
            var command = new UnlockSubAccountCommand(1, 1, "127.0.0.1", "E");

            //// Act & Assert
            Assert.Equal("CS_CustSubUnlock", command.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            //// Arrange
            var command = new UnlockSubAccountCommand(custId, 1, "127.0.0.1", "E");

            //// Act & Assert
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidSubAccId_ReturnsFalse(int subAccId)
        {
            //// Arrange
            var command = new UnlockSubAccountCommand(1, subAccId, "127.0.0.1", "E");

            //// Act & Assert
            Assert.False(command.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            //// Arrange
            var command = new UnlockSubAccountCommand(1, 1, "127.0.0.1", "E");

            //// Act & Assert
            Assert.True(command.IsValid());
        }
    }
}