namespace AccountManagement.Application.Contracts.ProjectOffer
{
    public class ProjectOfferViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public long ProjectId { get; set; }
        public long EditorPageId { get; set; }
        public bool IsAccept { get; set; }
        public bool IsCancel { get; set; }
        public double DoublePrice { get; set; }
    }
}
