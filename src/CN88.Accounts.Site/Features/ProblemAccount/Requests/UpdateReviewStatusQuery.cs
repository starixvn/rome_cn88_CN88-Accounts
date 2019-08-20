namespace CN88.Accounts.Site.Features.ProblemAccount.Requests
{
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.ProblemAccount.ViewModels;
    using MediatR;

    public class UpdateReviewStatusQuery : UpdateReviewStatusBaseQuery, IRequest<UpdateReviewStatusViewModel>
    {
        public UpdateReviewStatusQuery(IUser viewer)
            : base(viewer)
        {
        }

        public string SelectedDownlineUsernames { get; set; }
    }
}
