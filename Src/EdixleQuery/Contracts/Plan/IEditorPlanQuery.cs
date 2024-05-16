namespace EdixleQuery.Contracts.Plan
{
    public interface IEditorPlanQuery
    {
        ValueTask<EditorPlanQueryModel> GetEditorPagePlanAsync(long pageId);
    }
}
