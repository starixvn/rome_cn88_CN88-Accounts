namespace CN88.Accounts.Shared.Models
{
    using System.Collections.Generic;

    public class PagedData<T> where T : class
    {
        public PagedData(
            IEnumerable<T> dataSource,
            int pageIndex,
            int pageSize,
            int totalRecords)
        {
            DataSource = dataSource;
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalRecords = totalRecords;
        }

        public IEnumerable<T> DataSource { get; }

        public int PageIndex { get; }

        public int PageSize { get; }

        public int TotalRecords { get; }

        public bool Any() => TotalRecords > 0;
    }
}
