using CN88.Accounts.ProblemAccount.Criteria;
using Xunit;

namespace CN88.Accounts.Tests.ProblemAccount.Criteria
{
    public class UpdateReviewStatusCommandTests
    {
        [Fact]
        public void GetSettingKey_Always_ReturnsCorrectSettingKey()
        {
            // Arrange
            var criteria = new UpdateReviewStatusCommand(1, 0, 3, true, false, string.Empty, "E", "127.0.0.1");

            // Act & Assert
            Assert.Equal("CS_ProblemAccount_ReviewStatusUpd", criteria.GetSettingKey());
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void IsValid_InvalidCustId_ReturnsFalse(int custId)
        {
            // Arrange
            var criteria = new UpdateReviewStatusCommand(custId, 0, 3, true, false, string.Empty, "E", "127.0.0.1");

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Theory]
        [InlineData(-1)]
        public void IsValid_InvalidSubAccId_ReturnsFalse(int subAccId)
        {
            // Arrange
            var criteria = new UpdateReviewStatusCommand(1, subAccId, 3, true, false, string.Empty, "E", "127.0.0.1");

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void IsValid_InvalidDownlineList_ReturnsFalse(string downlinelist)
        {
            // Arrange
            var criteria = new UpdateReviewStatusCommand(1, 0, 3, true, false, downlinelist, "E", "127.0.0.1");

            // Act & Assert
            Assert.False(criteria.IsValid());
        }

        [Fact]
        public void IsValid_ValidCriteria_ReturnsTrue()
        {
            // Arrange
            var criteria = new UpdateReviewStatusCommand(1, 0, 3, true, false, "1,2,3", "E", "127.0.0.1");

            // Act & Assert
            Assert.True(criteria.IsValid());
        }
    }
}