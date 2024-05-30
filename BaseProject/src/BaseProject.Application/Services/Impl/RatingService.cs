using AutoMapper;
using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Services.Impl
{
    public class RatingService : IRatingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RatingService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /*Task<bool> CreateRating(RatingRequest ratingRequest);
        Task<bool> UpdateRating(string userId, long bookId, RatingRequest ratingRequest);
        Task<bool> DeleteRating(string userId, long bookId);
        Task<IEnumerable<RatingResponse>> GetRatings();
        Task<IEnumerable<RatingResponse>> GetRatingsByUserId(string userId);
        Task<IEnumerable<RatingResponse>> GetRatingsByBookId(long bookId);
        Task<RatingResponse> GetRatingByUserIdAndBookId(string userId, long bookId);*/

        public async Task<bool> CreateRating(RatingRequest ratingRequest)
        {
            var rateExist = await _unitOfWork.RatingRepository.GetAsync(r => !r.IsDeleted && r.UserId == ratingRequest.UserId && r.BookId == ratingRequest.BookId);
            if (rateExist != null)
            {
                return false;
            }
            var rating = new Rating
            {
                Rate = ratingRequest.Rate,
                BookId = ratingRequest.BookId,
                UserId = ratingRequest.UserId,
                Description = ratingRequest.Description
            };

            await _unitOfWork.RatingRepository.AddAsync(rating);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> DeleteRating(string userId, long bookId)
        {
            var rating = await _unitOfWork.RatingRepository.GetAsync(r => !r.IsDeleted && r.UserId == userId && r.BookId == bookId);
            if (rating == null)
            {
                return false;
            }
            _unitOfWork.RatingRepository.SoftDelete(rating);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<IEnumerable<RatingResponse>> GetRatings()
        {
            var ratings = await _unitOfWork.RatingRepository.GetAllAsync(r => !r.IsDeleted, r => r.User, r => r.Book);
            return _mapper.Map<IEnumerable<RatingResponse>>(ratings);
        }

        public async Task<IEnumerable<RatingResponse>> GetRatingsByBookId(long bookId)
        {
            var ratings = await _unitOfWork.RatingRepository.GetAllAsync(r => !r.IsDeleted && r.BookId == bookId, r => r.User, r => r.Book);
            return _mapper.Map<IEnumerable<RatingResponse>>(ratings);
        }

        public async Task<IEnumerable<RatingResponse>> GetRatingsByUserId(string userId)
        {
            var ratings = await _unitOfWork.RatingRepository.GetAllAsync(r => !r.IsDeleted && r.UserId == userId, r => r.User, r => r.Book);
            return _mapper.Map<IEnumerable<RatingResponse>>(ratings);
        }

        public async Task<bool> UpdateRating(string userId, long bookId, RatingRequest ratingRequest)
        {
            var existingRating = await _unitOfWork.RatingRepository.GetAsync(r => !r.IsDeleted && r.UserId == userId && r.BookId == bookId);
            if (existingRating == null)
            {
                return false;
            }

            existingRating.Rate = ratingRequest.Rate;
            existingRating.Description = ratingRequest.Description;

            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<RatingResponse> GetRatingByUserIdAndBookId(string userId, long bookId)
        {
            var rating = await _unitOfWork.RatingRepository.GetAsync(r => !r.IsDeleted && r.UserId == userId && r.BookId == bookId, r => r.User, r => r.Book);
            return _mapper.Map<RatingResponse>(rating);
        }
    }
}