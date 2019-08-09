namespace CN88.Accounts.Site.Features.ProblemAccount
{
    using System.Threading.Tasks;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.ProblemAccount.Params;
    using CN88.Accounts.ProblemAccount.Resources;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.ProblemAccount.ViewModels;
    using CN88.Accounts.Site.Features.Shared;
    using CN88.Accounts.Site.Features.Shared.ViewModels;
    using CN88.Web.Helpers;
    using CN88.Web.Models;
    using MediatR;
    using ONEbook.SpreadSheets;
    using static ONEbook.SpreadSheets.Column;

    public class ProblemAccountHandler : ExcelBaseHandler,
        IAsyncRequestHandler<ProblemAccountQuery, IndexViewModel>,
        IAsyncRequestHandler<ProblemAccountExcelQuery, ExcelFileViewModel>
    {
        private int currentRowIndex = 1;
        private readonly BrowserHelper browserHelper;
        private readonly ProblemAccountService problemAccountService;
        private readonly Workbook<ProblemAccount> workbook;

        public ProblemAccountHandler(
            BrowserHelper browserHelper,
            ProblemAccountService problemAccountService,
            Workbook<ProblemAccount> workbook)
        {
            this.browserHelper = browserHelper;
            this.problemAccountService = problemAccountService;
            this.workbook = workbook;
        }

        public async Task<IndexViewModel> Handle(ProblemAccountQuery message)
        {
            var param = CreateParam(message);
            var problemAccounts = await problemAccountService.GetProblemAccounts(param);

            return new IndexViewModel(message, browserHelper.IsMobile(), browserHelper, problemAccounts);
        }

        public async Task<ExcelFileViewModel> Handle(ProblemAccountExcelQuery message)
        {
            var param = CreateParam(message);
            var problemAccounts = await problemAccountService.GetProblemAccounts(param);

            var fileContent = workbook
                .UseTemplate(GetTemplateInfo(message.CustLevel))
                .Body(problemAccounts.DataSource, GetBodyRowMapper(message.CustLevel))
                .Header(ProblemAccountResources.ResourceManager.GetString)
                .ExportToBytes();

            return new ExcelFileViewModel("Problem Account", fileContent);
        }

        protected override string GetExcelTemplateDir() => "~/Features/ProblemAccount";

        private GetProblemAccountsParam CreateParam(ProblemAccountQuery message)
            => new GetProblemAccountsParam(
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

        private Mapper<ProblemAccount> GetBodyRowMapper(UserLevel userLevel)
        {
            var mapper = new Mapper<ProblemAccount>()
                .Map(A, _ => currentRowIndex++)
                .Map(B, data => data.Username)
                .Map(C, data => data.Name)
                .Map(D, data => data.Nickname);

            if (userLevel.IsMasterLevel() || userLevel.IsAgentLevel())
            {
                mapper.Map(E, data => data.NumberOfDownline)
                    .Map(F, data => data.StatusName)
                    .Map(G, data => data.CreatedDate)
                    .Map(H, data => data.ReviewStatus);
            }
            else
            {
                mapper.Map(E, data => data.StatusName)
                    .Map(F, data => data.CreatedDate)
                    .Map(G, data => data.ReviewStatus);
            }

            return mapper;
        }
    }
}
