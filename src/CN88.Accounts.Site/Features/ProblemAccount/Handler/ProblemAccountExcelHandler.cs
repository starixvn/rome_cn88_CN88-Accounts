namespace CN88.Accounts.Site.Features.ProblemAccount.Handler
{
    using System.Threading.Tasks;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.Shared.Extensions;
    using CN88.Accounts.Shared.Resources;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.Shared;
    using CN88.Accounts.Site.Features.Shared.ViewModels;
    using CN88.Common.Models;
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
                return GetMemberBodyRowMapper(message.ViewerLevel);
            }

            if (message.CustLevel.IsSuperLevel() || (message.CustLevel.IsMasterLevel() && message.ViewerLevel.IsNotHeadlineLevel()))
            {
                return GetTopLevelBodyRowMapper(message.ViewerLevel);
            }

            return GetBodyRowMapper(message.ViewerLevel);
        }

        private Mapper<ProblemAccount> GetTopLevelBodyRowMapper(UserLevel viewerLevel)
        {
            var mapper = new Mapper<ProblemAccount>()
                .Map(A, _ => currentRowIndex++)
                .Map(B, data => data.Username)
                .Map(C, data => data.Name)
                .Map(D, data => data.Nickname ?? string.Empty)
                .Map(E, data => data.NumberOfDownline)
                .Map(F, data => data.StatusName)
                .Map(G, data => data.CreatedDate);

            if (viewerLevel.IsNotHeadlineLevel())
            {
                mapper.Map(H, data => data.ReviewStatus);
            }

            return mapper;
        }

        private Mapper<ProblemAccount> GetBodyRowMapper(UserLevel viewerLevel)
        {
            var mapper = new Mapper<ProblemAccount>()
                .Map(A, _ => currentRowIndex++)
                .Map(B, data => data.UplineUsername)
                .Map(C, data => data.Username)
                .Map(D, data => data.Name)
                .Map(E, data => data.Nickname ?? string.Empty)
                .Map(F, data => data.NumberOfDownline)
                .Map(G, data => data.StatusName)
                .Map(H, data => data.CreatedDate);

            if (viewerLevel.IsNotHeadlineLevel())
            {
                mapper.Map(I, data => data.ReviewStatus);
            }

            return mapper;
        }

        private Mapper<ProblemAccount> GetMemberBodyRowMapper(UserLevel viewerLevel)
        {
            var mapper = new Mapper<ProblemAccount>()
                .Map(A, _ => currentRowIndex++)
                .Map(B, data => data.UplineUsername)
                .Map(C, data => data.Username)
                .Map(D, data => data.Name)
                .Map(E, data => data.Nickname ?? string.Empty)
                .Map(F, data => data.StatusName)
                .Map(G, data => data.CreatedDate);

            if (viewerLevel.IsNotHeadlineLevel())
            {
                mapper.Map(H, data => data.ReviewStatus);
            }

            return mapper;
        }

        private static string GetExcelFileName(ProblemAccountExcelQuery message)
            => $"{Resources.ProblemAccount} {message.FromDate.ToShortDateWithHyphen()} --> {message.ToDate.ToShortDateWithHyphen()} - {Resources.ResourceManager.GetString(message.CustLevel.DisplayName)}";

        private TemplateInfo GetTemplateInfo(ProblemAccountExcelQuery message)
        {
            var templateName = message.ViewerLevel.IsHeadlineLevel() ? $"HL{message.CustLevel.DisplayName}" : message.CustLevel.DisplayName;

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