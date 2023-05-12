using IdentityCRUD.DTOs.ProductDto;
using IdentityCRUD.Models;

namespace IdentityCRUD.Services.ProductManage
{
    public interface IProductService
    {
        Task<List<Product>> GetProductListAsync();
        Task<Boolean>CreateAsync(ProductRequest request);
        Task<List<string>> GetTypeAsync();

    }
}
