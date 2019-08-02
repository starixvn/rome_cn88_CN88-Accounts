namespace CN88.Accounts.Site.Features.Shared
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using CN88.Web.Controllers;
    using MediatR;

    public abstract class SiteBaseController : CN88BaseController
    {
        private const string NoInformationPageViewPath = "~/Shared/Views/NoInformation.cshtml";

        protected readonly IMediator mediator;

        public SiteBaseController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        protected async Task<ActionResult> ViewAsync<TResponse>(IRequest<TResponse> message)
            => (message as BaseQuery).IsValid()
                ? View(await mediator.Send(message))
                : View(NoInformationPageViewPath);
    }
}
