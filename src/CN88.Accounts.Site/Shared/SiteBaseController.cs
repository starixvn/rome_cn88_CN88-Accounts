namespace CN88.Accounts.Site.Shared
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

        protected async Task<ActionResult> ViewAsync<TResponse>(IRequest<TResponse> message, string viewName = "")
        {
            if ((message as BaseQuery).IsValid())
            {
                viewName = string.IsNullOrWhiteSpace(viewName)
                    ? ControllerContext.RouteData.Values["Action"].ToString()
                    : viewName;

                return View($"{FeatureFolder}/{viewName}.cshtml", await mediator.Send(message));
            }

            return View(NoInformationPageViewPath);
        }

        protected virtual string FeatureFolder
        {
            get => $"~/Features/{ControllerContext.RouteData.Values["Controller"]}/Views";
        }
    }
}
