namespace CN88.Accounts.Site.Features.ProblemAccount.Handler
{
    using System.Threading.Tasks;
    using CN88.Accounts.ProblemAccount;
    using CN88.Accounts.ProblemAccount.Params;
    using CN88.Accounts.Site.Features.ProblemAccount.Requests;
    using CN88.Accounts.Site.Features.ProblemAccount.ViewModels;
    using CN88.Common.Models;
    using MediatR;

    public class ProblemAccountHandler :
        IAsyncRequestHandler<ProblemAccountQuery, IndexViewModel>,
        IRequestHandler<UpdateReviewStatusQuery, UpdateReviewStatusViewModel>,
        IAsyncRequestHandler<UpdateReviewStatusCommand, Result>
    {
        private readonly ProblemAccountService problemAccountService;

        public ProblemAccountHandler(ProblemAccountService problemAccountService)
        {
            this.problemAccountService = problemAccountService;
        }

        public async Task<IndexViewModel> Handle(ProblemAccountQuery message)
        {
            var param = CreateParam(message);
            var problemAccounts = await problemAccountService.GetProblemAccounts(param);

            return new IndexViewModel(message, problemAccounts);
        }

        public UpdateReviewStatusViewModel Handle(UpdateReviewStatusQuery message)
            => new UpdateReviewStatusViewModel(message);

        public async Task<Result> Handle(UpdateReviewStatusCommand message)
        {
            var param = new UpdateReviewStatusParam(
                message.ViewerId,
                message.SubAccountId,
                message.ViewerLevelId,
                message.CustLevelId,
                message.IsAllDownline,
                message.IsAffectedAllDownlines,
                message.SelectedDownlineIds,
                message.LanguageKey,
                message.IP);

            return await problemAccountService.UpdateReviewStatus(param);
        }

        protected internal static GetProblemAccountsParam CreateParam(ProblemAccountQuery message)
            => new GetProblemAccountsParam(
                message.ViewerId,
                message.ViewerLevelId,
                message.SubAccountId,
                message.IsAllDownline,
                message.UplineUsername,
                message.CustLevelId,
                message.CustName,
                message.SelectedReviewStatus,
                message.FromDate,
                message.ToDate,
                message.PageIndex,
                message.PageSize);
    }
}