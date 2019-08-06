namespace CN88.Accounts.Site.Features.ProblemAccount.Requests
{
    using System;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.Shared;
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.ProblemAccount.ViewModels;
    using CN88.Accounts.Site.Features.Shared;
    using CN88.Web.Models;
    using MediatR;

    public class IndexQuery : BaseQuery, IRequest<IndexViewModel>
    {
        public IndexQuery(IUser viewer)
            : base(viewer)
        {
            CustLevelId = UserLevel.Master.Value;
            FromDate = SystemTime.Today.AddDays(-14);
            ToDate = SystemTime.Today;
            SelectedReviewStatus = ReviewStatusConstants.NotReviewed;
        }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int SelectedReviewStatus { get; set; }
    }
}
