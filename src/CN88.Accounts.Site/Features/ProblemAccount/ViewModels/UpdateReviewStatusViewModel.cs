namespace CN88.Accounts.Site.Features.ProblemAccount.ViewModels
{
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Web.Models;

    public class UpdateReviewStatusViewModel
    {
        public UpdateReviewStatusViewModel(UpdateReviewStatusQuery query)
        {
            SelectedDownlineIds = query.SelectedDownlineIds;
            SelectedDownlineUsernames = query.SelectedDownlineUsernames;
            CustLevel = query.CustLevel;
        }

        public string SelectedDownlineIds { get; }

        public string SelectedDownlineUsernames { get; }

        public UserLevel CustLevel { get; }
    }
}
