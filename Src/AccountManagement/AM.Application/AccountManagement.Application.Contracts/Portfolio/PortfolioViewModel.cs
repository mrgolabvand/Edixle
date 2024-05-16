namespace AccountManagement.Application.Contracts.Portfolio
{
    public class PortfolioViewModel
    {
        public long Id { get; set; }
        public long PageId { get; set; }
        public long AccountId { get; set; }
        public long DownloadCount { get; set; }
        public string PortfolioName { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public string File { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Tags { get; set; }
        public string Slug { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }
        public bool IsRemoved { get; set; }
        public bool IsConfirm { get; set; }
        public string CreationDate { get; set; }
    }
}