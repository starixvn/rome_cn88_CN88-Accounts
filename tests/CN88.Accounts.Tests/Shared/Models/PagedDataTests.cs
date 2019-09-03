namespace CN88.Accounts.Tests.Shared.Models
{
    using System.Linq;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.Shared.Models;
    using Xunit;

    public class PagedDataTests
    {
        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(int.MinValue)]
        public void Any_LessThan1Record_ReturnsFalse(int totalRecords)
        {
            // Arrange
            var reportData = new PagedData<ProblemAccount>(Enumerable.Empty<ProblemAccount>(), 0, 0, totalRecords);

            // Act
            var actual = reportData.Any();

            // Assert
            Assert.False(actual);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(int.MaxValue)]
        public void Any_AtLeast1Record_ReturnsTrue(int totalRecords)
        {
            // Arrange
            var reportData = new PagedData<ProblemAccount>(Enumerable.Empty<ProblemAccount>(), 0, 0, totalRecords);

            // Act
            var actual = reportData.Any();

            // Assert
            Assert.True(actual);
        }
    }
}
