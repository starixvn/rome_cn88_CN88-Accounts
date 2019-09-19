namespace CN88.Accounts.Tests.SubAccount.Criteria
{
    using CN88.Accounts.SubAccount.Criteria;
    using Xunit;

    public class KickoutSubAccountCommandTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsCorrectValue()
        {
            //// Arrange
            var command = new KickoutSubAccountCommand(1, 1);

            //// Act & Assert
            Assert.Equal("Age_On_UserKickedout", command.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            //// Arrange
            var command = new KickoutSubAccountCommand(custId, 1);

            //// Act & Assert
            Assert.False(command.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidSubAccId_ReturnsFalse(int subAccId)
        {
            //// Arrange
            var command = new KickoutSubAccountCommand(1, subAccId);

            //// Act & Assert
            Assert.False(command.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            //// Arrange
            var command = new KickoutSubAccountCommand(1, 1);

            //// Act & Assert
            Assert.True(command.IsValid());
        }
    }
}