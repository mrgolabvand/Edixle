using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace AccountManagement.Application.Contracts.PortfolioAndCategoryLinked
{
    public interface IPortfolioAndCategoryLinkedApplication
    {
        ValueTask AddAsync(List<AddPortfolioAndCategoryLink> command);
        ValueTask DeleteAsync(long portfolioId);
        ValueTask DeleteAsync(long portfolioId, long categoryId);
    }
}
