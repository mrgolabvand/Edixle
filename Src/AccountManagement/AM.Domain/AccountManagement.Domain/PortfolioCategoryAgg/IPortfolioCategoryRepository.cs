using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.PortfolioCategory;

namespace AccountManagement.Domain.PortfolioCategoryAgg
{
    public interface IPortfolioCategoryRepository : IBaseRepository<long, PortfolioCategory>
    {
        ValueTask<EditPortfolioCategory> GetDetailsAsync(long id);
        ValueTask<List<PortfolioCategoryViewModel>> SearchAsync(PortfolioCategorySearchModel searchModel);
        ValueTask<string> GetCategorySlugAsync(long id);
        ValueTask<List<PortfolioCategoryViewModel>> GetListAsync();
    }
}
