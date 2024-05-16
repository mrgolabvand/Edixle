using _0_Framework.Domain;
using AccountManagement.Domain.EmployerPageAgg;
using AccountManagement.Domain.ProjectOfferAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountManagement.Domain.ProjectAgg
{
    public class Project : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Budget { get; private set; }
        public bool IsOpened { get; private set; }
        public bool IsConfirm { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public string Tags { get; private set; }
        public long EmployerPageId { get; private set; }
        public EmployerPage EmployerPage { get; private set; }
        public List<ProjectOffer> ProjectOffers { get; private set; }

        public Project(string title, string budget, DateTime expireDate, string tags, long employerPageId, string description)
        {
            Title = title;
            Budget = budget;
            ExpireDate = expireDate;
            Tags = tags;
            EmployerPageId = employerPageId;
            IsConfirm = true;
            IsOpened = true;
            Description = description;
        }
        public void Edit(string title, string budget, string tags, long employerPageId, string description)
        {
            Title = title;
            Budget = budget;
            Tags = tags;
            EmployerPageId = employerPageId;
            Description = description;
        }

        public void Confirm()
        {
            IsConfirm = true;
        }
        public void Cancel()
        {
            IsConfirm = false;
        }

        public void Open()
        {
            IsOpened = true;
        }

        public void Close()
        {
            IsOpened = false;
        }
    }
}
