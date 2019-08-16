namespace CN88.Accounts.Site.Features.ProblemAccount.Requests
{
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.Shared;

    public class UpdateReviewStatusQuery : BaseQuery
    {
        public UpdateReviewStatusQuery(IUser viewer)
            : base(viewer)
        {
        }

        public string SelectedDownlineIds { get; set; }

        public string SelectedDownlineUsernames { get; set; }

        public override bool IsValid()
            => base.IsValid() && !string.IsNullOrWhiteSpace(SelectedDownlineIds);
    }
}
