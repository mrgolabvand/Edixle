namespace EdixleQuery.Contracts.Plan
{
    public class EditorPlanQueryModel
    {
        public long Id { get; set; }
        public DateTime ExpireDate { get; set; }
        public int RemainingDays { get; set; }
        public double ChatUploadSizeLimit { get; set; }
        public double PortfolioUploadSizeLimit { get; set; }
        public short VipProjectOfferCount { get; set; }
        public bool IsPlanActive { get; set; }
    }
}
