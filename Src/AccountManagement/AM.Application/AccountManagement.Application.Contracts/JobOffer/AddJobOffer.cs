namespace AccountManagement.Application.Contracts.JobOffer
{
    public class AddJobOffer
    {
        public string Title { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public long EditorPageId { get; set; }
        public long EmployerPageId { get; set; }
    }
}
