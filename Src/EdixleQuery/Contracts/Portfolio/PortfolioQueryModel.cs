using AccountManagement.Application.Contracts.PortfolioAndCategoryLinked;
using EdixleQuery.Contracts.PortfolioCategory;
using EdixleQuery.Contracts.Review;

namespace EdixleQuery.Contracts.Portfolio
{
    public class PortfolioQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string AccountName { get; set; }
        public string AccountSlug { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string Video { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string PagePicture { get; set; }
        public string PagePictureAlt { get; set; }
        public string PagePictureTitle { get; set; }
        public string Tags { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public bool IsRemoved { get; set; }

        public List<PortfolioAndCategoryLinkViewModel> PortfoliosAndCategories = new();
        public List<PortfolioCategoryQueryModel> PortfolioCategories = new();
        public long PageId { get; set; }
        public string CreationDate { get; set; }
        public List<string> TagList { get; set; }
        public List<ReviewQueryModel> Reviews = new();

        public double EditVideoQualityTotalRate { get; set; } = 0;
        public double EditSoundQualityTotalRate { get; set; } = 0;
        public double UseProperVideoEffectsTotalRate { get; set; } = 0;
        public double UseProperSoundEffectsTotalRate { get; set; } = 0;
        public double UseProperMemesTotalRate { get; set; } = 0;
        public double UseProperFontAndColorsTotalRate { get; set; } = 0;
        public double ReviewsTotalRate { get; set; } = 0;
    }
}
