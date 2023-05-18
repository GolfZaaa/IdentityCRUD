using IdentityCRUD.Data;
using IdentityCRUD.DTOs.ProductDto;
using IdentityCRUD.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace IdentityCRUD.Services.ProductManage
{
    public class ProductService : ControllerBase, IProductService
    {
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public ProductService(IMapper mapper, DataContext dataContext)
        {
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<Boolean> CreateAsync(ProductRequest productrequest)
        {

            var product = _mapper.Map<Product>(productrequest);

             await _dataContext.Products.AddAsync(product);

             var result = await _dataContext.SaveChangesAsync();

            if (result > 0) return true;

            return false;
        }

        public async Task<bool> DeleteAsync(Product product)
        {
             _dataContext.Products.Remove(product);

            var result = await _dataContext.SaveChangesAsync();

            if (result > 0) return true;

            return false;
        }

        public async Task<Product> GetProductByIDAsync(int productid)
        {
            var re = await _dataContext.Products.AsNoTracking()
                .FirstOrDefaultAsync(a => a.Id == productid);

            if (re is null) return null;

            return re;
        }

        public async Task<List<Product>> GetProductListAsync()
        {
            var result = await _dataContext.Products.Include(a => a.ProductImages)
                .OrderByDescending(a => a.Id)
                .ToListAsync();
            return result;
        }

        public async Task<List<string>> GetTypeAsync()
        {
            var get = await _dataContext.Products.GroupBy(a => a.Type).Select(a => a.Key).ToListAsync();

            return get;
        }

        public async Task<List<Product>> SearchAsync(string name)
        {
            //สามารถเขียนได้ 2 แบบอันนี้เป็นการ Include เพื่อหาชื่อขอวภาพ
            //var search = await _dataContext.Products.Include(a => a.ProductImages).Where(a => a.Name == name).ToListAsync();
            var search = await _dataContext.Products.Include(a => a.ProductImages).Where(a => a.Name.Contains(name)).ToListAsync();
            
            return search;
        }

        public async Task<bool> UpdateAsync(ProductRequest productrequest)
        {

            var product = _mapper.Map<Product>(productrequest);

            _dataContext.Products.Update(product);

            var result = await _dataContext.SaveChangesAsync();

            if (result > 0) return true;

            return false;

        }
    }
}
