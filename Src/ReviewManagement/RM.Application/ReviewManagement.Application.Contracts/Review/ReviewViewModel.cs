namespace ReviewManagement.Application.Contracts.Review
{
    public class ReviewViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public bool IsConfirmed { get; set; }
        public string CreationDate { get; set; }
        public long OwnerRecordId { get; set; }

    }
}
