using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.DataAccess;

namespace BaseProject.Infrastructure.Repositories
{
    public class BorrowingRepository : GenericRepository<Borrowing>, IBorrowingRepository
    {
        public BorrowingRepository(LibraryContext context) : base(context)
        {
        }
    }
}