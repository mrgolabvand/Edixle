using _0_Framework.Domain;
using _0_Framework.Domain.PortfolioCategory;
using AccountManagement.Domain.PortfolioCategoryAgg;
using System.Collections.Generic;

namespace AccountManagement.Domain.PortfolioBaseCategoryAgg
{
    public class PortfolioBaseCategory : PortfolioCategoryEntity
    {
        public List<PortfolioCategory> PortfolioCategories { get; private set; }

        public PortfolioBaseCategory(string name, string slug, string keywords, string metaDescription, string picture,
            string pictureAlt, string pictureTitle) 
            : base(name, slug, keywords, metaDescription, picture, pictureAlt, pictureTitle)
        {
            PortfolioCategories = new List<PortfolioCategory>();
        }
    }
}
