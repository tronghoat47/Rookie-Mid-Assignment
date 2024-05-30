using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using BaseProject.Domain.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Services
{
    public interface IRatingService
    {
        Task<bool> CreateRating(RatingRequest ratingRequest);

        Task<bool> UpdateRating(string userId, long bookId, RatingRequest ratingRequest);

        Task<bool> DeleteRating(string userId, long bookId);

        Task<IEnumerable<RatingResponse>> GetRatings();

        Task<IEnumerable<RatingResponse>> GetRatingsByUserId(string userId);

        Task<IEnumerable<RatingResponse>> GetRatingsByBookId(long bookId);

        Task<RatingResponse> GetRatingByUserIdAndBookId(string userId, long bookId);
    }
}