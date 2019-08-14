namespace CN88.Accounts.Site.Features.ProblemAccount.Requests
{
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.Shared;
    using MediatR;

    public class UpdateReviewStatusCommand : BaseQuery, IRequest<Result>
    {
        public UpdateReviewStatusCommand(IUser viewer)
            : base(viewer)
        {
        }

        public bool IsAffectedAllDownlines { get; set; }

        public string SelectedDownlineIds { get; set; }
    }
}
