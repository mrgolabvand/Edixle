namespace EdixleQuery.Contracts.Feature
{
    public interface IFeatureQuery
    {
        ValueTask<List<FeatureQueryModel>> GetFeaturesAsync();
    }
}