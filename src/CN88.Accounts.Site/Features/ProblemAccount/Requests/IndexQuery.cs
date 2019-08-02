namespace CN88.Accounts.Site.Features.ProblemAccount.Requests
{
    using System;
    using CN88.Accounts.Shared;
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.ProblemAccount.ViewModels;
    using CN88.Accounts.Site.Features.Shared;
    using MediatR;

    public class IndexQuery : BaseQuery, IRequest<IndexViewModel>
    {
        public IndexQuery(IUser viewer)
            : base(viewer)
        {
            FromDate = SystemTime.Today.AddDays(-14);
            ToDate = SystemTime.Today;
        }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }
    }
}
