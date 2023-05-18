using IdentityCRUD.DTOs.ProductDto;
using IdentityCRUD.Models;

namespace IdentityCRUD.Services.ProductManage
{
    public interface IProductService
    {
        Task<List<Product>> GetProductListAsync();
        Task<Boolean>CreateAsync(ProductRequest request);
        Task<List<string>> GetTypeAsync();
        Task <Boolean>UpdateAsync(ProductRequest request);
        Task <Boolean>DeleteAsync(Product product);
        Task<Product> GetProductByIDAsync(int productid);
        Task<List<Product>> SearchAsync(string name);
    }
}
