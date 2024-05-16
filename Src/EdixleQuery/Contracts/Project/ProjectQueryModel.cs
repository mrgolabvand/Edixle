using EdixleQuery.Contracts.ProjectOffer;

namespace EdixleQuery.Contracts.Project;

public class ProjectQueryModel
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Budget { get; set; }
    public int IntBudget { get; set; }
    public bool IsOpened { get; set; }
    public bool IsConfirm { get; set; }
    public string ExpireDate { get; set; }
    public string Tags { get; set; }
    public long EmployerPageId { get; set; }
    public string EmployerName { get; set; }
    public string EmployerPicture { get; set; }
    public string CreationDate { get; set; }
    public List<ProjectOfferQueryModel> ProjectOffers { get; set; }
}