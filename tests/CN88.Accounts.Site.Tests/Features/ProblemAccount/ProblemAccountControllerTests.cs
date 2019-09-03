using CN88.Accounts.Site.Features.ProblemAccount;
using CN88.Accounts.Site.Features.ProblemAccount.Requests;
using CN88.Accounts.Site.Features.Shared.ViewModels;
using CN88.Accounts.Site.Tests.Fixtures;
using CN88.Web.Helpers;
using MediatR;
using NSubstitute;
using Xunit;

namespace CN88.Accounts.Site.Tests.Features.ProblemAccount
{
    [Collection("UserCollection")]
    public class ProblemAccountControllerTests
    {
        private readonly IMediator mediator;
        private readonly BrowserHelper browserHelper;
        private readonly UserFixture userFixture;
        private readonly ProblemAccountController problemAccountController;

        public ProblemAccountControllerTests(UserFixture userFixture)
        {
            this.userFixture = userFixture;

            mediator = Substitute.For<IMediator>();
            browserHelper = Substitute.For<BrowserHelper>();
            problemAccountController = new ProblemAccountController(mediator, browserHelper);
        }


        [Fact]
        public void Index_MessageIsValid_CallsMediatorSendProblemAccountQuery()
        {
            // Arrange
            var message = new ProblemAccountQuery(userFixture.User);

            // Act
            problemAccountController.Index(message).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public void Index_MessageIsValidAndIsMobile_CallsMediatorSendProblemAccountQuery()
        {
            // Arrange
            var message = new ProblemAccountQuery(userFixture.User);
            browserHelper.IsMobile().Returns(true);

            // Act
            problemAccountController.Index(message).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(message);
        }


        [Fact]
        public void Excel_MessageIsValid_CallsMediatorSendProblemAccountExcelQuery()
        {
            // Arrange
            var message = new ProblemAccountExcelQuery(userFixture.User);

            // Act
            problemAccountController.Excel(message).ConfigureAwait(false);

            // Assert
            mediator.Received().Send<ExcelFileViewModel>(message);
        }

        [Fact]
        public void UpdateReviewStatus_MessageIsValid_CallsMediatorSendUpdateReviewStatusQuery()
        {
            // Arrange
            var message = new UpdateReviewStatusQuery(userFixture.User);
            message.SelectedDownlineIds = "1,2";

            // Act
            problemAccountController.UpdateReviewStatus(message).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public void UpdateReviewStatus_MessageIsValid_CallsMediatorSendUpdateReviewStatusCommand()
        {
            // Arrange
            var message = new UpdateReviewStatusCommand(userFixture.User);
            message.SelectedDownlineIds = "1,2";

            // Act
            problemAccountController.UpdateReviewStatus(message).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(message);
        }
    }
}
