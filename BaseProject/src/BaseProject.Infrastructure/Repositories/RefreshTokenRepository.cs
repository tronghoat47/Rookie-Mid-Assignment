﻿using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using BaseProject.Infrastructure.DataAccess;

namespace BaseProject.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(LibraryContext context) : base(context)
        {
        }
    }
}