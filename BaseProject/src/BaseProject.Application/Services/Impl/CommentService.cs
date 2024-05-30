﻿using AutoMapper;
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
    public class CommentService : ICommentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CommentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateComment(CommentRequest commentRequest)
        {
            var comment = new Comment
            {
                Content = commentRequest.Content,
                BookId = commentRequest.BookId,
                UserId = commentRequest.UserId,
            };

            await _unitOfWork.CommentRepository.AddAsync(comment);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> DeleteComment(long commentId)
        {
            var comment = await _unitOfWork.CommentRepository.GetAsync(c => !c.IsDeleted && c.Id == commentId);
            if (comment == null)
            {
                return false;
            }
            _unitOfWork.CommentRepository.SoftDelete(comment);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<CommentResponse> GetCommentById(long commentId)
        {
            var comment = await _unitOfWork.CommentRepository.GetAsync(c => c.Id == commentId);
            if (comment == null)
            {
                return null;
            }

            return _mapper.Map<CommentResponse>(comment);
        }

        public async Task<IEnumerable<CommentResponse>> GetCommentsByBookId(long bookId)
        {
            var comments = await _unitOfWork.CommentRepository.GetAllAsync(c => !c.IsDeleted && c.BookId == bookId);
            return _mapper.Map<IEnumerable<CommentResponse>>(comments);
        }

        public async Task<IEnumerable<CommentResponse>> GetCommentsByUserId(string userId)
        {
            var comments = await _unitOfWork.CommentRepository.GetAllAsync(c => !c.IsDeleted && c.UserId == userId);
            return _mapper.Map<IEnumerable<CommentResponse>>(comments);
        }

        public async Task<bool> UpdateComment(long commentId, CommentRequest commentRequest)
        {
            var existingComment = await _unitOfWork.CommentRepository.GetAsync(c => !c.IsDeleted && c.Id == commentId);
            if (existingComment == null)
            {
                return false;
            }

            existingComment.Content = commentRequest.Content;
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<IEnumerable<CommentResponse>> GetComments()
        {
            var comments = await _unitOfWork.CommentRepository.GetAllAsync(c => !c.IsDeleted);
            return _mapper.Map<IEnumerable<CommentResponse>>(comments);
        }
    }
}