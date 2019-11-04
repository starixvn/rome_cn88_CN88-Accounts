using System;
using CN88.Accounts.ProblemAccount.Criteria;
using Xunit;

namespace CN88.Accounts.Tests.ProblemAccount.Criteria
{
    public class GetProblemAccountsCriteriaTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsCorrectSettingKey()
        {
            // Arrange
            var criteria = new GetProblemAccountsCriteria(1, 4, 0, true, string.Empty, 3, string.Empty, 0, DateTime.Now, DateTime.Now, 1, 10);

            // Act & Assert
            Assert.Equal("CS_ProblemAccount_CustomerList", criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            // Arrange
            var criteria = new GetProblemAccountsCriteria(custId, 4, 0, true, string.Empty, 3, string.Empty, 0, DateTime.Now, DateTime.Now, 1, 10);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Theory]
        [InlineData(-1)]
        public void IsValid_InvalidSubAccId_ReturnsFalse(int subAccId)
        {
            // Arrange
            var criteria = new GetProblemAccountsCriteria(1, 4, subAccId, true, string.Empty, 3, string.Empty, 0, DateTime.Now, DateTime.Now, 1, 10);

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var criteria = new GetProblemAccountsCriteria(1, 4, 0, true, string.Empty, 3, string.Empty, 0, DateTime.Now, DateTime.Now, 1, 10);

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}