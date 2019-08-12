namespace CN88.Accounts.Site.Features.Shared
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using CN88.Accounts.Site.Features.Shared.ViewModels;
    using CN88.Web.Controllers;
    using MediatR;

    public abstract class SiteBaseController : CN88BaseController
    {
        private const string InvalidExcelFileName = "Invalid";
        private const string NoInformationPageViewPath = "~/Shared/Views/NoInformation.cshtml";

        protected readonly IMediator mediator;

        protected SiteBaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected async Task<ActionResult> ViewAsync<TResponse>(IRequest<TResponse> message)
            => (message as BaseQuery).IsValid()
                ? View(await mediator.Send(message))
                : View(NoInformationPageViewPath);

        protected async Task<FileContentResult> ExcelView(IRequest<ExcelFileViewModel> message)
            => (message as BaseQuery).IsValid()
                ? GetExcelContent(await mediator.Send(message))
                : GetExcelContent(new ExcelFileViewModel(InvalidExcelFileName, new byte[] { }));

        private FileContentResult GetExcelContent(ExcelFileViewModel viewModel)
            => File(viewModel.Content, viewModel.ContentType, viewModel.Name);
    }
}
