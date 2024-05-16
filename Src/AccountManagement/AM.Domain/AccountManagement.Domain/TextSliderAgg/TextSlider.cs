using _0_Framework.Domain;

namespace AccountManagement.Domain.TextSliderAgg
{
    public class TextSlider : BaseEntity
    {
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Link { get; private set; }
        public bool IsRemoved { get; private set; }

        public TextSlider(string title, string description, string link)
        {
            Title = title;
            Description = description;
            Link = link;
            IsRemoved = false;
        }

        public void Edit(string title, string description, string link)
        {
            Title = title;
            Description = description;
            Link = link;
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
