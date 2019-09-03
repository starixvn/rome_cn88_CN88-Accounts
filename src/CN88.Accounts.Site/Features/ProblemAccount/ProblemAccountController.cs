namespace CN88.Accounts.Site.Features.ProblemAccount
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.Shared;
    using CN88.Web.Filters;
    using CN88.Web.Helpers;
    using MediatR;

    public class ProblemAccountController : SiteBaseController
    {
        private readonly BrowserHelper browserHelper;

        public ProblemAccountController(IMediator mediator, BrowserHelper browserHelper)
            : base(mediator)
        {
            this.browserHelper = browserHelper;
        }

        public async Task<ActionResult> Index(ProblemAccountQuery message)
            => browserHelper.IsMobile()
                ? await ViewAsync(message, "IndexMobile")
                : await ViewAsync(message);

        public async Task<ActionResult> Excel(ProblemAccountExcelQuery message)
            => await ExcelView(message);

        public async Task<ActionResult> UpdateReviewStatus(UpdateReviewStatusQuery message)
            => await ViewAsync(message);

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> UpdateReviewStatus(UpdateReviewStatusCommand message)
            => await JsonDataAsync(message);
    }
}
