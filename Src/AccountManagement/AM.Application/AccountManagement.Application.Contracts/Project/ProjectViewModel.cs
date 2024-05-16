namespace AccountManagement.Application.Contracts.Project
{
    public class ProjectViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Budget { get; set; }
        public bool IsOpened { get; set; }
        public bool IsConfirm { get; set; }
        public string ExpireDate { get; set; }
        public string Tags { get; set; }
        public long EmployerPageId { get; set; }
        public string CreationDate { get; set; }
    }
}
