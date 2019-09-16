namespace CN88.Accounts.Site.Features.SubAccount.Requests
{
    using CN88.Accounts.Shared.Constants;
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.Shared;
    using CN88.Accounts.Site.Features.SubAccount.ViewModels;
    using MediatR;

    public class IndexQuery : BaseQuery, IRequest<IndexViewModel>
    {
        public IndexQuery(IUser viewer)
            : base(viewer)
        {
        }

        public bool ShowActionButtons => ViewerStatusId == CustomerStatusConstants.Active;

        public bool ShowAccountManagementPermission => ViewerLevel.IsSuperLevel();
    }
}