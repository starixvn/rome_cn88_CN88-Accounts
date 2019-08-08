namespace CN88.Accounts.ProblemAccount.Params
{
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

        public int PageIndex { get; }

        public int PageSize { get; }
    }
}
