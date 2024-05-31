using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.DataAccess;

namespace BaseProject.Infrastructure.Repositories
{
    public class BorrowingDetailRepository : GenericRepository<BorrowingDetail>, IBorrowingDetailRepository
    {
        public BorrowingDetailRepository(LibraryContext context) : base(context)
        { }
    }
}