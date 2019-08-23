using CN88.Accounts.Shared.Models;
using CN88.Accounts.Site.Features.ProblemAccount;
using CN88.Accounts.Site.Features.ProblemAccount.Requests;
using CN88.Accounts.Site.Features.Shared.ViewModels;
using CN88.Web.Helpers;
using MediatR;
using NSubstitute;
using Xunit;

namespace CN88.Accounts.Site.Tests.Features.ProblemAccount
{
    public class ProblemAccountControllerTests
    {
        private readonly IUser viewer;
        private readonly IMediator mediator;
        private readonly BrowserHelper browserHelper;
        private readonly ProblemAccountController problemAccountController;

        public ProblemAccountControllerTests()
        {
            viewer = Substitute.For<IUser>();
            viewer.Id.Returns(1);
            viewer.Name.Returns("ViewerName");
            viewer.Language.Returns("en-US");

            mediator = Substitute.For<IMediator>();
            browserHelper = Substitute.For<BrowserHelper>();
            problemAccountController = new ProblemAccountController(mediator, browserHelper);
        }


        [Fact]
        public void Index_MessageIsValid_CallsMediatorSendProblemAccountQuery()
        {
            // Arrange
            var message = new ProblemAccountQuery(viewer);

            // Act
            problemAccountController.Index(message).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(message);
        }

        [Fact]
        public void Index_MessageIsValidAndIsMobile_CallsMediatorSendProblemAccountQuery()
        {
            // Arrange
            var message = new ProblemAccountQuery(viewer);
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
            var message = new ProblemAccountExcelQuery(viewer);

            // Act
            problemAccountController.Excel(message).ConfigureAwait(false);

            // Assert
            mediator.Received().Send<ExcelFileViewModel>(message);
        }

        [Fact]
        public void UpdateReviewStatus_MessageIsValid_CallsMediatorSendUpdateReviewStatusQuery()
        {
            // Arrange
            var message = new UpdateReviewStatusQuery(viewer);
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
            var message = new UpdateReviewStatusCommand(viewer);
            message.SelectedDownlineIds = "1,2";

            // Act
            problemAccountController.UpdateReviewStatus(message).ConfigureAwait(false);

            // Assert
            mediator.Received().Send(message);
        }
    }
}
