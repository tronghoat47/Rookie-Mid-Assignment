using BaseProject.Application.Models.Requests;
using BaseProject.Application.Services;
using BaseProject.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaseProject.WebAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var response = new GeneralResponse();
            try
            {
                var categories = await _categoryService.GetCategories();
                if (categories == null || !categories.Any())
                {
                    response.Success = false;
                    response.Message = "No categories found";
                    return NotFound(response);
                }
                response.Message = "Get categories successfully";
                response.Data = categories;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            var response = new GeneralResponse();
            try
            {
                var category = await _categoryService.GetCategoryById(id);
                if (category == null)
                {
                    response.Success = false;
                    response.Message = "Category not found";
                    return NotFound(response);
                }
                response.Message = "Get category successfully";
                response.Data = category;
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryRequest categoryRequest)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _categoryService.CreateCategory(categoryRequest);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Create category failed";
                    return BadRequest(response);
                }
                response.Message = "Create category successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }

        [HttpPut("{cateId}")]
        public async Task<IActionResult> UpdateCategory(long cateId, CategoryRequest categoryRequest)
        {
            var response = new GeneralResponse();
            try
            {
                var result = await _categoryService.UpdateCategory(cateId, categoryRequest);
                if (!result)
                {
                    response.Success = false;
                    response.Message = "Update category failed";
                    return BadRequest(response);
                }
                response.Message = "Update category successfully";
                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
                return BadRequest(response);
            }
        }
    }
}