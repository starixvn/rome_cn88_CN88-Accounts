using System;
using CN88.Accounts.ProblemAccount;
using CN88.Accounts.ProblemAccount.Criteria;
using CN88.Accounts.ProblemAccount.Params;
using Fanex.Data.Repository;
using NSubstitute;
using Xunit;

namespace CN88.Accounts.Tests.ProblemAccount
{
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
            dynamicRepository.Received().FetchAsync<Accounts.ProblemAccount.ProblemAccount>(
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
