namespace EdixleQuery.Contracts.Category
{
    public interface ICategoryQuery
    {
        ValueTask<string> GetCategoryNameAsync(string slug);
        ValueTask<CategoryQueryModel> GetCategoriesAsync();
        ValueTask<List<BaseCategoryQueryModel>> GetBaseCategoriesAsync();
        ValueTask<List<ShowCategoryQueryModel>> GetCategoryAsync(string name);
    }
}
