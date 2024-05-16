namespace AccountManagement.Application.Contracts.Feature
{
    public class FeatureViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Picture { get; set; }
        public bool IsRemoved { get; set; }
    }
}