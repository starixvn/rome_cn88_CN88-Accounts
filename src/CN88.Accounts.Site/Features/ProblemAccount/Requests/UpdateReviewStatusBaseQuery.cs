namespace CN88.Accounts.Site.Features.ProblemAccount.Requests
{
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.Shared;

    public class UpdateReviewStatusBaseQuery : BaseQuery
    {
        public UpdateReviewStatusBaseQuery(IUser viewer)
            : base(viewer)
        {
        }

        public string SelectedDownlineIds { get; set; }

        public override bool IsValid()
            => base.IsValid() && !IsInternal && !string.IsNullOrWhiteSpace(SelectedDownlineIds);
    }
}
