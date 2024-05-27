using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Infrastructure.Repositories
{
    public class LovedBookRepository : GenericRepository<LovedBook>, ILovedBookRepository
    {
        public LovedBookRepository(BaseProjectContext context) : base(context)
        {
        }
    }
}