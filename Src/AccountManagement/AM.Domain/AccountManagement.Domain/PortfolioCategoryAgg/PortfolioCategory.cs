using System.Collections.Generic;
using _0_Framework.Domain;
using _0_Framework.Domain.PortfolioCategory;
using AccountManagement.Domain.PersonalPageAgg;
using AccountManagement.Domain.PortfolioAndCategoryLinkedAgg;
using AccountManagement.Domain.PortfolioBaseCategoryAgg;

namespace AccountManagement.Domain.PortfolioCategoryAgg
{
    public class PortfolioCategory : PortfolioCategoryEntity
    {
        public long PortfolioBaseCategoryId { get; private set; }
        public PortfolioBaseCategory PortfolioBaseCategory { get; private set; }
        public List<PortfolioAndCategoryLinked> Portfolios { get; private set; }

        public PortfolioCategory(string name, long portfolioBaseCategoryId, string slug, string keywords, string metaDescription,
            string picture, string pictureAlt, string pictureTitle) : base(name, slug, keywords, metaDescription, picture, pictureAlt, pictureTitle)
        {
            PortfolioBaseCategoryId = portfolioBaseCategoryId;
            Portfolios = new List<PortfolioAndCategoryLinked>();
        }

        public void Edit(string name, long portfolioBaseCategoryId, string slug, string keywords, string metaDescription,
            string picture, string pictureAlt, string pictureTitle)
        {
            PortfolioBaseCategoryId = portfolioBaseCategoryId;

            base.Edit(name, slug, keywords, metaDescription, picture, pictureAlt, pictureTitle);
        }
    }
}
