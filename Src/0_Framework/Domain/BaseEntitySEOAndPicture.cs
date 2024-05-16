namespace _0_Framework.Domain
{
    public abstract class BaseEntitySEOAndPicture : BaseEntitySEO
    {
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }

        public BaseEntitySEOAndPicture(string slug, string keywords, string metaDescription,
            string picture, string pictureAlt, string pictureTitle) : base(slug, keywords, metaDescription)
        {
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
        }

        public void Edit(string slug, string keywords, string metaDescription,
            string picture, string pictureAlt, string pictureTitle)
        {
            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            base.Edit(slug, keywords, metaDescription);
        }
    }
}
