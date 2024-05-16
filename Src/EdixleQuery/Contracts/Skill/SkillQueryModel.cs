namespace EdixleQuery.Contracts.Skill
{
    public class SkillQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public long PageId { get; set; }
        public bool IsRemoved { get; set; }
    }
}
