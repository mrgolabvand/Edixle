namespace EdixleQuery.Contracts.PortfolioCategory
{
    public interface IPortfolioCategoryQuery
    {
        ValueTask<List<PortfolioCategoryQueryModel>> GetCategoriesAsync();
    }
}
