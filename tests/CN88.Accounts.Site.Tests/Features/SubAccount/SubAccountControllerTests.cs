namespace CN88.Accounts.Site.Tests.Features.SubAccount
{
    using CN88.Accounts.Site.Features.SubAccount;
    using CN88.Accounts.Site.Features.SubAccount.Requests;
    using CN88.Accounts.Site.Tests.Fixtures;
    using CN88.Web.Helpers;
    using MediatR;
    using NSubstitute;
    using Xunit;

    [Collection("UserCollection")]
    public class SubAccountControllerTests
    {
        private readonly IMediator mediator;
        private readonly BrowserHelper browserHelper;
        private readonly UserFixture userFixture;
        private readonly SubAccountController subAccountController;

        public SubAccountControllerTests(UserFixture userFixture)
        {
            this.userFixture = userFixture;
            mediator = Substitute.For<IMediator>();
            browserHelper = Substitute.For<BrowserHelper>();
            subAccountController = new SubAccountController(mediator, browserHelper);
        }

        [Fact]
        public void Index_Always_CallsMediatorSendIndexQuery()
        {
            //// Arrange
            var message = new IndexQuery(userFixture.User);

            //// Act
            subAccountController.Index(message).ConfigureAwait(false);

            //// Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public void Index_Always_CallsMediatorSendNewQuery()
        {
            //// Arrange
            var message = new NewQuery(userFixture.User);

            //// Act
            subAccountController.New(message).ConfigureAwait(false);

            //// Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public void New_Always_CallsMediatorSendNewCommand()
        {
            //// Arrange
            var command = new NewCommand(userFixture.User)
            {
                IsSelectAllDownline = true
            };

            //// Act
            subAccountController.New(command).ConfigureAwait(false);

            //// Assert
            mediator.Received().Send(command);
        }

        [Fact]
        public void Edit_Always_CallsMediatorSendUpdateQuery()
        {
            //// Arrange
            var message = new UpdateQuery(userFixture.User)
            {
                CustId = 2
            };

            //// Act
            subAccountController.Edit(message).ConfigureAwait(false);

            //// Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public void Edit_Always_CallsMediatorSendUpdateCommand()
        {
            //// Arrange
            var command = new UpdateCommand(userFixture.User)
            {
                IsSelectAllDownline = true
            };

            //// Act
            subAccountController.Edit(command).ConfigureAwait(false);

            //// Assert
            mediator.Received().Send(command);
        }

        [Fact]
        public void Unlock_Always_CallsMediatorSendUnlockCommand()
        {
            //// Arrange
            var command = new UnlockCommand(userFixture.User);

            //// Act
            subAccountController.Unlock(command).ConfigureAwait(false);

            //// Assert
            mediator.Received().Send(command);
        }

        [Fact]
        public void Delete_Always_CallsMediatorSendDeleteCommand()
        {
            //// Arrange
            var command = new DeleteCommand(userFixture.User);

            //// Act
            subAccountController.Delete(command).ConfigureAwait(false);

            //// Assert
            mediator.Received().Send(command);
        }
    }
}