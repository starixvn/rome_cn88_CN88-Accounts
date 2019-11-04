namespace CN88.Accounts.Site.Features.ProblemAccount.ViewModels
{
    using System.Collections.Generic;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.Shared.Constants;
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.Shared.ViewModels;
    using CN88.Common.Models;

    public class IndexViewModel
    {
        private readonly ProblemAccountQuery query;

        public IndexViewModel(
            ProblemAccountQuery query,
            PagedData<ProblemAccount> pagedData)
        {
            this.query = query;
            PageData = pagedData;

            var hasEditAccountPermission = query.ViewerStatusId == CustomerStatusConstants.Active && query.AccountManagementEditView;
            CanUpdateAccountStatus = query.ViewerLevel.IsNotHeadlineLevel() && hasEditAccountPermission;
            CanUpdateReviewStatus = hasEditAccountPermission && query.SelectedReviewStatus != ReviewStatusConstants.Reviewed;
        }

        public bool IsMobile { get; }

        public bool CanUpdateAccountStatus { get; }

        public bool CanUpdateReviewStatus { get; }

        public UserLevel ViewerLevel => query.ViewerLevel;

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

        public bool IsShowNumberOfDownline => !CustLevel.IsMemberLevel();

        public bool IsShowUplineUsername
            => (ViewerLevel.IsHeadlineLevel() && CustLevel.IsMasterLevel()) || CustLevel.IsAgentLevel() || CustLevel.IsMemberLevel();

        public FilterViewModel FilterViewModel => new FilterViewModel(query, IsShowUplineUsername);

        public PagerViewModel PagerViewModel
            => new PagerViewModel(PageData.PageIndex, PageData.PageSize, PageData.TotalRecords);

        public int StartIndex => PageData.PageSize * (PageData.PageIndex - 1) + 1;

        public string BuildDownlineLink(string uplineUsername)
            => Shared.Helpers.UrlHelper.AddQueryStringParamsFunc(
                Shared.Helpers.UrlHelper.MapAccountsPath("~/ProblemAccount/Index"),
                GetDownlineLinkRouteValues(uplineUsername));

        public string BuildStatusLink(int custId, byte statusId)
            => Shared.Helpers.UrlHelper.AddQueryStringParamsFunc(
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