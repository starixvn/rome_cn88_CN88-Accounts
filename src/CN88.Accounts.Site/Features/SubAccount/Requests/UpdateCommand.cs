namespace CN88.Accounts.Site.Features.SubAccount.Requests
{
    using System.Web.Mvc;
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.Shared;
    using CN88.Common.Models;
    using MediatR;

    public class UpdateCommand : BaseQuery, IRequest<Result>
    {
        public UpdateCommand(IUser viewer)
            : base(viewer)
        {
        }

        [AllowHtml]
        public string Name { get; set; }

        public string PageAccess { get; set; }

        public bool IsSelectAllDownline { get; set; }

        public string DownlineId { get; set; }

        public override bool IsValid()
            => base.IsValid()
            && !IsInternal
            && (IsSelectAllDownline || !string.IsNullOrWhiteSpace(DownlineId));
    }
}