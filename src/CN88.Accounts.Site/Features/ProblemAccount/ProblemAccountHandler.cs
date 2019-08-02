namespace CN88.Accounts.Site.Features.ProblemAccount
{
    using System.Threading.Tasks;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.ProblemAccount.ViewModels;
    using MediatR;

    public class ProblemAccountHandler : IAsyncRequestHandler<IndexQuery, IndexViewModel>
    {
        public Task<IndexViewModel> Handle(IndexQuery message)
        {
            return Task.FromResult(new IndexViewModel(message));
        }
    }
}
