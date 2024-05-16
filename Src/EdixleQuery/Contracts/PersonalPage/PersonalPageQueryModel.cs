using EdixleQuery.Contracts.JobHistory;
using EdixleQuery.Contracts.Portfolio;
using EdixleQuery.Contracts.Review;
using EdixleQuery.Contracts.Skill;

namespace EdixleQuery.Contracts.PersonalPage;

public class PersonalPageQueryModel
{
    public long Id { get; set; }
    public string FullName { get; set; }
    public string Slug { get; set; }
    public string About { get; set; }
    public string Picture { get; set; }
    public string PictureTitle { get; set; }
    public string PictureAlt { get; set; }
    public string MinPay { get; set; }
    public string MaxPay { get; set; }
    public int IntMinPay { get; set; }
    public int IntMaxPay { get; set; }
    public string PayDate { get; set; }
    public string Age { get; set; }
    public bool IsActive { get; set; }
    public bool IsBusy { get; set; }
    public bool IsConfirm { get; set; }
    public string CreationDate { get; set; }
    public long AccountId { get; set; }
    public List<PortfolioQueryModel> Portfolios = new();
    public List<SkillQueryModel> Skills = new();
    public List<ReviewQueryModel> Reviews = new();
    public List<JobHistoryQueryModel> JobHistories = new();
    public double EditVideoQualityTotalRate { get; set; } = 0;
    public double EditSoundQualityTotalRate { get; set; } = 0;
    public double UseProperVideoEffectsTotalRate { get; set; } = 0;
    public double UseProperSoundEffectsTotalRate { get; set; } = 0;
    public double UseProperMemesTotalRate { get; set; } = 0;
    public double UseProperFontAndColorsTotalRate { get; set; } = 0;
    public double ReviewsTotalRate { get; set; } = 0;
}