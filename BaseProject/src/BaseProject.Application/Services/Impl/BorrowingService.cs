using AutoMapper;
using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using BaseProject.Domain.Constants;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.Services;

namespace BaseProject.Application.Services.Impl
{
    public class BorrowingService : IBorrowingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public BorrowingService(IUnitOfWork unitOfWork, IEmailService emailService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _emailService = emailService;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(BorrowingRequest request)
        {
            var borrowing = new Borrowing
            {
                RequestorId = request.RequestorId,
                CreatedAt = DateTime.Now,
                Status = StatusBorrowing.PENDING,
                BorrowingDetails = request.BorrowingDetails?.Select(b => new BorrowingDetail
                {
                    BorrowingId = b.BorrowingId,
                    BookId = b.BookId,
                    ReturnedAt = b.ReturnedAt,
                }).ToList() ?? new List<BorrowingDetail>()
            };
            if (borrowing.BorrowingDetails.Count > 5)
            {
                throw new IOException("Don't exceed 5 borrowed books per request borrow");
            }
            var borrowings = await _unitOfWork.BorrowingRepository.GetAllAsync(b => b.RequestorId == request.RequestorId && b.CreatedAt.Month == DateTime.Now.Month);
            if (borrowings.Count() > 3)
            {
                throw new IOException("Limit 3 borrowed reuest per month");
            }
            await _unitOfWork.BorrowingRepository.AddAsync(borrowing);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> UpdateStatusAsync(long id, BorrowingUpdateStatusRequest request)
        {
            var borrowing = await _unitOfWork.BorrowingRepository.GetAsync(b => !b.IsDeleted && b.Id == id, b => b.Requestor);
            if (borrowing == null)
            {
                return false;
            }
            borrowing.Status = request.Status;
            borrowing.ApproverId = request.ApproverId;
            _unitOfWork.BorrowingRepository.Update(borrowing);
            if (await _unitOfWork.CommitAsync() <= 0)
            {
                return false;
            }
            string subject = string.Empty;
            string content = string.Empty;
            if (request.Status == StatusBorrowing.APPROVED)
            {
                subject = EmailConstants.SUBJECT_BORROWING_APPROVED;
                content = EmailConstants.BodyBorrowingApprovedEmail(id);
            }
            else if (request.Status == StatusBorrowing.REJECTED)
            {
                subject = EmailConstants.SUBJECT_BORROWING_REJECTED;
                content = EmailConstants.BodyBorrowingRejectedEmail(id);
            }
            await _emailService.SendEmailAsync(borrowing.Requestor?.Email ?? string.Empty, subject, content);
            return true;
        }

        public async Task<BorrowingResponse> GetByIdAsync(long id)
        {
            var borrowing = await _unitOfWork.BorrowingRepository.GetAsync(b => !b.IsDeleted && b.Id == id, b => b.BorrowingDetails);
            if (borrowing == null)
            {
                return null;
            }
            return _mapper.Map<BorrowingResponse>(borrowing);
        }

        public async Task<IEnumerable<BorrowingResponse>> GetByRequestorIdAsync(string requestorId)
        {
            var borrowings = await _unitOfWork.BorrowingRepository.GetAllAsync(b => b.RequestorId == requestorId, b => b.Requestor, b => b.Approver);
            return _mapper.Map<IEnumerable<BorrowingResponse>>(borrowings);
        }

        public async Task<IEnumerable<BorrowingResponse>> GetByApproverIdAsync(string approverId)
        {
            var borrowings = await _unitOfWork.BorrowingRepository.GetAllAsync(b => b.ApproverId == approverId, b => b.Requestor, b => b.Approver);
            return _mapper.Map<IEnumerable<BorrowingResponse>>(borrowings);
        }

        public async Task<IEnumerable<BorrowingResponse>> GetAllAsync()
        {
            var borrowings = await _unitOfWork.BorrowingRepository.GetAllAsync(b => true, b => b.Requestor, b => b.Approver);
            return _mapper.Map<IEnumerable<BorrowingResponse>>(borrowings);
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var borrowing = await _unitOfWork.BorrowingRepository.GetAsync(b => !b.IsDeleted && b.Id == id);
            if (borrowing == null)
            {
                return false;
            }
            _unitOfWork.BorrowingRepository.SoftDelete(borrowing);
            return await _unitOfWork.CommitAsync() > 0;
        }
    }
}