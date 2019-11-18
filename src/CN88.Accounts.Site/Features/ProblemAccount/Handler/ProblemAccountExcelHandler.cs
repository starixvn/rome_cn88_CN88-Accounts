namespace CN88.Accounts.Site.Features.ProblemAccount.Handler
{
    using System.Threading.Tasks;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.Shared.Extensions;
    using CN88.Accounts.Shared.Resources;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.Shared;
    using CN88.Accounts.Site.Features.Shared.ViewModels;
    using MediatR;
    using ONEbook.SpreadSheets;
    using static ONEbook.SpreadSheets.Column;

    public class ProblemAccountExcelHandler : ExcelBaseHandler, IAsyncRequestHandler<ProblemAccountExcelQuery, ExcelFileViewModel>
    {
        private int currentRowIndex = 1;
        private readonly ProblemAccountService problemAccountService;
        private readonly Workbook<ProblemAccount> workbook;

        public ProblemAccountExcelHandler(
            ProblemAccountService problemAccountService,
            Workbook<ProblemAccount> workbook)
        {
            this.problemAccountService = problemAccountService;
            this.workbook = workbook;
        }

        public async Task<ExcelFileViewModel> Handle(ProblemAccountExcelQuery message)
        {
            var param = ProblemAccountHandler.CreateParam(message);
            var problemAccounts = await problemAccountService.GetProblemAccounts(param);

            var fileContent = workbook
                .UseTemplate(GetTemplateInfo(message))
                .Header(Resources.ResourceManager.GetString)
                .Body(problemAccounts.DataSource, GetBodyRowMapper(message))
                .ExportToBytes();

            return new ExcelFileViewModel(GetExcelFileName(message), fileContent);
        }

        protected override string GetExcelTemplateDir() => "~/Features/ProblemAccount/ExcelTemplates";

        internal Mapper<ProblemAccount> GetBodyRowMapper(ProblemAccountExcelQuery message)
        {
            if (message.CustLevel.IsMemberLevel())
            {
                return GetMemberBodyRowMapper();
            }

            if (message.CustLevel.IsSuperLevel() || (message.CustLevel.IsMasterLevel() && message.ViewerLevel.IsNotHeadlineLevel()))
            {
                return GetTopLevelBodyRowMapper();
            }

            return GetBodyRowMapper();
        }

        private Mapper<ProblemAccount> GetTopLevelBodyRowMapper()
            => new Mapper<ProblemAccount>()
                .Map(A, _ => currentRowIndex++)
                .Map(B, data => data.Username)
                .Map(C, data => data.Name)
                .Map(D, data => data.Nickname ?? string.Empty)
                .Map(E, data => data.NumberOfDownline)
                .Map(F, data => data.StatusName)
                .Map(G, data => data.CreatedDate)
                .Map(H, data => data.ReviewStatus);

        private Mapper<ProblemAccount> GetBodyRowMapper()
            => new Mapper<ProblemAccount>()
                .Map(A, _ => currentRowIndex++)
                .Map(B, data => data.UplineUsername)
                .Map(C, data => data.Username)
                .Map(D, data => data.Name)
                .Map(E, data => data.Nickname ?? string.Empty)
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
                .Map(E, data => data.Nickname ?? string.Empty)
                .Map(F, data => data.StatusName)
                .Map(G, data => data.CreatedDate)
                .Map(H, data => data.ReviewStatus);

        private static string GetExcelFileName(ProblemAccountExcelQuery message)
            => $"{Resources.ProblemAccount} {message.FromDate.ToShortDateWithHyphen()} --> {message.ToDate.ToShortDateWithHyphen()} - {Resources.ResourceManager.GetString(message.CustLevel.DisplayName)}";

        private TemplateInfo GetTemplateInfo(ProblemAccountExcelQuery message)
        {
            var templateName = message.ViewerLevel.IsHeadlineLevel() && message.CustLevel.IsMasterLevel() ? "HLMaster" : message.CustLevel.DisplayName;

            return new TemplateInfo(
                   GetExcelTemplatePath(templateName),
                   noInfomationText: Resources.NoInformationIsAvailable,
                   titleStartIndex: -1,
                   headerStartIndex: 1,
                   bodyStartIndex: 2,
                   bodyEndIndex: 3);
        }
    }
}