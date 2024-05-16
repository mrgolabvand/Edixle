namespace EdixleQuery.Contracts.Article
{
    public interface IArticleQuery
    {
        ValueTask<List<ArticleQueryModel>> GetLatestArticlesAsync();
        ValueTask<List<ArticleQueryModel>> GetArticlesAsync();
        ValueTask<ArticleQueryModel> GetArticleAsync(string slug);
    }
}
