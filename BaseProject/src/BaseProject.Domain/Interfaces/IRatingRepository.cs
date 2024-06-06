using BaseProject.Domain.Entities;

namespace BaseProject.Domain.Interfaces
{
    public interface IRatingRepository : IGenericRepository<Rating>
    {
        Task<Rating> GetNewestRatingByUserId(string userId);
    }
}