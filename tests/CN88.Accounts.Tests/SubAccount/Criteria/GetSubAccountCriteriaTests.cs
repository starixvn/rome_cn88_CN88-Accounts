namespace CN88.Accounts.Tests.SubAccount.Criteria
{
    using CN88.Accounts.SubAccount.Criteria;
    using Xunit;

    public class GetSubAccountCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsCorrectValue()
        {
            //// Arrange
            var criteria = new GetSubAccountCriteria(1, 1);

            //// Act & Assert
            Assert.Equal("CS_CustSubSelInfo", criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            //// Arrange
            var criteria = new GetSubAccountCriteria(custId, 1);

            //// Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidSubAccId_ReturnsFalse(int subAccId)
        {
            //// Arrange
            var criteria = new GetSubAccountCriteria(1, subAccId);

            //// Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            //// Arrange
            var criteria = new GetSubAccountCriteria(1, 1);

            //// Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}