namespace EdixleQuery.Contracts.PortfolioCategory
{
    public class PortfolioCategoryQueryModel
    {
        public long Id { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Name { get; set; }
        public bool IsRemoved { get; set; }
        public string CreationDate { get; set; }
        public string BaseCategory { get; set; }
        public long BaseCategoryId { get; set; }
        public long PortfolioId { get; set; }
    }
}
