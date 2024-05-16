using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Domain.PortfolioAndCategoryLinkedAgg;
using Microsoft.EntityFrameworkCore;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class PortfolioAndCategoryLinkedRepository : IPortfolioAndCategoryLinkedRepository
    {
        private readonly AccountContext _context;

        public PortfolioAndCategoryLinkedRepository(AccountContext context)
        {
            _context = context;
        }

        public async ValueTask AddAsync(List<PortfolioAndCategoryLinked> command)
        {
            await _context.PortfolioAndCategoryLinkeds.AddRangeAsync(command);
            await SaveChangesAsync();
        }

        public async ValueTask SaveChangesAsync() => await _context.SaveChangesAsync();

        public async ValueTask DeleteAsync(long portfolioId)
        {
            var links = await _context.PortfolioAndCategoryLinkeds.Where(v => v.PortfolioId == portfolioId).ToListAsync();
            links.ForEach(v => _context.PortfolioAndCategoryLinkeds.Remove(v));
            await SaveChangesAsync();
        }

        public async ValueTask DeleteAsync(long portfolioId, long categoryId)
        {
            var links = await _context.PortfolioAndCategoryLinkeds
                .Where(v => v.PortfolioId == portfolioId && v.PortfolioCategoryId == categoryId).ToListAsync();
            links.ForEach(v => _context.PortfolioAndCategoryLinkeds.Remove(v));
            await SaveChangesAsync();
        }
    }
}
