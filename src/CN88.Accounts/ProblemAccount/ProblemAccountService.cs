namespace CN88.Accounts.ProblemAccount
{
    using System.Threading.Tasks;
    using CN88.Accounts.ProblemAccount.Criteria;
    using CN88.Accounts.ProblemAccount.Params;
    using CN88.Accounts.Shared.Models;
    using Fanex.Data.Repository;

    public interface ProblemAccountService
    {
        Task<PagedData<ProblemAccount>> GetProblemAccounts(GetProblemAccountsParam param);
    }

    public class ProblemAccountServiceImpl : ProblemAccountService
    {
        private readonly IDynamicRepository dynamicRepository;

        public ProblemAccountServiceImpl(IDynamicRepository dynamicRepository)
        {
            this.dynamicRepository = dynamicRepository;
        }

        public async Task<PagedData<ProblemAccount>> GetProblemAccounts(GetProblemAccountsParam param)
        {
            var criteria = new GetProblemAccountsCriteria(
                param.CustId,
                param.SubAccId,
                param.IsAllDownline,
                param.UplineUsername,
                param.SearchRoleId,
                param.SearchUsername,
                param.ReviewStatus,
                param.FromDate,
                param.ToDate,
                param.PageIndex,
                param.PageSize);

            var problemAccounts = await dynamicRepository.FetchAsync<ProblemAccount>(criteria);

            return new PagedData<ProblemAccount>(
                problemAccounts,
                criteria.PageIndex,
                criteria.PageSize,
                criteria.TotalRows);
        }
    }
}
