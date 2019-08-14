namespace CN88.Accounts.Site.Features.ProblemAccount.Requests
{
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.ProblemAccount.ViewModels;
    using CN88.Accounts.Site.Features.Shared;
    using MediatR;

    public class UpdateReviewStatusQuery : BaseQuery, IRequest<UpdateReviewStatusViewModel>
    {
        public UpdateReviewStatusQuery(IUser viewer)
            : base(viewer)
        {
        }

        public string SelectedDownlineIds { get; set; }

        public string SelectedDownlineUsernames { get; set; }
    }
}
