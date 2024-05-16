namespace EdixleQuery.Contracts.EmployerPage
{
    public interface IEmployerPageQuery
    {
        ValueTask<EmployerPageQueryModel> GetPageAsync(long pageId);
        ValueTask<bool> IsEmployerAsync(long accountId);
    }
}
