using BaseProject.Application.Models.Requests;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Services.Impl
{
    public class LovedBookService : ILovedBookService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LovedBookService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateLovedBook(LovedBookRequest lovedBookRequest)
        {
            try
            {
                var lovedBook = new LovedBook
                {
                    UserId = lovedBookRequest.UserId,
                    BookId = lovedBookRequest.BookId
                };

                await _unitOfWork.LovedBookRepository.AddAsync(lovedBook);
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<bool> DeleteLovedBook(string userId, long bookId)
        {
            try
            {
                var lovedBook = await _unitOfWork.LovedBookRepository.GetAsync(lb => lb.UserId == userId && lb.BookId == bookId);
                if (lovedBook == null)
                {
                    throw new KeyNotFoundException("Loved book not found");
                }

                _unitOfWork.LovedBookRepository.Delete(lovedBook);
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<IEnumerable<LovedBook>> GetLovedBooks()
        {
            try
            {
                return await _unitOfWork.LovedBookRepository.GetAllAsync(x => true, lb => lb.Book, lb => lb.User);
            }
            catch (SqlException)
            {
                return null;
            }
        }
    }
}