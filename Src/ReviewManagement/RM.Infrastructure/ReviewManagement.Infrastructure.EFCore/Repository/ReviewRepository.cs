using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ReviewManagement.Application.Contracts.Review;
using ReviewManagement.Domain.ReviewAgg;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReviewManagement.Infrastructure.EFCore.Repository
{
    public class ReviewRepository : BaseRepository<long, Review>, IReviewRepository
    {
        private readonly ReviewContext _reviewContext;

        public ReviewRepository(ReviewContext reviewContext) : base(reviewContext)
        {
            _reviewContext = reviewContext;
        }

        public async ValueTask<List<ReviewViewModel>> SearchAsync(ReviewSearchModel searchModel)
        {
            var query = _reviewContext.Reviews.Select(v => new ReviewViewModel
            {
                Id = v.Id,
                Name = v.Name,
                IsConfirmed = v.IsConfirmed,
                Message = v.Message,
                OwnerRecordId = v.OwnerRecordId,
                CreationDate = v.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
                query = query.Where(v => v.Name.Contains(searchModel.Name));

            return await query.AsNoTracking().OrderByDescending(v => v.Id).ToListAsync();
        }
    }
}
