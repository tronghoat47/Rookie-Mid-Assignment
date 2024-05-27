using BaseProject.Application.Models.Requests;
using BaseProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetCategories();

        Task<Category> GetCategoryById(long id);

        Task<bool> CreateCategory(CategoryRequest categoryRequest);

        Task<bool> UpdateCategory(long cateId, CategoryRequest categoryRequest);
    }
}