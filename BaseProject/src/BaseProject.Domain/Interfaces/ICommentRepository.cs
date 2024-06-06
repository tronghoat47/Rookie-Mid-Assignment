using BaseProject.Domain.Entities;

namespace BaseProject.Domain.Interfaces
{
    public interface ICommentRepository : IGenericRepository<Comment>
    {
        Task<Comment> GetNewestCommentByUserId(string userId);
    }
}