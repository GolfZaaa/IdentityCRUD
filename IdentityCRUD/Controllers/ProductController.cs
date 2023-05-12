using IdentityCRUD.Data;
using IdentityCRUD.DTOs.ProductDto;
using IdentityCRUD.Models;
using IdentityCRUD.Services.ProductManage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityCRUD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly DataContext _dataContext;
        private readonly IProductService _productService;

        public ProductController(DataContext dataContext,IProductService productService)
        {
            _dataContext = dataContext;
            _productService = productService;
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> Get()
        {
            var g = await _productService.GetProductListAsync();
            return Ok(g);
        }

        [HttpPost("[action]")]
        public async Task <IActionResult> AddProduct([FromForm]ProductRequest productRequest)
        {
            var res = await _productService.CreateAsync(productRequest);

            if ( !res ) return BadRequest("Create Not Success");

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetType()
        {
            var res = await _productService.GetTypeAsync();

            return Ok(res);
        }
    }
}
