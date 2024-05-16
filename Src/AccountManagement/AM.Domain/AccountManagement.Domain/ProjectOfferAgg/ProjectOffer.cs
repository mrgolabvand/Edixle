using _0_Framework.Domain;
using _0_Framework.Domain.Offer;
using AccountManagement.Domain.EmployerPageAgg;
using AccountManagement.Domain.PersonalPageAgg;
using AccountManagement.Domain.ProjectAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Domain.ProjectOfferAgg
{
    public class ProjectOffer : OfferEntity
    {
        public long ProjectId { get; private set; }
        public long EditorPageId { get; private set; }
        public Project Project { get; private set; }
        public PersonalPage PersonalPage { get; private set; }

        public ProjectOffer(string title, double price, string description, long projectId, long editorPageId)
            : base(title, price, description)
        {
            ProjectId = projectId;
            EditorPageId = editorPageId;
        }

        public void Edit(string title, double price, string description, long projectId, long editorPageId)
        {
            ProjectId = projectId;
            EditorPageId = editorPageId;
            base.Edit(title, price, description);
        }
    }
}
