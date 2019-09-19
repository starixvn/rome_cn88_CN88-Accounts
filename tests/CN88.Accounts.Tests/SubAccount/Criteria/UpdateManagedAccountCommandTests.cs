namespace CN88.Accounts.Tests.SubAccount.Criteria
{
    using CN88.Accounts.SubAccount.Criteria;
    using Xunit;

    public class UpdateManagedAccountCommandTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsCorrectValue()
        {
            //// Arrange
            var command = new UpdateManagedAccountCommand(1, 1, 1, true, string.Empty, "E", "127.0.0.1", 1);

            //// Act & Assert
            Assert.Equal("CS_SubAccManagementUpd", command.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            //// Arrange
            var command = new UpdateManagedAccountCommand(custId, 1, 1, true, string.Empty, "E", "127.0.0.1", 1);

            //// Act & Assert
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData(0)]
        public void IsValid_InvalidRoleId_ReturnsFalse(byte roleId)
        {
            //// Arrange
            var command = new UpdateManagedAccountCommand(1, roleId, 1, true, string.Empty, "E", "127.0.0.1", 1);

            //// Act & Assert
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidSubAccId_ReturnsFalse(int subAccId)
        {
            //// Arrange
            var command = new UpdateManagedAccountCommand(1, 1, subAccId, true, string.Empty, "E", "127.0.0.1", 1);

            //// Act & Assert
            Assert.False(command.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            //// Arrange
            var command = new UpdateManagedAccountCommand(1, 1, 1, true, string.Empty, "E", "127.0.0.1", 1);

            //// Act & Assert
            Assert.True(command.IsValid());
        }
    }
}