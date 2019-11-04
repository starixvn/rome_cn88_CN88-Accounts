namespace CN88.Accounts.Site.Features.ProblemAccount.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.Shared;
    using CN88.Accounts.Shared.Resources;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.Shared.ViewModels;
    using CN88.Common.Models;

    public class FilterViewModel
    {
        public FilterViewModel(ProblemAccountQuery query, bool isShowUplineUsername)
        {
            UplineUsername = query.UplineUsername;
            CustName = query.CustName;
            CustLevel = query.CustLevel;
            IsShowUplineUsername = isShowUplineUsername;

            DateRangePicker = new DateRangePickerViewModel(
               DateTime.MinValue,
               SystemTime.Today,
               query.FromDate,
               query.ToDate);

            ReviewStatusSelection = new SingleSelectionViewModel(
                BuildReviewStatusSelectListItems(query.SelectedReviewStatus),
                query.SelectedReviewStatus);
        }

        public bool IsShowUplineUsername { get; }

        public string UplineUsername { get; }

        public string CustName { get; }

        public UserLevel CustLevel { get; }

        public DateRangePickerViewModel DateRangePicker { get; }

        public SingleSelectionViewModel ReviewStatusSelection { get; }

        private IEnumerable<SelectListItem> BuildReviewStatusSelectListItems(int selectedStatus)
            => new List<SelectListItem>
            {
                new SelectListItem { Text = Resources.All, Value = ReviewStatusConstants.All.ToString() },
                new SelectListItem { Text = Resources.Reviewed, Value = ReviewStatusConstants.Reviewed.ToString() },
                new SelectListItem { Text = Resources.NotReviewed, Value = ReviewStatusConstants.NotReviewed.ToString() }
            }.Select(item =>
            {
                item.Selected = item.Value == selectedStatus.ToString();
                return item;
            });
    }
}