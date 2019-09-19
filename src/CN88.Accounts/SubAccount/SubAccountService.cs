namespace CN88.Accounts.SubAccount
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using CN88.Accounts.Shared.Resources;
    using CN88.Accounts.SubAccount.Constants;
    using CN88.Accounts.SubAccount.Criteria;
    using CN88.Accounts.SubAccount.Models;
    using CN88.Accounts.SubAccount.Params;
    using CN88.Common.Models;
    using Fanex.Data.Repository;

    public interface SubAccountService
    {
        Task<IEnumerable<SubAccountItem>> GetSubAccountsAsync(int custId);

        Task<SubAccountItem> GetSubAccountAsync(int custId, int subAccId);

        Task<Result> CreateSubAccountAsync(CreateSubAccountParam param);

        Task<Result> UpdateSubAccountAsync(UpdateSubAccountParam param);

        Task<Result> UnlockSubAccountAsync(UnlockSubAccountParam param);

        Task<Result> DeleteSubAccountAsync(UnlockSubAccountParam param);

        Task<Tuple<bool, IEnumerable<ManagedAccount>>> GetManagedAccounts(int custId, byte roleId, int subAccId);
    }

    public class SubAccountServiceImpl : SubAccountService
    {
        private readonly IDynamicRepository dynamicRepository;
        private readonly SubAccountValidator subAccountValidator;

        public SubAccountServiceImpl(
            IDynamicRepository dynamicRepository,
            SubAccountValidator subAccountValidator)
        {
            this.dynamicRepository = dynamicRepository;
            this.subAccountValidator = subAccountValidator;
        }

        public async Task<IEnumerable<SubAccountItem>> GetSubAccountsAsync(int custId)
            => await dynamicRepository.FetchAsync<SubAccountItem>(new GetSubAccountListCriteria(custId));

        public async Task<SubAccountItem> GetSubAccountAsync(int custId, int subAccId)
        {
            var subAccount = await dynamicRepository.GetAsync<SubAccountItem>(new GetSubAccountCriteria(custId, subAccId));

            if (subAccount != null)
            {
                subAccount.SubAccId = subAccId;
            }

            return subAccount;
        }

        public async Task<Result> CreateSubAccountAsync(CreateSubAccountParam param)
        {
            var result = ValidateCreateSubAccountParam(param);

            if (result.IsSuccess)
            {
                param.UserName = $"{param.ActorName}{SubAccountConstants.SubAccountPadding}{param.Suffix1}{param.Suffix2}";
                param.Password = subAccountValidator.Encode(param.Password);
                param.PageAccess = string.Join(string.Empty, subAccountValidator.FilterWithValidPermissions(param.PageAccess));

                var command = new CreateSubAccountCommand(
                    param.UserId,
                    param.Name,
                    param.UserName,
                    param.Password,
                    param.PageAccess,
                    param.LanguageKey,
                    param.LoginIP,
                    param.UserId);
                result = await ExecuteAsync(command);

                if (result.IsSuccess)
                {
                    var updateManagedAccountResult = await UpdateManagedAccount(
                        new UpdateManagedAccountParam(
                            param.UserId,
                            param.RoleId,
                            command.SubAccId,
                            param.IsAllDownline,
                            param.DownlineId,
                            param.LanguageKey,
                            param.LoginIP,
                            param.UserId));

                    return updateManagedAccountResult.Item2;
                }
            }

            return result;
        }

        public async Task<Result> UpdateSubAccountAsync(UpdateSubAccountParam param)
        {
            var subAccount = await GetSubAccountAsync(param.UserId, param.SubAccId);

            return subAccount != null
                ? await UpdateSubAccountAndKickoutAsync(param, subAccount.PageAccess)
                : Result.CreateSuccessResult(message: Resources.UpdateSuccessfully);
        }

        internal Result ValidateCreateSubAccountParam(CreateSubAccountParam param)
        {
            var result = subAccountValidator.ValidateEmptyPassword(param.Password);

            if (!result.IsSuccess)
            {
                return result;
            }

            var rawPassword = subAccountValidator.Decrypt(param.Password, param.HashKey);
            result = subAccountValidator.IsMatchPasswordRules(rawPassword, param.InvalidPasswords);

            if (!result.IsSuccess)
            {
                return result;
            }

            result = subAccountValidator.ValidateSuffix(param.Suffix1, param.Suffix2);

            if (!result.IsSuccess)
            {
                return result;
            }

            param.Password = rawPassword;
            return Result.CreateSuccessResult();
        }

        internal async Task<Result> UpdateSubAccountAndKickoutAsync(UpdateSubAccountParam param, string oldPageAccess)
        {
            var pageAccessList = subAccountValidator.FilterWithValidPermissions(param.PageAccess);
            var oldPageAccessList = subAccountValidator.GetAccountManagementPermission(oldPageAccess);
            param.PageAccess = string.Join(string.Empty, pageAccessList.Concat(oldPageAccessList));

            var command = new UpdateSubAccountCommand(
                    param.UserId,
                    param.SubAccId,
                    param.Name,
                    param.PageAccess,
                    param.LanguageKey,
                    param.LoginIP,
                    param.UserId);
            var result = await ExecuteAsync(command);

            if (result.IsSuccess)
            {
                var updatedManagedAccountResult = await UpdateManagedAccount(
                    new UpdateManagedAccountParam(
                        param.UserId,
                        param.RoleId,
                        param.SubAccId,
                        param.IsAllDownline,
                        param.DownlineId,
                        param.LanguageKey,
                        param.LoginIP,
                        param.UserId));

                if (oldPageAccess != param.PageAccess || updatedManagedAccountResult.Item1)
                {
                    await KickoutSubAccountAsync(param.UserId, param.SubAccId);
                }

                return updatedManagedAccountResult.Item2;
            }

            return result;
        }

        public async Task<Result> UnlockSubAccountAsync(UnlockSubAccountParam param)
        {
            var command = new UnlockSubAccountCommand(param.UserId, param.SubAccId, param.LoginIP, param.LanguageKey);
            await dynamicRepository.ExecuteAsync(command);

            return new Result(command.ErrCode, command.ErrMsg);
        }

        public async Task<Result> DeleteSubAccountAsync(UnlockSubAccountParam param)
        {
            var command = new DeleteSubAccountCommand(param.UserId, param.SubAccId, param.LoginIP, param.LanguageKey);
            await dynamicRepository.ExecuteAsync(command);

            var result = new Result(command.ErrCode, command.ErrMsg);

            // Kickout.
            if (result.IsSuccess)
            {
                await KickoutSubAccountAsync(param.UserId, param.SubAccId);
            }

            return result;
        }

        private async Task KickoutSubAccountAsync(int actorId, int subAccountId)
            => await dynamicRepository.ExecuteAsync(new KickoutSubAccountCommand(actorId, subAccountId));

        private async Task<Result> ExecuteAsync(BaseSubAccountCommand command)
        {
            await dynamicRepository.ExecuteAsync(command);

            return new Result(command.ErrCode, command.ErrMsg);
        }

        public async Task<Tuple<bool, IEnumerable<ManagedAccount>>> GetManagedAccounts(int custId, byte roleId, int subAccId)
        {
            var criteria = new GetManagedAccountCriteria(custId, roleId, subAccId);
            var downlines = await dynamicRepository.FetchAsync<ManagedAccount>(criteria);

            return Tuple.Create(criteria.IsAllDownline, downlines);
        }

        internal async Task<Tuple<bool, Result>> UpdateManagedAccount(UpdateManagedAccountParam param)
        {
            var command = new UpdateManagedAccountCommand(
                param.CustId,
                param.RoleId,
                param.SubAccId,
                param.IsAllDownline,
                param.DownlineId,
                param.LanguageKey,
                param.LoginIP,
                param.UserId);
            await dynamicRepository.ExecuteAsync(command);

            return Tuple.Create(command.IsUpdated, new Result(command.ErrCode, command.ErrMsg));
        }
    }
}