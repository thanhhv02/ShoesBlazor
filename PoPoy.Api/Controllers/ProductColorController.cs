using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoPoy.Api.Helpers;
using PoPoy.Api.Services.ProductColorService;
using PoPoy.Api.Services.ProductSizeService;
using PoPoy.Shared.ViewModels;
using System.Threading.Tasks;

namespace PoPoy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeToken(roles: AuthorizeToken.ADMIN_STAFF)]
    public class ProductColorController : Controller
    {
        private readonly IProductColorService productColorService;

        public ProductColorController(IProductColorService productColorService)
        {
            this.productColorService = productColorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductColors()
        {
            var list = await productColorService.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await productColorService.GetProductColorById(id);
            return Ok(model);
        }

        [HttpPut]


        public async Task<IActionResult> UpdateProductColor(ProductColorDto productColorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await productColorService.UpdateProductColor(productColorDto);
            return Ok(result);
        }

        [HttpPost]

        public async Task<IActionResult> CreateProductSize(ProductColorDto productColorDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await productColorService.CreateProductColor(productColorDto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductSizeById(int id)
        {
            var model = await productColorService.DeleteProductColorById(id);
            return Ok(model);
        }
    }
}
