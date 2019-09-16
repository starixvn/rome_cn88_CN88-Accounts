namespace CN88.Accounts.Site.Features.SubAccount.Requests
{
    using System.Collections.Generic;
    using CN88.Accounts.Shared.Models;
    using CN88.Common.Models;
    using MediatR;

    public class NewCommand : UpdateCommand, IRequest<Result>
    {
        public NewCommand(IUser viewer)
            : base(viewer)
        {
        }

        public string Suffix1 { get; set; }

        public string Suffix2 { get; set; }

        public string Password { get; set; }

        public IEnumerable<string> InvalidPasswords
        {
            get
            {
                yield return ViewerName;
            }
        }
    }
}