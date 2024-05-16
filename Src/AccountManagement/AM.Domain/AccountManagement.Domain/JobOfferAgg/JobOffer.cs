using _0_Framework.Domain;
using _0_Framework.Domain.Offer;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.EmployerPageAgg;
using AccountManagement.Domain.PersonalPageAgg;
using AccountManagement.Domain.ProjectAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Domain.JobOfferAgg
{
    public class JobOffer : OfferEntity
    {
        public long EditorPageId { get; private set; }
        public long EmployerPageId { get; private set; }
        public PersonalPage EditorPage { get; private set; }
        public EmployerPage EmployerPage { get; private set; }

        public JobOffer(string title, double price, string description, long editorPageId, long employerPageId)
            : base(title, price, description)
        {
            EditorPageId = editorPageId;
            EmployerPageId = employerPageId;
        }

        public void Edit(string title, double price, string description, long editorPageId, long employerPageId)
        {
            EditorPageId = editorPageId;
            EmployerPageId = employerPageId;
            base.Edit(title, price, description);
        }
    }
}
