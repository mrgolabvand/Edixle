using _0_Framework.Domain;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.JobOfferAgg;
using AccountManagement.Domain.ProjectAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Domain.EmployerPageAgg
{
    public class EmployerPage : BaseEntity
    {
        public string FullName { get; private set; }
        public string Card { get; private set; }
        public string Picture { get; private set; }
        public long AccountId { get; private set; }
        public Account Account { get; private set; }
        public List<JobOffer> JobOffers { get; set; }
        public List<Project> Projects { get; set; }

        public EmployerPage(string fullName, string picture, long accountId, string card)
        {
            FullName = fullName;
            Picture = picture;
            AccountId = accountId;
            Card = card;
        }

        public void Edit(string fullName, string picture, long accountId, string card)
        {
            FullName = fullName;
            if (!string.IsNullOrWhiteSpace(picture))
            {
                Picture = picture;
            }
            AccountId = accountId;
            Card = card;
        }
    }
}
