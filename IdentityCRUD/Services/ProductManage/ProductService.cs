using IdentityCRUD.Data;
using IdentityCRUD.DTOs.ProductDto;
using IdentityCRUD.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace IdentityCRUD.Services.ProductManage
{
    public class ProductService : ControllerBase, IProductService
    {
        private readonly DataContext _dataContext;

        public ProductService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<Boolean> CreateAsync(ProductRequest request)
        {
            var product = new Product
            {
                Name = request.Name,
                Price = request.Price,
                QuantityInStock = request.QuantityInStock,
                Description = request.Description,
                Type = request.Type,
            };

             await _dataContext.Products.AddAsync(product);

             var result = await _dataContext.SaveChangesAsync();

            if (result > 0) return Task.CompletedTask.IsCompletedSuccessfully;

            return false;
        }

        public async Task<List<Product>> GetProductListAsync()
        {
            var result = await _dataContext.Products.Include(a => a.ProductImages).ToListAsync();
            return result;
        }

        public async Task<List<string>> GetTypeAsync()
        {
            var get = await _dataContext.Products.GroupBy(a => a.Type).Select(a => a.Key).ToListAsync();

            return get;
        }
    }
}
