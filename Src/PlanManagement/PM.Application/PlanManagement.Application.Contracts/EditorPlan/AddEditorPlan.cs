namespace PlanManagement.Application.Contracts.EditorPlan
{
    public class AddEditorPlan
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public short ExpireDays { get; set; }
        public double ChatUploadSizeLimit { get; set; }
        public double PortfolioUploadSizeLimit { get; set; }
        public short VipProjectOfferCount { get; set; }
    }
}