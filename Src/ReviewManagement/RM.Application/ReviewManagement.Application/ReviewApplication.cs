using ReviewManagement.Application.Contracts.Review;
using ReviewManagement.Domain.ReviewAgg;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ReviewManagement.Application
{
    public class ReviewApplication : IReviewApplication
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewApplication(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }

        public async ValueTask CancelAsync(long id)
        {
            var review = await _reviewRepository.GetAsync(id);
            review.Cancel();
            await _reviewRepository.SaveChangesAsync();
        }

        public async ValueTask ConfirmAsync(long id)
        {
            var review = await _reviewRepository.GetAsync(id);
            review.Confirm();
            await _reviewRepository.SaveChangesAsync();
        }

        public async ValueTask CreateAsync(CreateReview command)
        {
            if (await _reviewRepository.ExistsAsync(v => v.Name == command.Name && v.OwnerRecordId == command.OwnerRecordId))
                return;

            var review = new Review(command.Name, command.Message, command.Strength, command.Weakness, command.Rate, command.EditVideoQuality, command.EditSoundQuality, command.UseProperVideoEffects, command.UseProperSoundEffects, command.UseProperMemes, command.UseProperFontAndColors, command.OwnerRecordId);

            await _reviewRepository.CreateAsync(review);
            await _reviewRepository.SaveChangesAsync();
        }

        public async ValueTask IsHarmfulAsync(long id, long portfolioId, string userName)
        {
            if (await _reviewRepository.ExistsAsync(v => v.OwnerRecordId == portfolioId && v.Name == userName))
                return;
            var review = await _reviewRepository.GetAsync(id);
            review.IncreaseHarmful();
            await _reviewRepository.SaveChangesAsync();
        }

        public async ValueTask IsHelpfulAsync(long id, long portfolioId, string userName)
        {
            if (await _reviewRepository.ExistsAsync(v => v.OwnerRecordId == portfolioId && v.Name == userName))
                return;
            var review = await _reviewRepository.GetAsync(id);
            review.IncreaseHelpful();
            await _reviewRepository.SaveChangesAsync();
        }

        public async ValueTask<List<ReviewViewModel>> SearchAsync(ReviewSearchModel searchModel) =>
            await _reviewRepository.SearchAsync(searchModel);
    }
}
