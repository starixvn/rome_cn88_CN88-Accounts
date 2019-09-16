namespace CN88.Accounts.Site.Features.SubAccount
{
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using CN88.Accounts.Site.Features.Shared;
    using CN88.Accounts.Site.Features.SubAccount.Requests;
    using CN88.Web.Filters;
    using CN88.Web.Helpers;
    using MediatR;

    public class SubAccountController : SiteBaseController
    {
        private readonly BrowserHelper browserHelper;

        public SubAccountController(IMediator mediator, BrowserHelper browserHelper)
            : base(mediator)
        {
            this.browserHelper = browserHelper;
        }

        public async Task<ActionResult> Index(IndexQuery message)
            => await ViewAsync(message);

        public async Task<ActionResult> New(NewQuery message)
            => await ViewAsync(message, browserHelper.IsMobile() ? "NewMobile" : "New");

        [HttpPost, AntiXsrfFilter]
        public async Task<ActionResult> New(NewCommand message)
            => await JsonDataAsync(message);

        public async Task<ActionResult> Edit(UpdateQuery message)
        {
            ViewBag.ShowAccountManagementPermission = message.ShowAccountManagementPermission;

            return await ViewAsync(message, browserHelper.IsMobile() ? "EditMobile" : "Edit");
        }

        [HttpPost, AntiXsrfFilter, CheckSubAccBelongingFilter]
        public async Task<ActionResult> Edit(UpdateCommand message)
            => await JsonDataAsync(message);

        [HttpPost, AntiXsrfFilter, CheckSubAccBelongingFilter]
        public async Task<ActionResult> Unlock(UnlockCommand message)
            => await JsonDataAsync(message);

        [HttpPost, AntiXsrfFilter, CheckSubAccBelongingFilter]
        public async Task<ActionResult> Delete(DeleteCommand message)
            => await JsonDataAsync(message);
    }
}