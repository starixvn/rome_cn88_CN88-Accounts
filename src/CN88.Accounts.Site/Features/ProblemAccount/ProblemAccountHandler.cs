namespace CN88.Accounts.Site.Features.ProblemAccount
{
    using System.Threading.Tasks;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.ProblemAccount.ViewModels;
    using CN88.Web.Helpers;
    using MediatR;

    public class ProblemAccountHandler : IAsyncRequestHandler<IndexQuery, IndexViewModel>
    {
        private readonly BrowserHelper browserHelper;

        public ProblemAccountHandler(BrowserHelper browserHelper)
        {
            this.browserHelper = browserHelper;
        }

        public Task<IndexViewModel> Handle(IndexQuery message)
        {
            return Task.FromResult(new IndexViewModel(message, browserHelper));
        }
    }
}
