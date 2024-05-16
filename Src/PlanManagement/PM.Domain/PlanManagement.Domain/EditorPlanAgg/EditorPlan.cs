using _0_Framework.Domain;

namespace PlanManagement.Domain.EditorPlanAgg
{
    public class EditorPlan : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public double Price { get; private set; }
        public short ExpireDays { get; private set; }
        public double ChatUploadSizeLimit { get; private set; }
        public double PortfolioUploadSizeLimit { get; private set; }
        public short VipProjectOfferCount { get; private set; }
        public bool IsRemoved { get; private set; }

        public EditorPlan(string title, string description, double price, short expireDays, double chatUploadSizeLimit, double portfolioUploadSizeLimit, short vipProjectOfferCount)
        {
            Title = title;
            Description = description;
            Price = price;
            ExpireDays = expireDays;
            ChatUploadSizeLimit = chatUploadSizeLimit;
            PortfolioUploadSizeLimit = portfolioUploadSizeLimit;
            VipProjectOfferCount = vipProjectOfferCount;
            IsRemoved = false;
        }

        public void Edit(string title, string description, double price, short expireDays, double chatUploadSizeLimit, double portfolioUploadSizeLimit, short vipProjectOfferCount)
        {
            Title = title;
            Description = description;
            Price = price;
            ExpireDays = expireDays;
            ChatUploadSizeLimit = chatUploadSizeLimit;
            PortfolioUploadSizeLimit = portfolioUploadSizeLimit;
            VipProjectOfferCount = vipProjectOfferCount;
        }

        public void Remove() => IsRemoved = true;
        public void Restore() => IsRemoved = false;
    }
}
