using BaseProject.Application.Models.Requests;
using BaseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Services
{
    public interface ILovedBookService
    {
        Task<IEnumerable<LovedBook>> GetLovedBooks();

        Task<bool> CreateLovedBook(LovedBookRequest lovedBook);

        Task<bool> DeleteLovedBook(string userId, long bookId);
    }
}