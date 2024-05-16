using System.Collections.Generic;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Application.Contracts.PortfolioAndCategoryLinked;
using AccountManagement.Domain.PortfolioAndCategoryLinkedAgg;

namespace AccountManagement.Application
{
    public class PortfolioAndCategoryLinkedApplication : IPortfolioAndCategoryLinkedApplication
    {
        private readonly IPortfolioAndCategoryLinkedRepository _portfolioAndCategoryLinkedRepository;

        public PortfolioAndCategoryLinkedApplication(IPortfolioAndCategoryLinkedRepository portfolioAndCategoryLinkedRepository)
        {
            _portfolioAndCategoryLinkedRepository = portfolioAndCategoryLinkedRepository;
        }

        public async ValueTask AddAsync(List<AddPortfolioAndCategoryLink> command)
        {
            var portfolioAndCategoryLinkedList = new List<PortfolioAndCategoryLinked>();
            foreach (var commandItem in command)
            {
                var portfolioAndCategoryLinked = new PortfolioAndCategoryLinked(commandItem.PortfolioId, commandItem.CategoryId);
                portfolioAndCategoryLinkedList.Add(portfolioAndCategoryLinked);
            }

            await _portfolioAndCategoryLinkedRepository.AddAsync(portfolioAndCategoryLinkedList);
        }

        public async ValueTask DeleteAsync(long portfolioId) =>
            await _portfolioAndCategoryLinkedRepository.DeleteAsync(portfolioId);
        public async ValueTask DeleteAsync(long portfolioId, long categoryId) =>
            await _portfolioAndCategoryLinkedRepository.DeleteAsync(portfolioId, categoryId);
    }
}
