using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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