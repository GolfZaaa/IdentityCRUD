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

        public ProductService(IUploadFileService uploadFileService, IMapper mapper, DataContext dataContext)
        {
            _uploadFileService = uploadFileService;
            _mapper = mapper;
            _dataContext = dataContext;
        }


        public async Task<bool> DeleteAsync(Product product)
        {

            var files = await _dataContext.ProductImages.Where(a => a.ProductId == product.Id).Select(a => a.Image).ToListAsync();
            
            if (files.Count > 0)
            {
                await _uploadFileService.DeleteFileImages(files);
            }

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


        //public async Task<string> CreateAsync(ProductRequest productrequest)
        //{

        //    (string errorMessage, List<string> imageNames) = await UploadImageAsync(productrequest.FormFiles);
        //    if (!string.IsNullOrEmpty(errorMessage)) return errorMessage;

        //    var product = _mapper.Map<Product>(productrequest);
        //     await _dataContext.Products.AddAsync(product);
        //     var resultProduct = await _dataContext.SaveChangesAsync();


        //    int ResultproductImage = 0;
        //    if (imageNames.Count > 0 && ResultproductImage > 0)
        //    {
        //        var image = new List<ProductImage>();
        //        imageNames.ForEach(imageName => image.Add(new ProductImage
        //        {
        //            ProductId = product.Id,
        //            Image = imageName
        //        }
        //        ));
        //        await _dataContext.ProductImages.AddRangeAsync(image);
        //         ResultproductImage = await _dataContext.SaveChangesAsync();
        //        if (ResultproductImage > 0) return null;
        //        return "Create Product Image not Successfuly";

        //    }

        //    if (resultProduct > 0)  return null;
        //    return "Create Product not Success";
        //}


        //อีกรูปแบบหนึ่งของ Create
        public async Task<string> CreateAsync(ProductRequest productrequest)
        {

            (string errorMessage, List<string> imageNames) = await UploadImageAsync(productrequest.FormFiles);
            if (!string.IsNullOrEmpty(errorMessage)) return errorMessage;

            var product = _mapper.Map<Product>(productrequest);
            await _dataContext.Products.AddAsync(product);
            var resultProduct = await _dataContext.SaveChangesAsync();

            if (resultProduct <= 0) return "Create Product not Success";


            if (imageNames.Count > 0)
            {
                var image = new List<ProductImage>();
                imageNames.ForEach(imageName => image.Add(new ProductImage
                {
                    ProductId = product.Id,
                    Image = imageName
                }
                ));
                await _dataContext.ProductImages.AddRangeAsync(image);
                var ResultproductImage = await _dataContext.SaveChangesAsync();
                if (ResultproductImage <= 0) return "Create ProductImage not Successfuly";
            }
            return null;
        }

        public async Task<string> UpdateAsync(ProductRequest productrequest)
        {
            (string errorMessage, List<string> imageNames) = await UploadImageAsync(productrequest.FormFiles);
            if (!string.IsNullOrEmpty(errorMessage)) return errorMessage;

            //UpdateProduct
            var product = _mapper.Map<Product>(productrequest);
            _dataContext.Products.Update(product);
            var resultProduct = await _dataContext.SaveChangesAsync();
            if (resultProduct <= 0) return "Updated Product is not Success";

            //End UpdateProduct

            //Update ProductImage
            if (imageNames.Count > 0)
            {
                // Delete Old Files
                var productImage = await _dataContext.ProductImages.Where(a => a.ProductId.Equals(product.Id)).ToListAsync();
                if (productImage is not null)
                {
                    //Delete Database
                    _dataContext.ProductImages.RemoveRange(productImage);
                    _dataContext.SaveChangesAsync().Wait();

                    //Delete Files
                    var files = productImage.Select(a => a.Image).ToList();
                    await _uploadFileService.DeleteFileImages(files);
                }
                // End Delete

                #region Add new Files to ProductImage
                //Add to Database
                var image = new List<ProductImage>();
                imageNames.ForEach(imageName => image.Add(new ProductImage
                {
                    ProductId = product.Id,
                    Image = imageName
                }));
                await _dataContext.ProductImages.AddRangeAsync(image);
                var ResultproductImage = await _dataContext.SaveChangesAsync();
                if (ResultproductImage <= 0) return "Update ProductImage not Successfuly";


                #endregion End Add new File
            }
            //End Update ProductImage
            return null;

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

        public async Task DeleteOneWay(int productid)
        {
            var product = await _dataContext.Products.FindAsync(productid);

            if (product == null) return;

            var files = await _dataContext.ProductImages.Where(a => a.ProductId == productid).Select(a => a.Image).ToListAsync();
            if (files.Count > 0)
            {
                await _uploadFileService.DeleteFileImages(files);
            }

            _dataContext.Products.Remove(product);
            await _dataContext.SaveChangesAsync();


        }
    }
}
