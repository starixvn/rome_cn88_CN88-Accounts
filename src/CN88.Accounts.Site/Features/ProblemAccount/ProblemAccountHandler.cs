namespace CN88.Accounts.Site.Features.ProblemAccount
{
    using System.Threading.Tasks;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.ProblemAccount.Params;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.ProblemAccount.ViewModels;
    using CN88.Web.Helpers;
    using MediatR;

    public class ProblemAccountHandler : IAsyncRequestHandler<IndexQuery, IndexViewModel>
    {
        private readonly BrowserHelper browserHelper;
        private readonly ProblemAccountService problemAccountService;

        public ProblemAccountHandler(
            BrowserHelper browserHelper,
            ProblemAccountService problemAccountService)
        {
            this.browserHelper = browserHelper;
            this.problemAccountService = problemAccountService;
        }

        public async Task<IndexViewModel> Handle(IndexQuery message)
        {
            var param = new GetProblemAccountsParam(
                message.ViewerId,
                message.SubAccountId,
                message.IsAllDownline,
                message.UplineUsername,
                message.CustLevelId,
                message.SearchUsername,
                message.SelectedReviewStatus,
                message.FromDate,
                message.ToDate,
                message.PageIndex,
                message.PageSize);

            var problemAccounts = await problemAccountService.GetProblemAccounts(param);

            return new IndexViewModel(message, browserHelper.IsMobile(), browserHelper, problemAccounts);
        }
    }
}
