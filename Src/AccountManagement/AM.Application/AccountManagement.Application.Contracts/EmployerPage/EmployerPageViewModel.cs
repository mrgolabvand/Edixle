namespace AccountManagement.Application.Contracts.EmployerPage
{
    public class EmployerPageViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; }
        public string Picture { get; set; }
        public long AccountId { get; set; }
        public string Account { get; set; }
        public string Card { get; set; }
        public string CreationDate { get; set; }
    }
}
