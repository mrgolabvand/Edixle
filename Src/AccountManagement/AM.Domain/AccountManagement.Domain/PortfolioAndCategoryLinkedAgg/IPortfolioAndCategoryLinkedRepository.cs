using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace AccountManagement.Domain.PortfolioAndCategoryLinkedAgg
{
    public interface IPortfolioAndCategoryLinkedRepository
    {
        ValueTask AddAsync(List<PortfolioAndCategoryLinked> command);
        ValueTask SaveChangesAsync();

        ValueTask DeleteAsync(long portfolioId);
        ValueTask DeleteAsync(long portfolioId, long categoryId);
    }
}
