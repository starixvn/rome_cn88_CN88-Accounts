namespace CN88.Accounts.Site.Tests.Features.ProblemAccount.Handlers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.ProblemAccount.Params;
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.ProblemAccount.Handler;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.ProblemAccount.ViewModels;
    using CN88.Accounts.Site.Tests.Fixtures;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using Xunit;

    [Collection("UserCollection")]
    public class ProblemAccountHandlerTests
    {
        private readonly UserFixture userFixture;
        private readonly ProblemAccountService problemAccountService;
        private readonly ProblemAccountHandler problemAccountHandler;
        private readonly CompareLogic compareLogic;

        public ProblemAccountHandlerTests(UserFixture userFixture)
        {
            this.userFixture = userFixture;
            compareLogic = new CompareLogic();
            problemAccountService = Substitute.For<ProblemAccountService>();
            problemAccountHandler = new ProblemAccountHandler(problemAccountService);
        }

        [Fact]
        public void HandleProblemAccountQuery_Always_CallsGetProblemAccounts()
        {
            // Arrange
            var message = new ProblemAccountQuery(userFixture.User);

            // Act
            problemAccountHandler.Handle(message).ConfigureAwait(false);

            // Assert
            problemAccountService.Received().GetProblemAccounts(
                Arg.Is<GetProblemAccountsParam>(
                    p => p.CustId == message.ViewerId
                        && p.SubAccId == message.SubAccountId
                        && p.IsAllDownline == message.IsAllDownline
                        && p.UplineUsername == message.UplineUsername
                        && p.CustLevelId == message.CustLevelId
                        && p.SearchUsername == message.CustName
                        && p.ReviewStatus == message.SelectedReviewStatus
                        && p.FromDate == message.FromDate
                        && p.ToDate == message.ToDate
                        && p.PageIndex == message.PageIndex
                        && p.PageSize == message.PageSize));
        }

        [Fact]
        public void HandleProblemAccountQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new ProblemAccountQuery(userFixture.User);

            var problemAccounts = new List<ProblemAccount>
            {
                new ProblemAccount()
            };
            var pagedData = new PagedData<ProblemAccount>(problemAccounts, 1, 10, 1);
            problemAccountService.GetProblemAccounts(Arg.Any<GetProblemAccountsParam>())
                .Returns(Task.FromResult(pagedData));

            var expectedResult = new IndexViewModel(message, pagedData);

            // Act
            var actualResult = problemAccountHandler.Handle(message).GetAwaiter().GetResult();

            // Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void HandleUpdateReviewStatusQuery_Always_ReturnsCorrectViewModel()
        {
            // Arrange
            var message = new UpdateReviewStatusQuery(userFixture.User)
            {
                SelectedDownlineIds = "1,2",
                SelectedDownlineUsernames = "username1,username2"
            };
            var expectedResult = new UpdateReviewStatusViewModel(message);

            // Act
            var actualResult = problemAccountHandler.Handle(message);

            // Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void HandleUpdateReviewStatusCommand_Always_CallsUpdateReviewStatus()
        {
            // Arrange
            var message = new UpdateReviewStatusCommand(userFixture.User);

            // Act
            problemAccountHandler.Handle(message).ConfigureAwait(false);

            // Assert
            problemAccountService.Received().UpdateReviewStatus(
                Arg.Is<UpdateReviewStatusParam>(
                    p => p.UserId == message.ViewerId
                        && p.SubAccId == message.SubAccountId
                        && p.RoleId == message.CustLevelId
                        && p.IsAllDownline == message.IsAllDownline
                        && p.IsAffectedAllDownlines == message.IsAffectedAllDownlines
                        && p.SelectedDownlineIds == message.SelectedDownlineIds
                        && p.LanguageKey == message.LanguageKey
                        && p.LoginIP == message.IP));
        }
    }
}
