namespace CN88.Accounts.Site.Features.ProblemAccount.Requests
{
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.Shared.ViewModels;
    using MediatR;

    public class ProblemAccountExcelQuery : ProblemAccountQuery, IRequest<ExcelFileViewModel>
    {
        public ProblemAccountExcelQuery(IUser viewer)
            : base(viewer)
        {
            PageIndex = 1;
            PageSize = int.MaxValue;
        }
    }
}
