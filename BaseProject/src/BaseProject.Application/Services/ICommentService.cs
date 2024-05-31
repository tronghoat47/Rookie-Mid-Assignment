using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;

namespace BaseProject.Application.Services
{
    public interface ICommentService
    {
        Task<bool> CreateComment(CommentRequest commentRequest);

        Task<bool> UpdateComment(long commentId, CommentRequest commentRequest);

        Task<bool> DeleteComment(long commentId);

        Task<CommentResponse> GetCommentById(long commentId);

        Task<IEnumerable<CommentResponse>> GetCommentsByUserId(string userId);

        Task<IEnumerable<CommentResponse>> GetCommentsByBookId(long bookId);

        Task<IEnumerable<CommentResponse>> GetComments();
    }
}