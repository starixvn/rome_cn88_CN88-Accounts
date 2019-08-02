using System;
using CN88.Accounts.Shared;
using CN88.Accounts.Site.Features.ProblemAccount.Requests;
using CN88.Accounts.Site.Features.Shared.ViewModels;

namespace CN88.Accounts.Site.Features.ProblemAccount.ViewModels
{
    public class IndexViewModel
    {
        private readonly IndexQuery query;

        public IndexViewModel(IndexQuery query)
        {
            this.query = query;
        }

        public DateRangePickerViewModel DateRangePickerViewModel
            => new DateRangePickerViewModel(DateTime.MinValue, SystemTime.Today, query.FromDate, query.ToDate);
    }
}
