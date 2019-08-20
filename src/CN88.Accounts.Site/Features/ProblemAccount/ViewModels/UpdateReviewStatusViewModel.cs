namespace CN88.Accounts.Site.Features.ProblemAccount.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CN88.Accounts.Shared.Resources;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Common.Models;

    public class UpdateReviewStatusViewModel
    {
        private const int numberOfDisplayDownlines = 10;

        public UpdateReviewStatusViewModel(UpdateReviewStatusQuery query)
        {
            CustLevel = query.CustLevel;
            SelectedDownlineIds = query.SelectedDownlineIds;
            SelectedDownlineUsernames = query.SelectedDownlineUsernames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            DisplayShowAllAccountOption = SelectedDownlineUsernames.Count() > numberOfDisplayDownlines;
            TopDownlineUsernames = SelectedDownlineUsernames.Take(numberOfDisplayDownlines);
            if (DisplayShowAllAccountOption)
            {
                MoreDownlineUsernames = SelectedDownlineUsernames.Skip(numberOfDisplayDownlines);
            }
        }

        public UserLevel CustLevel { get; }

        public string SelectedDownlineIds { get; }

        public IEnumerable<string> SelectedDownlineUsernames { get; }

        public bool DisplayShowAllAccountOption { get; }

        public IEnumerable<string> TopDownlineUsernames { get; }

        public IEnumerable<string> MoreDownlineUsernames { get; }

        public string ConfirmMessage => $"{Resources.SetReviewedConfirmMessage} {string.Join(", ", TopDownlineUsernames)}";
    }
}
