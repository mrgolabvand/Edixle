namespace AccountManagement.Application.Contracts.JobOffer
{
    public class JobOfferViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public double DoublePrice { get; set; }
        public string Description { get; set; }
        public long EditorPageId { get; set; }
        public long EditorAccountId { get; set; }
        public long EmployerPageId { get; set; }
        public long EmployerAccountId { get; set; }
        public bool IsAccept { get; set; }
        public bool IsCancel { get; set; }
        public string CreationDate { get; set; }
        public string EditorPicture { get; set; }
        public string EmployerPicture { get; set; }
        public string EditorName { get; set; }
        public string EditorSlug { get; set; }
        public string EmployerName { get; set; }

    }
}
