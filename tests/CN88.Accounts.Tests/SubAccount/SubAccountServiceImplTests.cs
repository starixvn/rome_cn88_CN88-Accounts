namespace CN88.Accounts.Tests.SubAccount
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CN88.Accounts.Shared.Resources;
    using CN88.Accounts.SubAccount;
    using CN88.Accounts.SubAccount.Criteria;
    using CN88.Accounts.SubAccount.Models;
    using CN88.Accounts.SubAccount.Params;
    using CN88.Common.Models;
    using Fanex.Data.Repository;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using Xunit;

    public class SubAccountServiceImplTests
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly SubAccountValidator subAccountValidator;
        private readonly SubAccountServiceImpl subAccountService;
        private readonly CompareLogic compareLogic;

        public SubAccountServiceImplTests()
        {
            dynamicRepository = Substitute.For<IDynamicRepository>();
            subAccountValidator = Substitute.For<SubAccountValidator>();
            subAccountService = Substitute.ForPartsOf<SubAccountServiceImpl>(dynamicRepository, subAccountValidator);
            compareLogic = new CompareLogic();
        }

        [Fact]
        public void GetSubAccountsAsync_Always_CallsFetchAsync()
        {
            //// Arrange
            var custId = 1;

            //// Act
            subAccountService.GetSubAccountsAsync(custId).ConfigureAwait(false);

            //// Assert
            dynamicRepository.Received().FetchAsync<SubAccountItem>(Arg.Is<GetSubAccountListCriteria>(c => c.CustId == custId));
        }

        [Fact]
        public void GetSubAccountAsync_Always_CallsGetAsync()
        {
            //// Arrange
            var custId = 1;
            var subAccId = 2;
            dynamicRepository.GetAsync<SubAccountItem>(Arg.Any<GetSubAccountCriteria>())
                .Returns(Task.FromResult(new SubAccountItem(1, "username", "nickname", "firstname", DateTime.Now, string.Empty, false)));

            //// Act
            subAccountService.GetSubAccountAsync(custId, subAccId).ConfigureAwait(false);

            //// Assert
            dynamicRepository.Received().GetAsync<SubAccountItem>(
                Arg.Is<GetSubAccountCriteria>(c => c.CustId == custId && c.SubAccId == subAccId));
        }

        [Fact]
        public async Task UpdateSubAccountAsync_Always_CallsGetSubAccountAsync()
        {
            //// Arrange
            var param = new UpdateSubAccountParam(1, "name", "[A],[B]", "E", "127.0.0.1", 2, UserLevel.Agent.Value, true, string.Empty);
            subAccountService.GetSubAccountAsync(param.UserId, param.SubAccId).Returns(Task.FromResult(new SubAccountItem()));

            //// Act
            await subAccountService.UpdateSubAccountAsync(param);

            //// Assert
            await subAccountService.Received().GetSubAccountAsync(param.UserId, param.SubAccId);
        }

        [Fact]
        public async Task UpdateSubAccountAsync_SubAccountIsNull_ReturnsSuccessResult()
        {
            //// Arrange
            var param = new UpdateSubAccountParam(1, "name", "[A],[B]", "E", "127.0.0.1", 2, UserLevel.Agent.Value, true, string.Empty);
            dynamicRepository.GetAsync<SubAccountItem>(Arg.Any<GetSubAccountCriteria>()).Returns(Task.FromResult<SubAccountItem>(null));
            var expectedResult = Result.CreateSuccessResult(message: Resources.UpdateSuccessfully);

            //// Act
            var actualResult = await subAccountService.UpdateSubAccountAsync(param);

            //// Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void UnlockSubAccountAsync_Always_CallsExecuteAsync()
        {
            //// Arrange
            var param = new UnlockSubAccountParam(1, 2, "127.0.0.1", "E");

            //// Act
            subAccountService.UnlockSubAccountAsync(param).ConfigureAwait(false);

            //// Assert
            dynamicRepository.Received().ExecuteAsync(
                Arg.Is<UnlockSubAccountCommand>(
                    c => c.CustId == param.UserId
                        && c.SubAccId == param.SubAccId
                        && c.IP == param.LoginIP
                        && c.Language == param.LanguageKey));
        }

        [Fact]
        public void DeleteSubAccountAsync_Always_CallsExecuteAsync()
        {
            //// Arrange
            var param = new UnlockSubAccountParam(1, 2, "127.0.0.1", "E");

            //// Act
            subAccountService.DeleteSubAccountAsync(param).ConfigureAwait(false);

            //// Assert
            dynamicRepository.Received().ExecuteAsync(
                Arg.Is<DeleteSubAccountCommand>(
                    c => c.CustId == param.UserId
                        && c.SubAccId == param.SubAccId
                        && c.LoginIP == param.LoginIP
                        && c.Language == param.LanguageKey));
        }

        [Fact]
        public void GetManagedAccounts_Always_CallsFetchAsync()
        {
            //// Arrange
            var custId = 1;
            var roleId = UserLevel.Agent.Value;
            var subAccId = 3;

            //// Act
            subAccountService.GetManagedAccounts(custId, roleId, subAccId).ConfigureAwait(false);

            //// Assert
            dynamicRepository.Received().FetchAsync<ManagedAccount>(
                Arg.Is<GetManagedAccountCriteria>(
                    c => c.CustId == custId
                        && c.SubAccId == subAccId
                        && c.RoleId == roleId));
        }

        [Fact]
        public async Task GetManagedAccounts_Always_ReturnsCorrectResult()
        {
            //// Arrange
            dynamicRepository.FetchAsync<ManagedAccount>(Arg.Any<GetManagedAccountCriteria>())
                .Returns(Enumerable.Empty<ManagedAccount>());
            var expectedResult = Tuple.Create(false, Enumerable.Empty<ManagedAccount>());

            //// Act
            var actualResult = await subAccountService.GetManagedAccounts(1, 2, 3);

            //// Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void UpdateManagedAccount_Always_CallsExecuteAsync()
        {
            //// Arrange
            var param = new UpdateManagedAccountParam(1, UserLevel.Agent.Value, 2, true, string.Empty, "E", "127.0.0.1", 1);

            //// Act
            subAccountService.UpdateManagedAccount(param).ConfigureAwait(false);

            //// Assert
            dynamicRepository.Received().ExecuteAsync(
                Arg.Is<UpdateManagedAccountCommand>(
                    c => c.CustId == param.CustId
                        && c.SubAccId == param.SubAccId
                        && c.RoleId == param.RoleId
                        && c.IsAllDownline == param.IsAllDownline
                        && c.DownlineId == param.DownlineId
                        && c.Language == param.LanguageKey
                        && c.LoginIP == param.LoginIP
                        && c.UserId == param.UserId));
        }

        [Fact]
        public async Task CreateSubAccountAsync_ValidateFailed_ReturnsErrorResult()
        {
            //// Arrange
            var param = new CreateSubAccountParam(
                "suffix1",
                "suffix1",
                "pwd",
                "hashkey",
                Enumerable.Empty<string>(),
                "name",
                "[A],[B]",
                "E",
                "127.0.0.1",
                1,
                "actorname",
                2,
                true,
                string.Empty);
            var expectedResult = Result.CreateErrorResult();
            subAccountValidator.ValidateEmptyPassword(Arg.Any<string>()).Returns(expectedResult);

            //// Act
            var actualResult = await subAccountService.CreateSubAccountAsync(param);

            //// Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public async Task CreateSubAccountAsync_ValidateSuccess_CallsExecuteAsync()
        {
            //// Arrange
            var param = new CreateSubAccountParam(
                "suffix1",
                "suffix1",
                "pwd",
                "hashkey",
                Enumerable.Empty<string>(),
                "name",
                "[A],[B]",
                "E",
                "127.0.0.1",
                1,
                "actorname",
                2,
                true,
                string.Empty);
            var successResult = Result.CreateSuccessResult();
            subAccountValidator.ValidateEmptyPassword(Arg.Any<string>()).Returns(successResult);
            subAccountValidator.IsMatchPasswordRules(Arg.Any<string>(), Arg.Any<IEnumerable<string>>()).Returns(successResult);
            subAccountValidator.ValidateSuffix(Arg.Any<string>(), Arg.Any<string>()).Returns(successResult);

            //// Act
            await subAccountService.CreateSubAccountAsync(param);

            //// Assert
            await dynamicRepository.Received().ExecuteAsync(
                Arg.Is<CreateSubAccountCommand>(
                    c => c.CustId == param.UserId
                    && c.Name == param.Name
                    && c.UserName == param.UserName
                    && c.UserPwd == param.Password
                    && c.PageAccess == param.PageAccess
                    && c.LanguageKey == param.LanguageKey
                    && c.LoginIP == param.LoginIP));
        }

        [Fact]
        public void ValidateCreateSubAccountParam_PasswordIsEmpty_ReturnsErrorResult()
        {
            //// Arrange
            var param = new CreateSubAccountParam(
                "suffix1",
                "suffix1",
                "pwd",
                "hashkey",
                Enumerable.Empty<string>(),
                "name",
                "[A],[B]",
                "E",
                "127.0.0.1",
                1,
                "actorname",
                2,
                true,
                string.Empty);
            var expectedResult = Result.CreateErrorResult();
            subAccountValidator.ValidateEmptyPassword(Arg.Any<string>()).Returns(expectedResult);

            //// Act
            var actualResult = subAccountService.ValidateCreateSubAccountParam(param);

            //// Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void ValidateCreateSubAccountParam_IsNOTMatchPasswordRules_ReturnsErrorResult()
        {
            //// Arrange
            var param = new CreateSubAccountParam(
                "suffix1",
                "suffix1",
                "pwd",
                "hashkey",
                Enumerable.Empty<string>(),
                "name",
                "[A],[B]",
                "E",
                "127.0.0.1",
                1,
                "actorname",
                2,
                true,
                string.Empty);
            var expectedResult = Result.CreateErrorResult();
            var successResult = Result.CreateSuccessResult();
            subAccountValidator.ValidateEmptyPassword(Arg.Any<string>()).Returns(successResult);
            subAccountValidator.IsMatchPasswordRules(Arg.Any<string>(), Arg.Any<IEnumerable<string>>()).Returns(expectedResult);

            //// Act
            var actualResult = subAccountService.ValidateCreateSubAccountParam(param);

            //// Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public void ValidateCreateSubAccountParam_ValidateSuffixIsFailed_ReturnsErrorResult()
        {
            //// Arrange
            var param = new CreateSubAccountParam(
                "suffix1",
                "suffix1",
                "pwd",
                "hashkey",
                Enumerable.Empty<string>(),
                "name",
                "[A],[B]",
                "E",
                "127.0.0.1",
                1,
                "actorname",
                2,
                true,
                string.Empty);
            var expectedResult = Result.CreateErrorResult();
            var successResult = Result.CreateSuccessResult();
            subAccountValidator.ValidateEmptyPassword(Arg.Any<string>()).Returns(successResult);
            subAccountValidator.IsMatchPasswordRules(Arg.Any<string>(), Arg.Any<IEnumerable<string>>()).Returns(successResult);
            subAccountValidator.ValidateSuffix(Arg.Any<string>(), Arg.Any<string>()).Returns(expectedResult);

            //// Act
            var actualResult = subAccountService.ValidateCreateSubAccountParam(param);

            //// Assert
            Assert.True(compareLogic.Compare(expectedResult, actualResult).AreEqual);
        }

        [Fact]
        public async Task UpdateSubAccountAndKickoutAsync_Always_CallsExecuteAsync()
        {
            //// Arrange
            var param = new UpdateSubAccountParam(1, "name", "[A],[B]", "E", "127.0.0.1", 1, 2, true, string.Empty);
            subAccountValidator.FilterWithValidPermissions(param.PageAccess).Returns(new List<string> { "[A]", "[B]" });

            //// Act
            await subAccountService.UpdateSubAccountAndKickoutAsync(param, string.Empty);

            //// Assert
            await dynamicRepository.Received().ExecuteAsync(
                Arg.Is<UpdateSubAccountCommand>(
                    c => c.CustId == param.UserId
                    && c.SubAccId == param.SubAccId
                    && c.Name == param.Name
                    && c.PageAccess == param.PageAccess
                    && c.LanguageKey == param.LanguageKey
                    && c.LoginIP == param.LoginIP));
        }
    }
}