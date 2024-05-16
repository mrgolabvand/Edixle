namespace AccountManagement.Application.Contracts.TextSlider
{
    public class TextSliderViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string CreationDate { get; set; }
        public bool IsRemoved { get; set; }
    }
}