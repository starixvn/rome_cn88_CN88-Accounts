namespace CN88.Accounts.Site.Features.ProblemAccount.Requests
{
    using System;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.Shared;
    using CN88.Accounts.Shared.Constants;
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.ProblemAccount.ViewModels;
    using CN88.Accounts.Site.Features.Shared;
    using CN88.Web.Models;
    using MediatR;

    public class ProblemAccountQuery : BaseQuery, IRequest<IndexViewModel>
    {
        private const int defaultNumberOfDays = -13;

        public ProblemAccountQuery(IUser viewer)
            : base(viewer)
        {
            CustLevelId = UserLevel.Master.Value;
            FromDate = SystemTime.Today.AddDays(defaultNumberOfDays);
            ToDate = SystemTime.Today;
            SelectedReviewStatus = ReviewStatusConstants.NotReviewed;
            PageIndex = 1;
            PageSize = PagerConstants.DefaultPageSize;
        }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public byte SelectedReviewStatus { get; set; }

        public string UplineUsername { get; set; }

        public string SearchUsername { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
    }
}
