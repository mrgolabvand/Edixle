namespace EdixleQuery.Contracts.PersonalPage
{
    public interface IPersonalPageQuery
    {
        ValueTask<PersonalPageQueryModel> GetPageAsync(long pageId);
        ValueTask<PersonalPageQueryModel> GetPageAsync(string slug, long pageId);
        ValueTask<bool> IsEditorAsync(long accountId);
        ValueTask<string> GetPersonalPageSlugAsync(long accountId);
        ValueTask<List<PersonalPageQueryModel>> SearchAsync(string searchWord, int orderId = 1, int minPay = 1000,
            int maxPay = 10000000, bool isBusy = false, string payDate = null);
        ValueTask<List<PersonalPageQueryModel>> GetBestEditorsAsync();
        ValueTask<PersonalPagesCountViewModel> GetPersonalPagesCountAsync();
    }
}
