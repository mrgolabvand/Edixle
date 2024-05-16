using _0_Framework.Domain;
using ReviewManagement.Application.Contracts.Review;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewManagement.Domain.ReviewAgg
{
    public interface IReviewRepository : IBaseRepository<long, Review>
    {
        ValueTask<List<ReviewViewModel>> SearchAsync(ReviewSearchModel model);
    }
}
