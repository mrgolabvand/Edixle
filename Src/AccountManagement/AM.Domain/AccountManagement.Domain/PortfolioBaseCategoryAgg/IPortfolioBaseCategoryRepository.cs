using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Domain;
using AccountManagement.Application.Contracts.PortfolioBaseCategory;
using AccountManagement.Application.Contracts.PortfolioCategory;

namespace AccountManagement.Domain.PortfolioBaseCategoryAgg
{
    public interface IPortfolioBaseCategoryRepository : IBaseRepository<long, PortfolioBaseCategory>
    {
        ValueTask<EditPortfolioBaseCategory> GetDetailsAsync(long id);
        ValueTask<List<PortfolioBaseCategoryViewModel>> SearchAsync(PortfolioBaseCategorySearchModel searchModel);
        ValueTask<string> GetCategorySlugAsync(long id);
        ValueTask<List<PortfolioBaseCategoryViewModel>> GetListAsync();
    }
}
