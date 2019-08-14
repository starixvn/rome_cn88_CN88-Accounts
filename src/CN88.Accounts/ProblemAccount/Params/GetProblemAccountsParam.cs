namespace CN88.Accounts.ProblemAccount.Params
{
    using System;

    public class GetProblemAccountsParam
    {
        public GetProblemAccountsParam(
            int custId,
            int subAccId,
            bool isAllDownline,
            string uplineUsername,
            byte searchRoleId,
            string searchUsername,
            byte reviewStatus,
            DateTime fromDate,
            DateTime toDate,
            int pageIndex,
            int pageSize)
        {
            CustId = custId;
            SubAccId = subAccId;
            IsAllDownline = isAllDownline;
            UplineUsername = uplineUsername;
            SearchRoleId = searchRoleId;
            SearchUsername = searchUsername;
            ReviewStatus = reviewStatus;
            FromDate = fromDate;
            ToDate = toDate;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public int CustId { get; }

        public int SubAccId { get; }

        public bool IsAllDownline { get; }

        public string UplineUsername { get; }

        public byte SearchRoleId { get; }

        public string SearchUsername { get; }

        public byte ReviewStatus { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public int PageIndex { get; }

        public int PageSize { get; }
    }
}
