namespace EdixleQuery.Contracts.ArticleCategory
{
    public interface IArticleCategoryQuery
    {
        ValueTask<List<ArticleCategoryQueryModel>> GetArticleCategoriesAsync();
        ValueTask<ArticleCategoryQueryModel> GetArticleCategoryAsync(string slug);
        ValueTask<List<ArticleCategoryQueryModel>> GetCategoriesWithArticlesAsync();
    }
}
