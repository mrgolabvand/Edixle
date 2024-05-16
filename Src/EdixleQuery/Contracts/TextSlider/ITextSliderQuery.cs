namespace EdixleQuery.Contracts.TextSlider
{
    public interface ITextSliderQuery
    {
        ValueTask<List<TextSliderQueryModel>> GetListAsync();
    }
}
