namespace PlanManagement.Application.Contracts.EditorPlan
{
    public class EditorPlanViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Price { get; set; }
        public double DoublePrice { get; set; }
        public short ExpireDays { get; set; }
        public double ChatUploadSizeLimit { get; set; }
        public double PortfolioUploadSizeLimit { get; set; }
        public short VipProjectOfferCount { get; set; }
        public bool IsRemoved { get; set; }
        public string CreationDate { get; set; }
    }
}
