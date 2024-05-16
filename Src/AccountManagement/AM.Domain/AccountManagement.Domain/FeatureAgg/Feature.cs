using _0_Framework.Domain;

namespace AccountManagement.Domain.FeatureAgg
{
    public class Feature : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }
        public bool IsRemoved { get; private set; }

        public Feature(string title, string description, string picture, string pictureAlt, string pictureTitle)
        {
            Title = title;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
            IsRemoved = false;
        }

        public void Edit(string title, string description, string picture, string pictureAlt, string pictureTitle)
        {
            Title = title;
            Description = description;
            Picture = picture;
            PictureAlt = pictureAlt;
            PictureTitle = pictureTitle;
        }

        public void Remove()
        {
            IsRemoved = true;
        }

        public void Restore()
        {
            IsRemoved = false;
        }
    }
}
