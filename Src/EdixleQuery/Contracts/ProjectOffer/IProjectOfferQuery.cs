namespace EdixleQuery.Contracts.ProjectOffer
{
    public interface IProjectOfferQuery
    {
        ValueTask<List<ProjectOfferQueryModel>> GetProjectOffersAsync(long projectId);
        ValueTask<List<ProjectOfferQueryModel>> GetEditorProjectOffers(long editorPageId);
    }
}
