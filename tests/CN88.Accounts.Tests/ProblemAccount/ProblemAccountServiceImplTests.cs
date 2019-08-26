namespace CN88.Accounts.Tests.ProblemAccount
{
    using System;
    using System.Collections.Generic;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.ProblemAccount.Criteria;
    using CN88.Accounts.ProblemAccount.Params;
    using CN88.Accounts.Shared.Models;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using Xunit;

    public class ProblemAccountServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly ProblemAccountServiceImpl problemAccountService;

        public ProblemAccountServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            problemAccountService = new ProblemAccountServiceImpl(dynamicRepository);
        }

        [Fact]
        public void GetProblemAccounts_Always_CallsDynamicRepositoryFecthAsync()
        {
            // Arrange
            var param = new GetProblemAccountsParam(1, 0, true, string.Empty, 3, string.Empty, 0, DateTime.Now, DateTime.Now, 1, 10);

            // Act
            problemAccountService.GetProblemAccounts(param).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received().FetchAsync<ProblemAccount>(
                Arg.Is<GetProblemAccountsCriteria>(
                    c => c.CustId == param.CustId
                        && c.SubAccId == param.SubAccId
                        && c.IsAllDownline == param.IsAllDownline
                        && c.UplineUsername == param.UplineUsername
                        && c.SearchRoleId == param.CustLevelId
                        && c.SearchUsername == param.SearchUsername
                        && c.ReviewStatusId == param.ReviewStatus
                        && c.FromDate == param.FromDate
                        && c.ToDate == param.ToDate
                        && c.PageIndex == param.PageIndex
                        && c.PageSize == param.PageSize));
        }

        [Fact]
        public void GetProblemAccounts_Always_ReturnsCorrectResult()
        {
            // Arrange
            var param = new GetProblemAccountsParam(1, 0, true, string.Empty, 3, string.Empty, 0, DateTime.Now, DateTime.Now, 1, 10);
            var problemAccounts = new List<ProblemAccount>
            {
                new ProblemAccount(1,"UplineUsername","Username","Name","Nickname",5,1,DateTime.Now,false)
            };
            dynamicRepository.FetchAsync<ProblemAccount>(Arg.Any<GetProblemAccountsCriteria>()).Returns(problemAccounts);
            var expectedResult = new PagedData<ProblemAccount>(problemAccounts, 1, 10, 0);

            // Act
            var actualResult = problemAccountService.GetProblemAccounts(param).GetAwaiter().GetResult();

            // Assert
            Assert.True(new CompareLogic().Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void UpdateReviewStatus_Always_CallsDynamicRepositoryExecuteAsync()
        {
            // Arrange
            var param = new UpdateReviewStatusParam(1, 0, 3, true, false, "1,2", "E", "127.0.0.1");

            // Act
            problemAccountService.UpdateReviewStatus(param).ConfigureAwait(false);

            // Assert
            dynamicRepository.Received().ExecuteAsync(
                Arg.Is<UpdateReviewStatusCommand>(
                    c => c.UserId == param.UserId
                        && c.SubAccId == param.SubAccId
                        && c.RoleId == param.RoleId
                        && c.IsAllDownline == param.IsAllDownline
                        && c.IsAffectedAllDownlines == param.IsAffectedAllDownlines
                        && c.CustomerList == param.SelectedDownlineIds
                        && c.Language == param.Language
                        && c.LoginIP == param.LoginIP));
        }
    }
}
