using _0_Framework.Domain;
using AccountManagement.Domain.PersonalPageAgg;

namespace AccountManagement.Domain.JobHistoryAgg
{
    public class JobHistory : BaseEntity
    {
        public string JobTitle { get; private set; }
        public string EmployerName { get; private set; }
        public string Description { get; private set; }
        public PersonalPage Page { get; private set; }
        public long PageId { get; private set; }
        public bool IsRemoved { get; private set; }

        public JobHistory(string jobTitle, string employerName, string description, long pageId)
        {
            JobTitle = jobTitle;
            EmployerName = employerName;
            Description = description;
            PageId = pageId;
            IsRemoved = false;
        }

        public void Edit(string jobTitle, string employerName, string description)
        {
            JobTitle = jobTitle;
            EmployerName = employerName;
            Description = description;
        }

        public void Remove() => IsRemoved = true;

        public void Restore() => IsRemoved = false;
    }
}
