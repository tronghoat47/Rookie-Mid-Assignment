using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace BaseProject.Infrastructure.Repositories
{
    public class CommentRepository : GenericRepository<Comment>, ICommentRepository
    {
        private readonly LibraryContext _context;

        public CommentRepository(LibraryContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Comment> GetNewestCommentByUserId(string userId)
        {
            return await _context.Comments.OrderByDescending(c => c.CreatedAt).FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}