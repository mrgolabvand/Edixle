namespace EdixleQuery.Contracts.ProjectOffer
{
    public class ProjectOfferQueryModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        public string Description { get; set; }
        public string CreationDate { get; set; }
        public long ProjectId { get; set; }
        public long EditorPageId { get; set; }
        public long AccountId { get; set; }
        public string EditorName { get; set; }
        public string EditorPicture { get; set; }
        public string EditorPageSlug { get; set; }
        public bool IsAccept { get; set; }
        public bool IsCancel { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectDescription { get; set; }
        public string ProjectBudget { get; set; }
        public string ProjectStatus { get; set; }
        public bool ProjectIsOpened { get; set; }
        public string ProjectOfferStatus { get; set; }
        public string ProjectEmployerPicture { get; set; }
        public string ProjectEmployerName { get; set; }
    }
}
