namespace EdixleQuery.Contracts.Portfolio
{
    public interface IPortfolioQuery
    {
        ValueTask<List<PortfolioQueryModel>> SearchAsync(PortfolioSearchQuery searchModel);
        ValueTask<List<PortfolioQueryModel>> UserSearchAsync(PortfolioUserSearchQuery searchModel);
        ValueTask<List<PortfolioQueryModel>> SettingSearchAsync(PortfolioSearchQuery searchModel);
        ValueTask<List<PortfolioQueryModel>> GetEditorPortfoliosAsync(long pageId);
        ValueTask<PortfolioQueryModel> GetPortfolioByAsync(string slug);
        ValueTask<List<PortfolioQueryModel>> GetPortfoliosByCategoryAsync(string category);
        ValueTask<List<PortfolioQueryModel>> GetLatestPortfoliosAsync();
        ValueTask<List<PortfolioQueryModel>> GetRelatedPortfoliosAsync(string tags);
        ValueTask<PortfolioQueryModel> GetFileFromPortfolioAsync(long id);
        ValueTask<List<PortfolioQueryModel>> GetTopPortfoliosAsync();
    }
}
