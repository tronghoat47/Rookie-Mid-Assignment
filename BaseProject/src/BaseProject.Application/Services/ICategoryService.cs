using BaseProject.Application.Models.Requests;
using BaseProject.Application.Models.Responses;

namespace BaseProject.Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryResponse>> GetCategories();

        Task<CategoryResponse> GetCategoryById(long id);

        Task<bool> CreateCategory(CategoryRequest categoryRequest);

        Task<bool> UpdateCategory(long cateId, CategoryRequest categoryRequest);
    }
}