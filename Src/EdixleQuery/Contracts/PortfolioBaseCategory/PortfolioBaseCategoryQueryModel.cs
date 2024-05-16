using EdixleQuery.Contracts.PortfolioCategory;

namespace EdixleQuery.Contracts.PortfolioBaseCategory
{
    public class PortfolioBaseCategoryQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string CreationDate { get; set; }
        public List<PortfolioCategoryQueryModel> PortfolioCategories { get; set; }
        
    }
}
