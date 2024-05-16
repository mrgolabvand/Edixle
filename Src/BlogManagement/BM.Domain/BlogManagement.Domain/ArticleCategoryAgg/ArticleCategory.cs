using _0_Framework.Domain;
using BlogManagement.Domain.ArticleAgg;
using System;
using System.Collections.Generic;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public class ArticleCategory : BaseEntity
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public int ShowOrder { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Slug { get; private set; }
        public string Keyeords { get; private set; }
        public string MetaDescription { get; private set; }
        public string CanonicalAddress { get; private set; }
        public List<Article> Articles { get; private set; }

        public ArticleCategory(string name, string description, int showOrder, string picture,string pictureAlt, string pictureTitle, string slug, string keyeords, string metaDescription, string canonicalAddress)
        {
            Name = name;
            Description = description;
            ShowOrder = showOrder;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keyeords = keyeords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
            Articles = new List<Article>();
        }


        public void Edit(string name, string description, int showOrder, string picture, string pictureAlt, string pictureTitle, string slug, string keyeords, string metaDescription, string canonicalAddress)
        {
            Name = name;
            Description = description;
            ShowOrder = showOrder;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Slug = slug;
            Keyeords = keyeords;
            MetaDescription = metaDescription;
            CanonicalAddress = canonicalAddress;
        }


    }
}
