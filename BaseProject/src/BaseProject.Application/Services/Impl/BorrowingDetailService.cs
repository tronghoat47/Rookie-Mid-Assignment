using AutoMapper;
using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;
using BaseProject.Domain.Constants;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.Services;

namespace BaseProject.Application.Services.Impl
{
    public class BorrowingDetailService : IBorrowingDetailService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;

        public BorrowingDetailService(IUnitOfWork unitOfWork, IMapper mapper, IEmailService emailService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
        }

        public async Task<bool> CreateAsync(BorrowingDetailRequest request)
        {
            var borrowingDetails = await _unitOfWork.BorrowingDetailRepository.GetAllAsync(b => !b.IsDeleted && b.BorrowingId == request.BorrowingId);
            if (borrowingDetails.Count() >= 5)
            {
                throw new IOException("Don't exceed 5 borrowed books per request borrow");
            }
            var borrowingDetail = await _unitOfWork.BorrowingDetailRepository.GetAsync(b => !b.IsDeleted && b.BorrowingId == request.BorrowingId && b.BookId == request.BookId);
            if (borrowingDetail != null)
            {
                throw new IOException("Book is already borrowed");
            }
            var newBorrowingDetail = new BorrowingDetail
            {
                BorrowingId = request.BorrowingId,
                BookId = request.BookId,
                ReturnedAt = request.ReturnedAt
            };
            await _unitOfWork.BorrowingDetailRepository.AddAsync(newBorrowingDetail);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> UpdateStatusAsync(long borrowingId, long bookId, BorrowingDetailUpdateStatusRequest request)
        {
            var borrowingDetail = await _unitOfWork.BorrowingDetailRepository.GetAsync(b => !b.IsDeleted && b.BorrowingId == borrowingId && b.BookId == bookId);
            if (borrowingDetail == null)
            {
                return false;
            }
            borrowingDetail.Status = request.Status;
            _unitOfWork.BorrowingDetailRepository.Update(borrowingDetail);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<bool> UpdateStatusExtendAsync(long borrowingId, long bookId, BorrowingDetailUpdateStatusExtendRequest request)
        {
            var borrowingDetail = await _unitOfWork
                .BorrowingDetailRepository
                .GetAsync(b => !b.IsDeleted && b.BorrowingId == borrowingId && b.BookId == bookId
                , b => b.Borrowing, b => b.Borrowing.Requestor);
            if (borrowingDetail == null)
            {
                return false;
            }
            borrowingDetail.StatusExtend = request.StatusExtend;
            _unitOfWork.BorrowingDetailRepository.Update(borrowingDetail);
            if (await _unitOfWork.CommitAsync() <= 0)
            {
                return false;
            }

            string subject = string.Empty;
            string content = string.Empty;
            string emailTo = borrowingDetail.Borrowing?.Requestor?.Email ?? string.Empty;
            if (request.StatusExtend == StatusBorrowingExtend.APPROVED)
            {
                subject = EmailConstants.SUBJECT_APPROVE_EXTENSION;
                content = EmailConstants.BodyApproveExtensionEmail(borrowingId, bookId);
            }
            else if (request.StatusExtend == StatusBorrowingExtend.REJECTED)
            {
                subject = EmailConstants.SUBJECT_REJECT_EXTENSION;
                content = EmailConstants.BodyRejectExtensionEmail(borrowingId, bookId);
            }

            await _emailService.SendEmailAsync(emailTo, subject, content);
            return true;
        }

        public async Task<bool> UpdateReturnedAtAsync(long borrowingId, long bookId, BorrowingDetailUpdateReturnedAtRequest request)
        {
            var borrowingDetail = await _unitOfWork
                .BorrowingDetailRepository
                .GetAsync(b => !b.IsDeleted && b.BorrowingId == borrowingId && b.BookId == bookId
                , b => b.Borrowing, b => b.Borrowing.Approver);
            if (borrowingDetail == null)
            {
                return false;
            }
            borrowingDetail.ReturnedAt = request.ReturnedAt;
            _unitOfWork.BorrowingDetailRepository.Update(borrowingDetail);
            if (await _unitOfWork.CommitAsync() <= 0)
            {
                return false;
            }

            string subject = EmailConstants.SUBJECT_EXTEND_BORROWING;
            string content = EmailConstants.BodyExtendBorrowingEmail(borrowingId, bookId);
            string emailTo = borrowingDetail.Borrowing?.Approver?.Email ?? string.Empty;

            await _emailService.SendEmailAsync(emailTo, subject, content);
            return true;
        }

        public async Task<bool> DeleteAsync(long borrowingId, long bookId)
        {
            var borrowingDetail = await _unitOfWork.BorrowingDetailRepository.GetAsync(b => !b.IsDeleted && b.BorrowingId == borrowingId && b.BookId == bookId);
            if (borrowingDetail == null)
            {
                return false;
            }
            _unitOfWork.BorrowingDetailRepository.SoftDelete(borrowingDetail);
            return await _unitOfWork.CommitAsync() > 0;
        }

        public async Task<IEnumerable<BorrowingDetailResponse>> GetBorrowingDetailsAsync()
        {
            var borrowingDetails = await _unitOfWork
                .BorrowingDetailRepository
                .GetAllAsync(b => !b.IsDeleted
                , b => b.Borrowing, b => b.Borrowing.Approver, b => b.Borrowing.Requestor);
            return _mapper.Map<IEnumerable<BorrowingDetailResponse>>(borrowingDetails);
        }

        public async Task<BorrowingDetailResponse> GetBorrowingDetailAsync(long borrowingId, long bookId)
        {
            var borrowingDetail = await _unitOfWork
                .BorrowingDetailRepository
                .GetAsync(b => b.BorrowingId == borrowingId && b.BookId == bookId
                , b => b.Borrowing, b => b.Borrowing.Approver, b => b.Borrowing.Requestor);
            return _mapper.Map<BorrowingDetailResponse>(borrowingDetail);
        }
    }
}