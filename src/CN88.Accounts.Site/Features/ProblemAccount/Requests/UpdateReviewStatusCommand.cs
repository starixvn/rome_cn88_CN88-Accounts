namespace CN88.Accounts.Site.Features.ProblemAccount.Requests
{
    using CN88.Accounts.Shared.Models;
    using CN88.Common.Models;
    using MediatR;

    public class UpdateReviewStatusCommand : UpdateReviewStatusBaseQuery, IRequest<Result>
    {
        public UpdateReviewStatusCommand(IUser viewer)
            : base(viewer)
        {
        }

        public bool IsAffectedAllDownlines { get; set; }
    }
}
