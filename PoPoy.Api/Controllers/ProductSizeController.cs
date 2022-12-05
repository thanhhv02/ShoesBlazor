using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoPoy.Api.Helpers;
using PoPoy.Api.Services.ProductSizeService;
using PoPoy.Shared.ViewModels;
using System.Threading.Tasks;

namespace PoPoy.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AuthorizeToken(roles: AuthorizeToken.ADMIN_STAFF)]
    public class ProductSizeController : Controller
    {
        private readonly IProductSizeService productSizeService;

        public ProductSizeController(IProductSizeService productSizeService)
        {
            this.productSizeService = productSizeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProductSizes()
        {
            var list = await productSizeService.GetAll();
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var model = await productSizeService.GetProductSizeById(id);
            return Ok(model);
        }

        [HttpPut]


        public async Task<IActionResult> UpdateProductSize(ProductSizeDto productSizeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await productSizeService.UpdateProductSize(productSizeDto);
            return Ok(result);
        }

        [HttpPost]

        public async Task<IActionResult> CreateProductSize(ProductSizeDto productSizeDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await productSizeService.CreateProductSize(productSizeDto);
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductSizeById(int id)
        {
            var model = await productSizeService.DeleteProductSizeById(id);
            return Ok(model);
        }
    }
}
