namespace _0_Framework.Domain
{
    public abstract class BaseEntitySEO : BaseEntity
    {
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }

        public BaseEntitySEO(string slug, string keywords, string metaDescription)
        {
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
        }

        public void Edit(string slug, string keywords, string metaDescription)
        {
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
        }
    }
}
