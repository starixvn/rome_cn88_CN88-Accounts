namespace CN88.Accounts.Site.Features.ProblemAccount
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Shared;
    using MediatR;

    public class ProblemAccountController : SiteBaseController
    {
        public ProblemAccountController(IMediator mediator)
            : base(mediator)
        {
        }

        public async Task<ActionResult> Index(IndexQuery message)
            => await ViewAsync(message);
    }
}
