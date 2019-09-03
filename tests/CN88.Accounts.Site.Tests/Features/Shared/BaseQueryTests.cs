namespace CN88.Accounts.Site.Tests.Features.Shared
{
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.Shared;
    using NSubstitute;
    using Xunit;

    public class BaseQueryTests
    {
        private readonly IUser viewer;

        public BaseQueryTests()
        {
            viewer = Substitute.For<IUser>();
        }

        [Fact]
        public void AccountManagementEditView_Always_ReturnsCorrectValue()
        {
            // Arrange
            viewer.AccountManagementEditView.Returns(true);

            // Act 
            var query = new BaseQuery(viewer);

            // Assert
            Assert.Equal(query.AccountManagementEditView, viewer.AccountManagementEditView);
        }
    }
}
