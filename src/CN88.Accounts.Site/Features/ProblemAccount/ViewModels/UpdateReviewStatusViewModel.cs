namespace CN88.Accounts.Site.Features.ProblemAccount.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CN88.Accounts.Shared.Resources;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Web.Models;

    public class UpdateReviewStatusViewModel
    {
        public UpdateReviewStatusViewModel(UpdateReviewStatusQuery query)
        {
            CustLevel = query.CustLevel;
            SelectedDownlineIds = query.SelectedDownlineIds;
            SelectedDownlineUsernames = query.SelectedDownlineUsernames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            DisplayShowAllAccountOption = SelectedDownlineUsernames.Count() > 5;
            Top10DownlineUsernames = SelectedDownlineUsernames.Take(5);
            if (DisplayShowAllAccountOption)
            {
                MoreDownlineUsernames = SelectedDownlineUsernames.Skip(5);
            }
        }

        public UserLevel CustLevel { get; }

        public string SelectedDownlineIds { get; }

        public IEnumerable<string> SelectedDownlineUsernames { get; }

        public bool DisplayShowAllAccountOption { get; }

        public IEnumerable<string> Top10DownlineUsernames { get; }

        public IEnumerable<string> MoreDownlineUsernames { get; }

        public string ConfirmMessage => $"{Resources.SetReviewedConfirmMessage} {string.Join(", ", Top10DownlineUsernames)}";
    }
}
