namespace EdixleQuery.Contracts.PortfolioBaseCategory
{
    public interface IPortfolioBaseCategoryQuery
    {
        ValueTask<List<PortfolioBaseCategoryQueryModel>> GetListAsync();
    }
}
