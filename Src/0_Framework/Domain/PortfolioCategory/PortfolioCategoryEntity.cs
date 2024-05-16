namespace _0_Framework.Domain.PortfolioCategory
{
    public abstract class PortfolioCategoryEntity : BaseEntitySEOAndPicture
    {
        public string Name { get; set; }
        public bool IsRemoved { get; set; }

        public PortfolioCategoryEntity(string name, string slug, string keywords, string metaDescription,
            string picture, string pictureAlt, string pictureTitle) : base(slug, keywords, metaDescription, picture, pictureAlt, pictureTitle)
        {
            Name = name;
            IsRemoved = false;
        }

        public void Edit(string name, string slug, string keywords, string metaDescription,
            string picture, string pictureAlt, string pictureTitle)
        {
            Name = name;
            Slug = slug;
            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Keywords = keywords;
            MetaDescription = metaDescription;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = true;
        }
    }
}
