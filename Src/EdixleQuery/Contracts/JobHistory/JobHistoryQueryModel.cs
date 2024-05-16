namespace EdixleQuery.Contracts.JobHistory
{
    public class JobHistoryQueryModel
    {
        public long Id { get; set; }
        public string JobTitle { get; set; }
        public string EmployerName { get; set; }
        public string Description { get; set; }
        public long PageId { get; set; }
        public bool IsRemoved { get; set; }
    }
}
