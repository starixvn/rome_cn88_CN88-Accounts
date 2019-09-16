namespace CN88.Accounts.Site.Features.SubAccount.Requests
{
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.Shared;
    using CN88.Accounts.Site.Features.SubAccount.ViewModels;
    using MediatR;

    public class NewQuery : BaseQuery, IRequest<NewViewModel>
    {
        public NewQuery(IUser viewer)
            : base(viewer)
        {
        }

        public bool ShowAccountManagementPermission => ViewerLevel.IsSuperLevel();
    }
}