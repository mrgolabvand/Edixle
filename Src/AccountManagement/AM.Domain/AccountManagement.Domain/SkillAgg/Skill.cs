using _0_Framework.Domain;
using AccountManagement.Domain.PersonalPageAgg;

namespace AccountManagement.Domain.SkillAgg
{
    public class Skill : BaseEntity
    {
        public string Name { get; private set; }
        public string Value { get; private set; }
        public PersonalPage Page { get; private set; }
        public long PageId { get; private set; }
        public bool IsRemoved { get; private set; }

        public Skill(string name, string value, long pageId)
        {
            Name = name;
            Value = value;
            PageId = pageId;
            IsRemoved = false;
        }

        public void Edit(string name, string value)
        {
            Name = name;
            Value = value;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
