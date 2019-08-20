namespace CN88.Accounts.Site.Features.Shared
{
    using System.Net;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using CN88.Accounts.Shared.Resources;
    using CN88.Accounts.Site.Features.Shared.ViewModels;
    using CN88.Common.Models;
    using CN88.Web.Controllers;
    using MediatR;

    public abstract class SiteBaseController : CN88BaseController
    {
        private const string InvalidExcelFileName = "Invalid";
        private const string NoInformationPageViewPath = "~/Features/Shared/Views/NoInformation.cshtml";

        protected readonly IMediator mediator;

        protected SiteBaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected async Task<ActionResult> ViewAsync<TResponse>(IRequest<TResponse> message)
            => (message as BaseQuery).IsValid()
                ? View(await mediator.Send(message))
                : View(NoInformationPageViewPath);

        protected async Task<ActionResult> JsonDataAsync<TResponse>(
            IRequest<TResponse> message,
            JsonRequestBehavior jsonRequestBehavior = JsonRequestBehavior.AllowGet)
            => (message as BaseQuery).IsValid()
                ? Json(await mediator.Send(message), jsonRequestBehavior)
                : Json(new Result((int)HttpStatusCode.Forbidden, Resources.NotAuthorized));


        protected async Task<FileContentResult> ExcelView(IRequest<ExcelFileViewModel> message)
            => (message as BaseQuery).IsValid()
                ? GetExcelContent(await mediator.Send(message))
                : GetExcelContent(new ExcelFileViewModel(InvalidExcelFileName, new byte[] { }));

        private FileContentResult GetExcelContent(ExcelFileViewModel viewModel)
            => File(viewModel.Content, viewModel.ContentType, viewModel.Name);
    }
}
