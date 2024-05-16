namespace EdixleQuery.Contracts.Skill
{
    public interface ISkillQuery
    {
        ValueTask<List<SkillQueryModel>> GetListAsync();
        ValueTask<List<SkillQueryModel>> GetListAsync(string slug);
        ValueTask<List<SkillQueryModel>> GetSettingListAsync();
    }
}
