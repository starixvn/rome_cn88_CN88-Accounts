namespace CN88.Accounts.ProblemAccount.Criteria
{
    using System;
    using Fanex.Data.Repository;

    public class GetProblemAccountsCriteria : CriteriaBase
    {
        public GetProblemAccountsCriteria(
            int custId,
            int subAccId,
            bool isAllDownline,
            string uplineUsername,
            byte searchRoleId,
            string searchUsername,
            byte reviewStatusId,
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
            ReviewStatusId = reviewStatusId;
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

        public byte ReviewStatusId { get; }

        public DateTime FromDate { get; }

        public DateTime ToDate { get; }

        public int PageIndex { get; }

        public int PageSize { get; }

        public int TotalRows { get; set; }

        public override string GetSettingKey()
            => "CS_ProblemAccount_CustomerList";

        public override bool IsValid()
            => CustId > 0 && SubAccId >= 0 && SearchRoleId > 0;

    }
}
