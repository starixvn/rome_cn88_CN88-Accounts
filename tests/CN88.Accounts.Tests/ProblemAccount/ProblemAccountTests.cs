namespace CN88.Accounts.Tests.ProblemAccount
{
    using System;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.Shared;
    using CN88.Accounts.Shared.Constants;
    using Xunit;

    public class ProblemAccountTests
    {
        public ProblemAccountTests()
        {
            SystemTime.Today = new DateTime(2019, 8, 29);
        }

        [Fact]
        public void Constructor_Always_ReturnsCorrectValues()
        {
            // Act
            var problemAccount = new ProblemAccount(1, "uplineUsername", "username", "name", "nickname", 5, 1, SystemTime.Today, true);

            // Assert
            Assert.True(problemAccount.CustId == 1
                && problemAccount.UplineUsername == "uplineUsername"
                && problemAccount.Username == "username"
                && problemAccount.Name == "name"
                && problemAccount.Nickname == "nickname"
                && problemAccount.NumberOfDownline == 5
                && problemAccount.Status == 1
                && problemAccount.CreatedDate == SystemTime.Today
                && problemAccount.IsReviewed);
        }

        [Theory]
        [InlineData(CustomerStatusConstants.Active, "Active")]
        [InlineData(CustomerStatusConstants.Inactive, "Inactive")]
        [InlineData(CustomerStatusConstants.Suspended, "Suspended")]
        [InlineData(CustomerStatusConstants.ViewOnly, "View Only")]
        public void StatusName_Always_ReturnsCorrectStatusName(byte statusId, string statusName)
        {
            // Act
            var problemAccount = new ProblemAccount(1, "uplineUsername", "username", "name", "nickname", 5, statusId, SystemTime.Today, true);

            // Assert
            Assert.Equal(statusName, problemAccount.StatusName);
        }

        [Theory]
        [InlineData(true, "Reviewed")]
        [InlineData(false, "Not Reviewed")]
        public void ReviewStatus_Always_ReturnsCorrectReviewStatus(bool isReviewed, string reviewStatus)
        {
            // Act
            var problemAccount = new ProblemAccount(1, "uplineUsername", "username", "name", "nickname", 5, 1, SystemTime.Today, isReviewed);

            // Assert
            Assert.Equal(reviewStatus, problemAccount.ReviewStatus);
        }
    }
}
