using System.Collections.Generic;
using _0_Framework.Domain;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.PersonalPageAgg;
using AccountManagement.Domain.PortfolioAndCategoryLinkedAgg;
using AccountManagement.Domain.PortfolioCategoryAgg;
using AccountManagement.Domain.PortfolioDownloadAgg;

namespace AccountManagement.Domain.PortfolioAgg
{
    public class Portfolio : BaseEntity
    {
        public string Name { get; private set; }
        public string ShortDescription { get; private set; }
        public string Description { get; private set; }
        public string Video { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public string Tags { get; private set; }
        public string Slug { get; private set; }
        public string Keywords { get; private set; }
        public string MetaDescription { get; private set; }
        public bool IsRemoved { get; private set; }
        public bool IsConfirm { get; private set; }
        public List<PortfolioAndCategoryLinked> Categories { get; private set; }
        public PersonalPage Page { get; private set; }
        public List<PortfolioDownload> PortfolioDownloads { get; private set; }    
        public long PageId { get; private set; }

        public Portfolio(string name, string shortDescription, string description, string video, string picture, string pictureAlt,
            string pictureTitle, string tags, string slug, string keywords, string metaDescription, long pageId)
        {
            Name = name;
            ShortDescription = shortDescription;
            Description = description;
            Video = video;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Tags = tags;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
            PageId = pageId;
            IsConfirm = true;
            IsRemoved = false;
            Categories = new List<PortfolioAndCategoryLinked>();
            PortfolioDownloads = new List<PortfolioDownload>();
        }
        public void Edit(string name, string shortDescription, string description, string video, string picture, string pictureAlt,
            string pictureTitle, string tags, string slug, string keywords, string metaDescription)
        {
            Name = name;
            ShortDescription = shortDescription;
            Description = description;
            if (!string.IsNullOrWhiteSpace(video))
                Video = video;
            if (!string.IsNullOrWhiteSpace(picture))
                Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            Tags = tags;
            Slug = slug;
            Keywords = keywords;
            MetaDescription = metaDescription;
        }

     

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }

        public void Confirm()
        {
            IsConfirm = true;
        }

        public void Cancel()
        {
            IsConfirm = false;
        }
    }
}
