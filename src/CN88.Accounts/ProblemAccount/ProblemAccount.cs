namespace CN88.Accounts.ProblemAccount
{
    using System;
    using CN88.Accounts.Shared.Constants;
    using CN88.Accounts.Shared.Resources;

    public class ProblemAccount
    {
        public ProblemAccount(
            int custId,
            string username,
            string name,
            string nickname,
            byte status,
            DateTime createdDate,
            bool isReviewed)
        {
            CustId = custId;
            Username = username;
            Name = name;
            Nickname = nickname;
            Status = status;
            CreatedDate = createdDate;
            IsReviewed = isReviewed;
        }

        public int CustId { get; }

        public string Username { get; }

        public string Name { get; }

        public string Nickname { get; }

        public byte Status { get; }

        public string StatusName => SharedResources.ResourceManager.GetString(CustomerStatusConstants.Items[Status]);

        public DateTime CreatedDate { get; }

        public bool IsReviewed { get; }

        public string ReviewStatus
            => Resources.ProblemAccountResources.ResourceManager.GetString(ReviewStatusConstants.Items[Convert.ToByte(IsReviewed)]);
    }
}
