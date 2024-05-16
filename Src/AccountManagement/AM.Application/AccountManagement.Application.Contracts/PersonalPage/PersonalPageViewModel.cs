using System;

namespace AccountManagement.Application.Contracts.PersonalPage
{
    public class PersonalPageViewModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Slug { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Age { get; set; }
        public string MinPay { get; set; }
        public string MaxPay { get; set; }
        public string PayDate { get; set; }
        public string CreationDate { get; set; }
        public long AccountId { get; set; }
        public string About { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsActive { get; set; }
        public bool HasPlan { get; set; }
        public DateTime PlanExpireDate { get; set; }
        public int PlanExpireDays { get; set; }
        public double PlanChatUploadSizeLimit { get; set; }
        public double PlanPortfolioUploadSizeLimit { get; set; }
        public double PlanChatUploadSizeLimitGB { get; set; }
        public double PlanPortfolioUploadSizeLimitMB { get; set; }
        public short PlanVipProjectOfferCount { get; set; }
    }
}