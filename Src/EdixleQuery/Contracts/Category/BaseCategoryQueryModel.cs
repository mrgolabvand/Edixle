using EdixleQuery.Contracts.PortfolioCategory;

namespace EdixleQuery.Contracts.Category
{
    public class BaseCategoryQueryModel
    {
        public long BaseCategoryId { get; set; }
        public string BaseCategoryName { get; set; }
        public string BaseCategorySlug { get; set; }
        public List<PortfolioCategoryQueryModel> PortfolioCategory { get; set; }
    }
}
