using System;
using System.Collections.Generic;
using _0_Framework.Domain;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.JobHistoryAgg;
using AccountManagement.Domain.JobOfferAgg;
using AccountManagement.Domain.PortfolioAgg;
using AccountManagement.Domain.ProjectOfferAgg;
using AccountManagement.Domain.SkillAgg;

namespace AccountManagement.Domain.PersonalPageAgg
{
    public class PersonalPage : BaseEntitySEOAndPicture
    {
        public string FullName { get; private set; }
        public string About { get; private set; }
        public string MinPay { get; private set; }
        public string MaxPay { get; private set; }
        public string PayDate { get; private set; }
        public string Age { get; private set; }
        public string Card { get; private set; }
        public bool IsActive { get; private set; }
        public bool IsBusy { get; private set; }
        public bool IsConfirm { get; private set; }
        public DateTime ExpireDate { get; private set; }
        public double ChatUploadSizeLimit { get; private set; }
        public double PortfolioUploadSizeLimit { get; private set; }
        public short VipProjectOfferCount { get; private set; }
        public Account Account { get; private set; }
        public long AccountId { get; private set; }
        public List<Portfolio> Portfolios { get; private set; }
        public List<Skill> Skills { get; private set; }
        public List<JobHistory> JobHistories { get; private set; }
        public List<JobOffer> JobOffers { get; private set; }
        public List<ProjectOffer> ProjectOffers { get; private set; }

        public PersonalPage(string fullName, string slug, string keywords, string metaDescription, string picture, string pictureAlt, string pictureTitle,
            string about, string age, long accountId, string minPay, string maxPay, string payDate, string card) : base(slug, keywords, metaDescription, picture, pictureAlt, pictureTitle)
        {
            FullName = fullName;
            About = about;
            Age = age;
            MinPay = minPay;
            MaxPay = maxPay;
            PayDate = payDate;
            AccountId = accountId;
            Skills = new List<Skill>();
            Portfolios = new List<Portfolio>();
            ProjectOffers = new List<ProjectOffer>();
            JobOffers = new List<JobOffer>();
            JobHistories = new List<JobHistory>();
            IsActive = true;
            IsConfirm = true;
            IsBusy = false;
            Card = card;
        }

        public void Edit(string fullName, string slug, string keywords, string metaDescription, string picture, string pictureAlt, string pictureTitle,
            string about, string age, string minPay, string maxPay, string payDate, string card)
        {
            FullName = fullName;
            About = about;
            MinPay = minPay;
            MaxPay = maxPay;
            PayDate = payDate;
            Age = age;
            Card = card;
            base.Edit(slug, keywords, metaDescription, picture, pictureAlt, pictureTitle);
        }
        public void Activate()
        {
            IsActive = true;
        }

        public void Disable()
        {
            IsActive = false;
        }

        public void Confirm()
        {
            IsConfirm = true;
        }

        public void Cancel()
        {
            IsConfirm = false;
        }

        public void Busy()
        {
            IsBusy = true;
        }

        public void NotBusy()
        {
            IsBusy = false;
        }

        public void SetPlan(DateTime expireDate, double chatUploadSizeLimit, double portfolioUploadSizeLimit, short vipProjectOfferCount)
        {
            ExpireDate = expireDate;
            ChatUploadSizeLimit = chatUploadSizeLimit;
            PortfolioUploadSizeLimit = portfolioUploadSizeLimit;
            VipProjectOfferCount = vipProjectOfferCount;
        }

        public void UseVipOffer()
        {
            var result = VipProjectOfferCount - 1;
            VipProjectOfferCount = (short)result;
        }
    }
}
