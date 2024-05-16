namespace EdixleQuery.Contracts.Project
{
    public interface IProjectQuery
    {
        ValueTask<List<ProjectQueryModel>> GetLatestProjectsAsync();
        ValueTask<List<ProjectQueryModel>> SearchProjectsAsync(string query);
        ValueTask<List<ProjectQueryModel>> SearchProjectsAsync(ProjectQuerySearchModel query);
        ValueTask<ProjectQueryModel> GetProjectAsync(long id);
        ValueTask<ProjectsCountViewModel> GetProjectsCountAsync();
    }
}
