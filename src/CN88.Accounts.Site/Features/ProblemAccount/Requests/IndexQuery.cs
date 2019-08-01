namespace CN88.Accounts.Site.Features.ProblemAccount.Requests
{
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.ProblemAccount.ViewModels;
    using CN88.Accounts.Site.Shared;
    using MediatR;

    public class IndexQuery : BaseQuery, IRequest<IndexViewModel>
    {
        public IndexQuery(IUser viewer)
            : base(viewer)
        {
        }
    }
}
