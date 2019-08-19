namespace CN88.Accounts.Site.Features.ProblemAccount
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.ProblemAccount.Params;
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Shared.Resources;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.ProblemAccount.ViewModels;
    using CN88.Accounts.Site.Features.Shared;
    using CN88.Accounts.Site.Features.Shared.ViewModels;
    using CN88.Web.Helpers;
    using MediatR;
    using ONEbook.SpreadSheets;
    using static ONEbook.SpreadSheets.Column;

    public class ProblemAccountHandler : ExcelBaseHandler,
        IAsyncRequestHandler<ProblemAccountQuery, IndexViewModel>,
        IAsyncRequestHandler<UpdateReviewStatusCommand, Result>,
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

        public async Task<Result> Handle(UpdateReviewStatusCommand message)
        {
            var param = new UpdateReviewStatusParam(
                message.ViewerId,
                message.SubAccountId,
                message.CustLevelId,
                message.IsAllDownline,
                message.IsAffectedAllDownlines,
                message.SelectedDownlineIds,
                message.LanguageKey,
                message.IP);

            return await problemAccountService.UpdateReviewStatus(param);
        }

        public async Task<ExcelFileViewModel> Handle(ProblemAccountExcelQuery message)
        {
            var param = CreateParam(message);
            var problemAccounts = await problemAccountService.GetProblemAccounts(param);

            var fileContent = workbook
                .UseTemplate(GetTemplateInfo(message.CustLevel))
                .Body(problemAccounts.DataSource, GetBodyRowMapper(message.CustLevel))
                .Header(Resources.ResourceManager.GetString)
                .ExportToBytes();

            return new ExcelFileViewModel("Problem Account", fileContent);
        }

        protected override string GetExcelTemplateDir() => "~/Features/ProblemAccount/ExcelTemplates";

        private static GetProblemAccountsParam CreateParam(ProblemAccountQuery message)
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

        private Mapper<ProblemAccount> GetBodyRowMapper(Web.Models.UserLevel userLevel)
            => BodyRowMapperByLevel[userLevel];

        private IDictionary<Web.Models.UserLevel, Mapper<ProblemAccount>> BodyRowMapperByLevel
            => new Dictionary<Web.Models.UserLevel, Mapper<ProblemAccount>>
            {
                [Web.Models.UserLevel.Master] = GetMasterBodyRowMapper(),
                [Web.Models.UserLevel.Agent] = GetAgentBodyRowMapper(),
                [Web.Models.UserLevel.Member] = GetMemberBodyRowMapper()
            };

        private Mapper<ProblemAccount> GetMasterBodyRowMapper()
            => new Mapper<ProblemAccount>()
                .Map(A, _ => currentRowIndex++)
                .Map(B, data => data.Username)
                .Map(C, data => data.Name)
                .Map(D, data => data.Nickname)
                .Map(E, data => data.NumberOfDownline)
                .Map(F, data => data.StatusName)
                .Map(G, data => data.CreatedDate)
                .Map(H, data => data.ReviewStatus);



        private Mapper<ProblemAccount> GetAgentBodyRowMapper()
            => new Mapper<ProblemAccount>()
                .Map(A, _ => currentRowIndex++)
                .Map(B, data => data.UplineUsername)
                .Map(C, data => data.Username)
                .Map(D, data => data.Name)
                .Map(E, data => data.Nickname)
                .Map(F, data => data.NumberOfDownline)
                .Map(G, data => data.StatusName)
                .Map(H, data => data.CreatedDate)
                .Map(I, data => data.ReviewStatus);

        private Mapper<ProblemAccount> GetMemberBodyRowMapper()
            => new Mapper<ProblemAccount>()
                .Map(A, _ => currentRowIndex++)
                .Map(B, data => data.UplineUsername)
                .Map(C, data => data.Username)
                .Map(D, data => data.Name)
                .Map(E, data => data.Nickname)
                .Map(F, data => data.StatusName)
                .Map(G, data => data.CreatedDate)
                .Map(H, data => data.ReviewStatus);

    }
}
