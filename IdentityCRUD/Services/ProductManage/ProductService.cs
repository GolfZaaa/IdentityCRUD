using Azure.Core;
using IdentityCRUD.Data;
using IdentityCRUD.DTOs.ProductDto;
using IdentityCRUD.Models;
using IdentityCRUD.Services.UploadService;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Any;

namespace IdentityCRUD.Services.ProductManage
{
    public class ProductService : ControllerBase, IProductService
    {
        private readonly IUploadFileService _uploadFileService;
        private readonly IMapper _mapper;
        private readonly DataContext _dataContext;

        public ProductService(IUploadFileService uploadFileService,IMapper mapper, DataContext dataContext)
        {
            _uploadFileService = uploadFileService;
            _mapper = mapper;
            _dataContext = dataContext;
        }

        public async Task<string> CreateAsync(ProductRequest productrequest)
        {

            (string errorMessage, List<string> imageNames) = await UploadImageAsync(productrequest.FormFiles);

            if (!string.IsNullOrEmpty(errorMessage)) return errorMessage;



            var product = _mapper.Map<Product>(productrequest);
             await _dataContext.Products.AddAsync(product);
             var resultProduct = await _dataContext.SaveChangesAsync();


            var image = new List<ProductImage>();
            imageNames.ForEach(imageName => image.Add(new ProductImage {ProductId = product.Id,Image = imageName}));
             await _dataContext.ProductImages.AddRangeAsync(image);
            var ResultproductImage = await _dataContext.SaveChangesAsync();

            if (resultProduct > 0 && ResultproductImage > 0 ) return null;
            return "Create Not Success";
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

        public async Task<(string errorMessage, List<string> imageNames)> UploadImageAsync(IFormFileCollection formFiles)
        {
            var errorMessage = string.Empty;
            var imageNames = new List<string>();

            if (_uploadFileService.IsUpload(formFiles))
            {
                errorMessage = _uploadFileService.Validation(formFiles);
                if (errorMessage is null)
                {
                    //บันทึกลงไฟล์ในโฟลเดอร์ 
                    imageNames = await _uploadFileService.UploadImages(formFiles);
                }
            }
            return (errorMessage, imageNames);
        }





    }
}
