namespace EdixleQuery.Contracts.Account
{
    public interface IAccountQuery
    {
        ValueTask<AccountQueryModel> GetAccountAsync(long id);
    }
}
