using Microsoft.AspNetCore.Mvc;
using PoPoy.Api.Helpers;
using PoPoy.Api.Services.CategoryService;
using PoPoy.Shared.ViewModels;
using System.Threading.Tasks;

namespace PoPoy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(
            ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var products = await _categoryService.GetAllCategories();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var category = await _categoryService.GetCateById(id);
            return Ok(category);
        }

        [HttpPost]
        [AuthorizeToken(AuthorizeToken.ADMIN_STAFF)]
        public async Task<IActionResult> CreateCategory(CategoryCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var category = await _categoryService.CreateCategory(request);
            return Ok(category);
        }

        [HttpPut("{categoryId}")]
        [AuthorizeToken(AuthorizeToken.ADMIN_STAFF)]

        public async Task<IActionResult> UpdateCategory(CategoryUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _categoryService.UpdateCategory(request);
            return Ok(result);
        }

        [HttpDelete("{categoryId}")]
        [AuthorizeToken(AuthorizeToken.ADMIN_STAFF)]

        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            var result = await _categoryService.DeleteCategory(categoryId);
            if (result == 0)
                return BadRequest();
            return Ok();
        }

        [HttpGet("searchCategory/{searchText}")]

        public async Task<IActionResult> SearchCategory(string searchText)
        {
            var result = await _categoryService.SearchCategory(searchText);
            if (result.Count == 0)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
