namespace AccountManagement.Application.Contracts.JobHistory
{
    public class JobHistoryViewModel
    {
        public long Id { get; set; }
        public string JobTitle { get; set; }
        public string EmployerName { get; set; }
        public string Description { get; set; }
        public long PageId { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
    }
}