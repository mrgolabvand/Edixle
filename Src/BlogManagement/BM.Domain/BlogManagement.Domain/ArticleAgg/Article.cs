using _0_Framework.Domain;
using BlogManagement.Domain.ArticleCategoryAgg;
using System;

namespace BlogManagement.Domain.ArticleAgg
{
    public class Article : BaseEntity
    {

        public string Title { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureTitle { get; private set; }
        public string PictureAlt { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
        public string CanonicalAddress { get; private set; }    
        public DateTime PublishDate { get; private set; }
        public long CategoryId { get; private set; }
        public ArticleCategory Category { get; private set; }

        public Article(string title, string shortDescription, string description, string picture, string pictureTitle, string pictureAlt, string keywords, string metaDescription, string slug, string canonicalAddress, DateTime publishDate, long categoryId)
        {
            Title = title;
            ShortDescription = shortDescription;
            Description = description;
            Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            CanonicalAddress = canonicalAddress;
            PublishDate = publishDate;
            CategoryId = categoryId;
        }


        public void Edit(string title, string shortDescription, string description, string picture, string pictureTitle, string pictureAlt, string keywords, string metaDescription, string slug, string canonicalAddress, DateTime publishDate, long categoryId)
        {
            Title = title;
            ShortDescription = shortDescription;
            Description = description;
            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
            Keywords = keywords;
            MetaDescription = metaDescription;
            Slug = slug;
            CanonicalAddress = canonicalAddress;
            PublishDate = publishDate;
            CategoryId = categoryId;
        }
    }
}
