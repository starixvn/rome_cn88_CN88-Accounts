namespace CN88.Accounts.Site.Features.ProblemAccount.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.ProblemAccount.Resources;
    using CN88.Accounts.Shared;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.Shared.ViewModels;
    using CN88.Web.Helpers;
    using CN88.Web.Models;

    public class IndexViewModel
    {
        public IndexViewModel(IndexQuery query, BrowserHelper browserHelper)
        {
            IsMobile = browserHelper.IsMobile();
            CustLevel = query.CustLevel;

            DateRangePicker = new DateRangePickerViewModel(
                DateTime.MinValue,
                SystemTime.Today,
                query.FromDate,
                query.ToDate);
            ReviewStatusSelection = new SingleSelectionViewModel(
                BuildReviewStatusSelectListItems(query.SelectedReviewStatus),
                query.SelectedReviewStatus);
        }

        public bool IsMobile { get; }

        public UserLevel CustLevel { get; }

        public int NumberOfColumns => CustLevel.IsMasterLevel() || CustLevel.IsAgentLevel() ? 9 : 8;

        public DateRangePickerViewModel DateRangePicker { get; }

        public SingleSelectionViewModel ReviewStatusSelection { get; }

        private IEnumerable<SelectListItem> BuildReviewStatusSelectListItems(int selectedStatus)
            => new List<SelectListItem>
            {
                new SelectListItem { Text = ProblemAccountResources.All, Value = ReviewStatusConstants.All.ToString() },
                new SelectListItem { Text = ProblemAccountResources.Reviewed, Value = ReviewStatusConstants.Reviewed.ToString() },
                new SelectListItem { Text = ProblemAccountResources.NotReviewed, Value = ReviewStatusConstants.NotReviewed.ToString() }
            }.Select(item =>
            {
                item.Selected = item.Value == selectedStatus.ToString();
                return item;
            });
    }
}
