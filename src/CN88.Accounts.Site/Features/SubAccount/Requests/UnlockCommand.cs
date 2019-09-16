namespace CN88.Accounts.Site.Features.SubAccount.Requests
{
    using CN88.Accounts.Shared.Models;
    using CN88.Accounts.Site.Features.Shared;
    using CN88.Common.Models;
    using MediatR;

    public class UnlockCommand : BaseQuery, IRequest<Result>
    {
        public UnlockCommand(IUser viewer)
            : base(viewer)
        {
        }

        public override bool IsValid()
            => base.IsValid() && !IsInternal;
    }
}