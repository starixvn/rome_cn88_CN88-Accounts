namespace CN88.Accounts.Site.Tests.Features.ProblemAccount.ViewModels
{
    using System.Collections.Generic;
    using System.Linq;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.ProblemAccount.ViewModels;
    using CN88.Accounts.Site.Features.Shared.Helpers;
    using CN88.Accounts.Site.Tests.Features.Shared.Helpers;
    using CN88.Accounts.Site.Tests.Fixtures;
    using KellermanSoftware.CompareNetObjects;
    using NSubstitute;
    using Xunit;

    [Collection("UserCollection")]
    public class IndexViewModelTests
    {
        private readonly UserFixture userFixture;
        private readonly ProblemAccountQuery query;
        private readonly IndexViewModel indexViewModel;
        private readonly CompareLogic compareLogic;

        public IndexViewModelTests(UserFixture userFixture)
        {
            this.userFixture = userFixture;
            query = new ProblemAccountQuery(userFixture.User);
            indexViewModel = new IndexViewModel(query, new PagedData<ProblemAccount>(Enumerable.Empty<ProblemAccount>(), 1, 10, 0));
            UrlHelperTestSetUp.SetUp();
            compareLogic = new CompareLogic();
        }

        [Fact]
        public void BuildDownlineLink_Always_CallsReplaceOrAddQueryStringParamsFuncWithCorrectRouteValues()
        {
            // Arrange
            var uplineUsername = "UplineUsername";
            var expectedRouteValues = new Dictionary<string, object>
            {
                [nameof(uplineUsername)] = uplineUsername,
                [nameof(query.FromDate)] = query.FromDate,
                [nameof(query.ToDate)] = query.ToDate,
                [nameof(query.SelectedReviewStatus)] = query.SelectedReviewStatus,
                [nameof(query.CustLevelId)] = query.CustLevel.GetDirectDownLineLevel().Value
            };

            // Act
            indexViewModel.BuildDownlineLink(uplineUsername);

            // Assert
            UrlHelper.AddQueryStringParamsFunc.Received().Invoke(
                "AccountsPath",
                Arg.Is<Dictionary<string, object>>(routeValues => compareLogic.Compare(expectedRouteValues, routeValues).AreEqual));
        }

        [Fact]
        public void BuildStatusLink_Always_CallsAddQueryStringParamsWithCorrectRouteValues()
        {
            // Arrange
            var custId = 1;
            byte statusId = 2;
            var expectedRouteValues = new Dictionary<string, object>
            {
                [nameof(custId)] = custId,
                [nameof(statusId)] = statusId,
                [nameof(query.CustLevelId)] = query.CustLevelId
            };

            // Act
            indexViewModel.BuildStatusLink(custId, statusId);

            // Assert
            UrlHelper.AddQueryStringParamsFunc.Received().Invoke(
                "FeaturesPath",
                Arg.Is<Dictionary<string, object>>(routeValues => compareLogic.Compare(expectedRouteValues, routeValues).AreEqual));
        }
    }
}
