namespace CN88.Accounts.Shared.Models
{
    public interface IUser
    {
        int Id { get; }

        string Name { get; }

        string NickName { get; }

        string Language { get; }

        string LanguageKey { get; }

        byte LevelId { get; }

        bool IsInternal { get; }

        int SubAccountId { get; }

        bool IsSubAccount { get; }

        bool IsAllDownline { get; }

        string IP { get; }

        bool AccountManagementEditView { get; }

        bool AccountManagementViewOnly { get; }
    }
}
