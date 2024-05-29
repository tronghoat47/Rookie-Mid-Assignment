using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.DataAccess;

namespace BaseProject.Infrastructure.Repositories
{
    public class LovedBookRepository : GenericRepository<LovedBook>, ILovedBookRepository
    {
        public LovedBookRepository(BaseProjectContext context) : base(context)
        {
        }
    }
}