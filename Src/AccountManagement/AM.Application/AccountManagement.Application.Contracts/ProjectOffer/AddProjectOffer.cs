namespace AccountManagement.Application.Contracts.ProjectOffer
{
    public class AddProjectOffer
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public long ProjectId { get; set; }
        public long EditorPageId { get; set; }
    }
}
