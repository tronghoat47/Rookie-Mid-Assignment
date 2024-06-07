using BaseProject.Application.Models.Responses;

namespace BaseProject.Application.Services
{
    public interface IRatingService
    {
        //Task<bool> CreateRating(RatingRequest ratingRequest);

        //Task<bool> UpdateRating(string userId, long bookId, RatingRequest ratingRequest);

        //Task<bool> DeleteRating(string userId, long bookId);

        //Task<IEnumerable<RatingResponse>> GetRatings();

        //Task<IEnumerable<RatingResponse>> GetRatingsByUserId(string userId);

        Task<IEnumerable<RatingResponse>> GetRatingsByBookId(long bookId);

        //Task<RatingResponse> GetRatingByUserIdAndBookId(string userId, long bookId);

        //Task<RatingResponse> GetNewestRatingByUserId(string userId);
    }
}