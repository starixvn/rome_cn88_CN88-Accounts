namespace CN88.Accounts.Site.Features.ProblemAccount.ViewModels
{
    using System.Collections.Generic;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.Shared.ViewModels;
    using CN88.Web.Helpers;
    using CN88.Web.Models;

    public class IndexViewModel
    {
        private readonly ProblemAccountQuery query;

        public IndexViewModel(
            ProblemAccountQuery query,
            bool isMobile,
            BrowserHelper browserHelper,
            PagedData<ProblemAccount> pagedData)
        {
            this.query = query;
            IsMobile = isMobile;
            PageData = pagedData;
            CanUpdateReviewStatus = query.AccountManagementEditView && query.SelectedReviewStatus != ReviewStatusConstants.Reviewed;
        }

        public bool IsMobile { get; }

        public bool CanUpdateReviewStatus { get; }

        public UserLevel CustLevel => query.CustLevel;

        public PagedData<ProblemAccount> PageData { get; }

        public int NumberOfColumns
        {
            get
            {
                var numberOfColumns = CustLevel.IsMasterLevel() || CustLevel.IsMemberLevel() ? 9 : 10;

                return CanUpdateReviewStatus ? numberOfColumns : numberOfColumns - 1;
            }
        }

        public FilterViewModel FilterViewModel => new FilterViewModel(query);

        public PagerViewModel PagerViewModel
            => new PagerViewModel(PageData.PageIndex, PageData.PageSize, PageData.TotalRecords);

        public string BuildDownlineLink(string uplineUsername, string currentLink)
            => Shared.Helpers.UrlHelper.ReplaceOrAddQueryStringParamsFunc(
                currentLink,
                GetDownlineLinkRouteValues(uplineUsername));

        public string BuildStatusLink(int custId, byte statusId)
            => Shared.Helpers.UrlHelper.AddQueryStringParams(
                Shared.Helpers.UrlHelper.MapFeaturesPath("~/CustomerList/StatusPopup"),
                new Dictionary<string, object>
                {
                    [nameof(custId)] = custId,
                    [nameof(statusId)] = statusId,
                    [nameof(query.CustLevelId)] = query.CustLevelId
                });

        private IDictionary<string, object> GetDownlineLinkRouteValues(string uplineUsername)
            => new Dictionary<string, object>
            {
                [nameof(uplineUsername)] = uplineUsername,
                [nameof(query.FromDate)] = query.FromDate,
                [nameof(query.ToDate)] = query.ToDate,
                [nameof(query.SelectedReviewStatus)] = query.SelectedReviewStatus,
                [nameof(query.CustLevelId)] = query.CustLevel.GetDirectDownLineLevel().Value
            };

    }
}
