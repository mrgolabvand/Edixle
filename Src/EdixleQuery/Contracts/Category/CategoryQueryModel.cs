using EdixleQuery.Contracts.EmployerPage;
using EdixleQuery.Contracts.PersonalPage;
using EdixleQuery.Contracts.PortfolioCategory;

namespace EdixleQuery.Contracts.Category
{
    public class CategoryQueryModel
    {
        public List<BaseCategoryQueryModel> BaseCategories { get; set; }
        public List<PortfolioCategoryQueryModel> PortfolioCategory { get; set; }
        public bool IsAuth { get; set; }
        public bool IsEditor { get; set; }
        public bool IsEmployer { get; set; }
        public long AccountId { get; set; }
        public PersonalPageQueryModel? PersonalPage = new ();
        public EmployerPageQueryModel? EmployerPage = new ();
        
    }
}
