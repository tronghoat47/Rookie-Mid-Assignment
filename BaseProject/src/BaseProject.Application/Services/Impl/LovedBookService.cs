using AutoMapper;
using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;

namespace BaseProject.Application.Services.Impl
{
    public class LovedBookService : ILovedBookService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LovedBookService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateLovedBook(LovedBookRequest lovedBookRequest)
        {
            var lovedBook = new LovedBook
            {
                UserId = lovedBookRequest.UserId,
                BookId = lovedBookRequest.BookId
            };

            await _unitOfWork.LovedBookRepository.AddAsync(lovedBook);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> DeleteLovedBook(string userId, long bookId)
        {
            var lovedBook = await _unitOfWork.LovedBookRepository.GetAsync(lb => !lb.IsDeleted && lb.UserId == userId && lb.BookId == bookId);
            if (lovedBook == null)
            {
                return false;
            }

            _unitOfWork.LovedBookRepository.SoftDelete(lovedBook);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<IEnumerable<LovedBookResponse>> GetLovedBooks()
        {
            var lovedBooks = await _unitOfWork.LovedBookRepository.GetAllAsync(lb => !lb.IsDeleted, lb => lb.User, lb => lb.Book);
            return lovedBooks.Select(lb => _mapper.Map<LovedBookResponse>(lb));
        }

        public async Task<LovedBookResponse> GetLovedBooksByBook(long bookId)
        {
            var lovedBook = await _unitOfWork.LovedBookRepository.GetAsync(lb => !lb.IsDeleted && lb.BookId == bookId, lb => lb.User, lb => lb.Book);
            return _mapper.Map<LovedBookResponse>(lovedBook);
        }

        public async Task<LovedBookResponse> GetLovedBooksByUser(string userId)
        {
            var lovedBook = await _unitOfWork.LovedBookRepository.GetAsync(lb => !lb.IsDeleted && lb.UserId == userId, lb => lb.User, lb => lb.Book);
            return _mapper.Map<LovedBookResponse>(lovedBook);
        }
    }
}