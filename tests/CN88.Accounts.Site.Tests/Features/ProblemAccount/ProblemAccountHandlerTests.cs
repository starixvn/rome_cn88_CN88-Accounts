using System.Collections.Generic;
using System.Threading.Tasks;
using CN88.Accounts.ProblemAccount;
using CN88.Accounts.ProblemAccount.Params;
using CN88.Accounts.Shared.Models;
using CN88.Accounts.Site.Features.ProblemAccount;
using CN88.Accounts.Site.Features.ProblemAccount.Requests;
using CN88.Accounts.Site.Features.ProblemAccount.ViewModels;
using KellermanSoftware.CompareNetObjects;
using NSubstitute;
using ONEbook.SpreadSheets;
using Xunit;

namespace CN88.Accounts.Site.Tests.Features.ProblemAccount
{
    public class ProblemAccountHandlerTests
    {
        private readonly IUser viewer;
        private readonly ProblemAccountService problemAccountService;
        private readonly Workbook<Accounts.ProblemAccount.ProblemAccount> workbook;
        private readonly ProblemAccountHandler problemAccountHandler;
        private readonly CompareLogic compareLogic;

        public ProblemAccountHandlerTests()
        {
            viewer = Substitute.For<IUser>();
            viewer.Id.Returns(1);
            viewer.Name.Returns("ViewerName");
            viewer.Language.Returns("en-US");

            problemAccountService = Substitute.For<ProblemAccountService>();
            workbook = Substitute.For<Workbook<Accounts.ProblemAccount.ProblemAccount>>();
            problemAccountHandler = new ProblemAccountHandler(problemAccountService, workbook);
            compareLogic = new CompareLogic();
        }

        [Fact]
        public void HandleProblemAccountQuery_Always_CallsGetProblemAccounts()
        {
            // Arrange
            var message = new ProblemAccountQuery(viewer);

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
                        && p.SearchUsername == message.SearchUsername
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
            var message = new ProblemAccountQuery(viewer);

            var problemAccounts = new List<Accounts.ProblemAccount.ProblemAccount>
            {
                new Accounts.ProblemAccount.ProblemAccount()
            };
            var pagedData = new PagedData<Accounts.ProblemAccount.ProblemAccount>(problemAccounts, 1, 10, 1);
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
            var message = new UpdateReviewStatusQuery(viewer);
            message.SelectedDownlineIds = "1,2";
            message.SelectedDownlineUsernames = "username1,username2";
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
            var message = new UpdateReviewStatusCommand(viewer);

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
                        && p.Language == message.LanguageKey
                        && p.LoginIP == message.IP));
        }
    }
}
