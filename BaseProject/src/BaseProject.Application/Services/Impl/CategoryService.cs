using BaseProject.Application.Models.Requests;
using BaseProject.Domain.Entities;
using BaseProject.Domain.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseProject.Application.Services.Impl
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> CreateCategory(CategoryRequest categoryRequest)
        {
            try
            {
                var category = new Category
                {
                    Name = categoryRequest.Name
                };
                await _unitOfWork.CategoryRepository.AddAsync(category);
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<bool> UpdateCategory(long cateId, CategoryRequest categoryRequest)
        {
            try
            {
                var category = await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == cateId);
                if (category == null)
                    throw new KeyNotFoundException("Category not found");

                category.Name = categoryRequest.Name;
                _unitOfWork.CategoryRepository.Update(category);
                return await _unitOfWork.CommitAsync() > 0;
            }
            catch (SqlException)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _unitOfWork.CategoryRepository.GetAllAsync();
        }

        public async Task<Category> GetCategoryById(long id)
        {
            return await _unitOfWork.CategoryRepository.GetAsync(c => c.Id == id);
        }
    }
}