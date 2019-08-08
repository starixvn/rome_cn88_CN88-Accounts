namespace CN88.Accounts.Site.Features.Shared.ViewModels
{
    public class PagerViewModel
    {
        public PagerViewModel(int pageIndex, int pageSize, int totalRecords)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }

        public int PageIndex { get; }

        public int PageSize { get; }

        public int TotalRecords { get; }
    }
}
