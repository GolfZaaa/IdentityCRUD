using IdentityCRUD.DTOs.ProductDto;
using IdentityCRUD.Models;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Writers;

namespace IdentityCRUD.Services.ProductManage
{
    public interface IProductService
    {
        Task<List<Product>> GetProductListAsync();
        Task<string> CreateAsync(ProductRequest request);
        Task<List<string>> GetTypeAsync();
        Task <string>UpdateAsync(ProductRequest request);
        Task <Boolean>DeleteAsync(Product product);
        Task<Product> GetProductByIDAsync(int productid);
        Task<List<Product>> SearchAsync(string name);
        Task<(string errorMessage, List<string> imageNames)> UploadImageAsync(IFormFileCollection formFiles);
        Task DeleteOneWay (int productid);
    }
}
