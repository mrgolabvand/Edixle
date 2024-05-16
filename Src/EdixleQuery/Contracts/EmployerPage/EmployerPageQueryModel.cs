using AccountManagement.Application.Contracts.JobOffer;
using AccountManagement.Application.Contracts.Project;

namespace EdixleQuery.Contracts.EmployerPage
{
    public class EmployerPageQueryModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Picture { get; set; }
        public long AccountId { get; set; }
        public List<JobOfferViewModel> JobOffers { get; set; }
        public List<ProjectViewModel> Projects { get; set; }
    }
}
