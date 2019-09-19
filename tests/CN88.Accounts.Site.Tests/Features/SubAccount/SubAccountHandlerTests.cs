namespace CN88.Accounts.Site.Tests.Features.SubAccount
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CN88.Accounts.Site.Features.SubAccount;
    using CN88.Accounts.Site.Features.SubAccount.Requests;
    using CN88.Accounts.Site.Features.SubAccount.ViewModels;
    using CN88.Accounts.Site.Tests.Fixtures;
    using CN88.Accounts.SubAccount;
    using CN88.Accounts.SubAccount.Models;
    using CN88.Accounts.SubAccount.Params;
    using CN88.Web.Helpers;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using Sunplus.Agent.Utilities;
    using Xunit;

    [Collection("UserCollection")]
    public class SubAccountHandlerTests
    {
        private const string HashKeySessionKey = "Alpha.CN88.Site.Handlers.PasswordHandler.PasswordHash";
        private readonly SubAccountService subAccountService;
        private readonly Encryptor encryptor;
        private readonly SessionManager sessionManager;
        private readonly BrowserHelper browserHelper;
        private readonly UserFixture userFixture;
        private readonly SubAccountHandler subAccountHandler;
        private readonly CompareLogic compareLogic;

        public SubAccountHandlerTests(UserFixture userFixture)
        {
            this.userFixture = userFixture;
            subAccountService = Substitute.For<SubAccountService>();
            encryptor = Substitute.For<Encryptor>();
            sessionManager = Substitute.For<SessionManager>();
            browserHelper = Substitute.For<BrowserHelper>();
            subAccountHandler = new SubAccountHandler(subAccountService, encryptor, sessionManager, browserHelper);
            compareLogic = new CompareLogic();
        }

        [Fact]
        public void HandleIndexQuery_Always_GetSubAccountsAsync()
        {
            //// Arrange
            var message = new IndexQuery(userFixture.User);

            //// Act
            subAccountHandler.Handle(message).ConfigureAwait(false);

            //// Assert
            subAccountService.Received().GetSubAccountsAsync(message.ViewerId);
        }

        [Fact]
        public async Task HandleIndexQuery_Always_ReturnsIndexViewModel()
        {
            //// Arrange
            var message = new IndexQuery(userFixture.User);
            subAccountService.GetSubAccountsAsync(message.ViewerId).Returns(Task.FromResult(Enumerable.Empty<SubAccountItem>()));
            var expectedViewModel = new IndexViewModel(message, Enumerable.Empty<SubAccountItem>(), browserHelper);

            //// Act
            var actualViewModel = await subAccountHandler.Handle(message);

            //// Assert
            Assert.True(compareLogic.Compare(expectedViewModel, actualViewModel).AreEqual);
        }

        [Fact]
        public void HandleNewQuery_Always_GetManagedAccounts()
        {
            //// Arrange
            var message = new NewQuery(userFixture.User);

            //// Act
            subAccountHandler.Handle(message).ConfigureAwait(false);

            //// Assert
            subAccountService.Received().GetManagedAccounts(message.ViewerId, message.ViewerLevelId, 0);
        }

        [Fact]
        public async Task HandleNewQuery_Always_ReturnsNewViewModel()
        {
            //// Arrange
            var message = new NewQuery(userFixture.User);
            encryptor.RandomString(Arg.Any<int>()).Returns("hashKey");
            var manageAccounts = new Tuple<bool, IEnumerable<ManagedAccount>>(true, Enumerable.Empty<ManagedAccount>());
            subAccountService.GetManagedAccounts(message.ViewerId, message.ViewerLevelId, 0).Returns(Task.FromResult(manageAccounts));
            var expectedViewModel = new NewViewModel(message, "hashKey", manageAccounts);

            //// Act
            var actualViewModel = await subAccountHandler.Handle(message);

            //// Assert
            Assert.True(compareLogic.Compare(expectedViewModel, actualViewModel).AreEqual);
        }

        [Fact]
        public void HandleUpdateQuery_Always_GetSubAccountAsync()
        {
            //// Arrange
            var message = new UpdateQuery(userFixture.User);

            //// Act
            subAccountHandler.Handle(message).ConfigureAwait(false);

            //// Assert
            subAccountService.Received().GetSubAccountAsync(message.ViewerId, message.CustId);
        }

        [Fact]
        public void HandleUpdateQuery_Always_GetManagedAccounts()
        {
            //// Arrange
            var message = new UpdateQuery(userFixture.User);

            //// Act
            subAccountHandler.Handle(message).ConfigureAwait(false);

            //// Assert
            subAccountService.Received().GetManagedAccounts(message.ViewerId, message.ViewerLevelId, message.CustId);
        }

        [Fact]
        public async Task HandleUpdateQuery_Always_ReturnsEditViewModel()
        {
            //// Arrange
            var message = new UpdateQuery(userFixture.User);
            subAccountService.GetSubAccountAsync(message.ViewerId, message.CustId).Returns(Task.FromResult(new SubAccountItem()));
            var manageAccounts = new Tuple<bool, IEnumerable<ManagedAccount>>(true, Enumerable.Empty<ManagedAccount>());
            subAccountService.GetManagedAccounts(message.ViewerId, message.ViewerLevelId, message.CustId)
                .Returns(Task.FromResult(manageAccounts));
            var expectedViewModel = new EditViewModel(new SubAccountItem(), manageAccounts);

            //// Act
            var actualViewModel = await subAccountHandler.Handle(message);

            //// Assert
            Assert.True(compareLogic.Compare(expectedViewModel, actualViewModel).AreEqual);
        }

        [Fact]
        public void HandleNewCommand_Always_CreateSubAccountAsync()
        {
            //// Arrange
            var message = new NewCommand(userFixture.User);
            sessionManager.Get<string>(HashKeySessionKey).Returns("HashKey");

            //// Act
            subAccountHandler.Handle(message).ConfigureAwait(false);

            //// Assert
            subAccountService.Received().CreateSubAccountAsync(
                Arg.Is<CreateSubAccountParam>(
                    p => p.Suffix1 == message.Suffix1
                        && p.Suffix2 == message.Suffix2
                        && p.Password == message.Password
                        && p.HashKey == "HashKey"
                        && p.PageAccess == message.PageAccess
                        && p.LanguageKey == message.LanguageKey
                        && p.LoginIP == message.IP
                        && p.UserId == message.ViewerId
                        && p.ActorName == message.ViewerName
                        && p.RoleId == message.ViewerLevelId
                        && p.IsAllDownline == message.IsSelectAllDownline
                        && p.DownlineId == message.DownlineId));
        }

        [Fact]
        public void HandleUpdateCommand_Always_UpdateSubAccountAsync()
        {
            //// Arrange
            var message = new UpdateCommand(userFixture.User);
            sessionManager.Get<string>(HashKeySessionKey).Returns("HashKey");

            //// Act
            subAccountHandler.Handle(message).ConfigureAwait(false);

            //// Assert
            subAccountService.Received().UpdateSubAccountAsync(
                Arg.Is<UpdateSubAccountParam>(
                    p => p.SubAccId == message.CustId
                        && p.PageAccess == message.PageAccess
                        && p.LanguageKey == message.LanguageKey
                        && p.LoginIP == message.IP
                        && p.UserId == message.ViewerId
                        && p.RoleId == message.ViewerLevelId
                        && p.IsAllDownline == message.IsSelectAllDownline
                        && p.DownlineId == message.DownlineId));
        }

        [Fact]
        public void HandleUnlockCommand_Always_UnlockSubAccountAsync()
        {
            //// Arrange
            var message = new UnlockCommand(userFixture.User);

            //// Act
            subAccountHandler.Handle(message).ConfigureAwait(false);

            //// Assert
            subAccountService.Received().UnlockSubAccountAsync(
                Arg.Is<UnlockSubAccountParam>(
                    p => p.UserId == message.ViewerId
                        && p.SubAccId == message.CustId
                        && p.LanguageKey == message.LanguageKey
                        && p.LoginIP == message.IP));
        }

        [Fact]
        public void HandleDeleteCommand_Always_DeleteSubAccountAsync()
        {
            //// Arrange
            var message = new DeleteCommand(userFixture.User);

            //// Act
            subAccountHandler.Handle(message).ConfigureAwait(false);

            //// Assert
            subAccountService.Received().DeleteSubAccountAsync(
                Arg.Is<UnlockSubAccountParam>(
                    p => p.UserId == message.ViewerId
                        && p.SubAccId == message.CustId
                        && p.LanguageKey == message.LanguageKey
                        && p.LoginIP == message.IP));
        }
    }
}