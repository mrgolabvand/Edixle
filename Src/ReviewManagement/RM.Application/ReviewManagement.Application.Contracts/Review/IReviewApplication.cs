using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewManagement.Application.Contracts.Review
{
    public interface IReviewApplication
    {
        ValueTask CreateAsync(CreateReview command);
        ValueTask<List<ReviewViewModel>> SearchAsync(ReviewSearchModel searchModel);
        ValueTask ConfirmAsync(long id);
        ValueTask CancelAsync(long id);
        ValueTask IsHelpfulAsync(long id, long portfolioId, string userName);
        ValueTask IsHarmfulAsync(long id, long portfolioId, string userName);
    }
}
