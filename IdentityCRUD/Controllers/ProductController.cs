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
            //Select สามมารถเขียนได้ 2 แบบ 
            //var response = g.Select(ProductResponse.FromProduct).ToList();
            var response = g.Select(a => ProductResponse.FromProduct(a)).ToList();
            return Ok(response);
        }

        [HttpPost("[action]")]
        public async Task <IActionResult> AddProduct([FromForm]ProductRequest productRequest)
        {
            var res = await _productService.CreateAsync(productRequest);

            if ( res is not null) return BadRequest(res);

            return StatusCode(StatusCodes.Status201Created);
        }


        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateProduct([FromForm] ProductRequest productRequest)
        {
            var foundid = await _productService.GetProductByIDAsync((int)productRequest.Id);

            if (foundid is null) return NotFound();

            var res = await _productService.UpdateAsync(productRequest);

            if (res is not null) return BadRequest(res);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct( int id)
        {
            var response = await _productService.GetProductByIDAsync(id);

            if (response is null) return NotFound();

            var res = await _productService.DeleteAsync(response);

            if (!res) return BadRequest("Delete Not Success");

            return Ok();
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> GetType()
        {
            var res = await _productService.GetTypeAsync();

            return Ok(res);
        }

        [HttpGet("[action]")]
        public async Task<IActionResult> SearchProduct([FromQuery]string name="")
        {
            var res = (await _productService.SearchAsync(name)).Select(ProductResponse.FromProduct).ToList();

            return Ok(res);
        }

    }
}
