using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Polly;
using PoPoy.Api.Data;
using PoPoy.Api.Extensions;
using PoPoy.Shared.Common;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Paging;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.ProductService
{

    public class ProductServices : IProductServices
    {
        private readonly DataContext _dataContext;
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public ProductServices(DataContext dataContext,
                                IWebHostEnvironment env,
                                IConfiguration configuration)
        {
            _dataContext = dataContext;
            _env = env;
            this._configuration = configuration;
        }

        public async Task<PagedList<Product>> GetAll(ProductParameters productParameters)
        {
            using (_dataContext)
            {
                var list_product = await AllProductInTable();
                list_product.Shuffle();
                return PagedList<Product>
                            .ToPagedList(list_product, productParameters.PageNumber, productParameters.PageSize);
            }
        }
        public async Task<ServiceResponse<Product>> Get(int id)
        {
            using (_dataContext)
            {
                var product = await _dataContext.Products.Include(x => x.ProductImages)
                    .Include(x => x.ProductInCategories)
                    .ThenInclude(x => x.Category)
                    .Include(x => x.ProductQuantities)
                    .ThenInclude(x => x.Size)
                    .Include(x => x.ProductQuantities)
                    .ThenInclude(x => x.Color)
                    .FirstOrDefaultAsync(x => x.Id == id);
                product.Views++;
                await _dataContext.SaveChangesAsync();
                var response = new ServiceResponse<Product>
                {
                    Data = product
                };
                return response;
            }
        }

        public async Task<List<ProductVM>> GetAllProducts()
        {
            var query = from p in _dataContext.Products
                        join pic in _dataContext.ProductInCategories on p.Id equals pic.ProductId into ppic
                        from pic in ppic.DefaultIfEmpty()
                        join c in _dataContext.Categories on pic.CategoryId equals c.Id into picc
                        from c in picc.DefaultIfEmpty()
                        join pi in _dataContext.ProductImages on p.Id equals pi.ProductId into ppi
                        from pi in ppi.DefaultIfEmpty()
                        select new { p, pic, pi };

            //var productQuantities = await (from pq in _dataContext.ProductQuantities
            //                               join p in _dataContext.Products on pq.ProductId equals p.Id
            //                               select pq).ToListAsync();

            var productSizes = await (from ps in _dataContext.ProductSizes
                                      join pq in _dataContext.ProductQuantities on ps.Id equals pq.SizeId
                                      select ps).ToListAsync();

            return await query.Select(x => new ProductVM()
            {
                Id = x.p.Id,
                Title = x.p.Title,
                DateCreated = x.p.DateCreated,
                Description = x.p.Description,
                OriginalPrice = x.p.OriginalPrice,
                Price = x.p.Price,
                Views = x.p.Views,
                //ProductQuantities = productQuantities,
                ProductSizes = productSizes,
                ThumbnailImage = x.pi.ImagePath
            }).ToListAsync();
        }

        public async Task<ProductVM> GetById(int productId)
        {
            var product = await _dataContext.Products.Include(x => x.ProductImages).FirstOrDefaultAsync(x => x.Id == productId);

            var categories = await (from c in _dataContext.Categories
                                    join p in _dataContext.Products on c.Id equals p.CategoryId
                                    where p.Id == productId
                                    select c.Name).ToListAsync();

            var productQuantities = await (from pq in _dataContext.ProductQuantities
                                           join p in _dataContext.Products on pq.ProductId equals p.Id
                                           where p.Id == productId
                                           select pq.SizeId.ToString()).ToListAsync();

            var productViewModel = new ProductVM()
            {
                Id = product.Id,
                Title = product.Title,
                DateCreated = product.DateCreated,
                Description = product.Description,
                OriginalPrice = product.OriginalPrice,
                Price = product.Price,
      
                Views = product.Views,
                Categories = categories,
                Sizes = productQuantities,
                ProductImages = product.ProductImages,
            };
            return productViewModel;
        }
        public async Task<bool> CreateProduct(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                Title = request.Title,
                OriginalPrice = request.OriginalPrice,
                Description = request.Description,
                Views = 0,
                DateCreated = DateTime.Now
            };

            _dataContext.Products.Add(product);
            var result = await _dataContext.SaveChangesAsync();
            if (result == 1)
            {
                return true;
            }
            return false;
        }
        public async Task<bool> UpdateProduct(ProductUpdateRequest request)
        {
            var product = await _dataContext.Products.FindAsync(request.Id);

            product.Title = request.Title;
            product.Description = request.Description;
            product.Price = request.Price;
            product.OriginalPrice = request.OriginalPrice;

            _dataContext.Products.Update(product);
            var result = await _dataContext.SaveChangesAsync();
            if (result == 1)
            {
                return true;
            }
            return false;
        }
        public async Task<List<Product>> AllProductInTable()
        {
            return await _dataContext.Products.Include(x => x.ProductImages).ToListAsync();
        }
        public async Task<int> DeleteProduct(int productId)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
            if (product == null) throw new Exception($"Cannot find image: {productId}");


            _dataContext.Products.Remove(product);

            return await _dataContext.SaveChangesAsync();
        }

        public async ValueTask<List<ProductQuantity>> FilterAllByIdsAsync(int[] ids, int[] sizes)
        {
            using (_dataContext)
            {
                List<ProductQuantity> productQuantities = new List<ProductQuantity>();
                var sortedList = new List<IdAndSize>();

                for (int index = 0; index < ids.Length; index++)
                {
                    sortedList.Add(new IdAndSize()
                    {
                        Id = ids[index],
                        Size = sizes[index]
                    });
                }
                if (ids != null && sizes != null)
                {
                    foreach (var p in sortedList)
                    {
                        productQuantities.Add(await _dataContext.ProductQuantities
                            .Where(x => x.ProductId == p.Id)
                                .Include(x => x.Product)
                                .ThenInclude(x => x.ProductImages)
                            .Where(x => x.SizeId == p.Size)
                                .Include(x => x.Size)
                                .Include(x => x.Product)
                                    .ThenInclude(x => x.ProductQuantities)
                            .FirstOrDefaultAsync());
                    }
                    return productQuantities;
                }
                else
                {
                    return new List<ProductQuantity>();
                }
            }
        }
        public async Task<List<ProductVM>> SearchProduct(string searchText)
        {
            var query = from p in _dataContext.Products
                        where p.Title.ToLower().Contains(searchText.ToLower())
                        select new { p };

            var categories = await (from c in _dataContext.Categories
                                    join pic in _dataContext.ProductInCategories on c.Id equals pic.CategoryId
                                    select c.Name).ToListAsync();

            return await query.Select(x => new ProductVM()
            {
                Id = x.p.Id,
                Title = x.p.Title,
                Description = x.p.Description,
                Price = x.p.Price,
                OriginalPrice = x.p.OriginalPrice,
                Views = x.p.Views,
                Categories = categories
            }).ToListAsync();
        }
        public async Task<ServiceResponse<List<UploadResult>>> UploadProductImage(List<IFormFile> files, int productId)
        {
            List<UploadResult> uploadResults = new List<UploadResult>();

            foreach (var file in files)
            {
                var uploadResult = new UploadResult();
                //string trustedFileNameForFileStorage;
                var untrustedFileName = file.FileName;
                uploadResult.FileName = untrustedFileName;
                //var trustedFileNameForDisplay = WebUtility.HtmlEncode(untrustedFileName);

                //trustedFileNameForFileStorage = Path.GetRandomFileName();
                var path = Path.Combine(_env.ContentRootPath, "wwwroot/uploads", untrustedFileName);

                await using FileStream fs = new(path, FileMode.Create);
                await file.CopyToAsync(fs);

                uploadResult.StoredFileName = untrustedFileName;
                uploadResult.ContentType = file.ContentType;
                uploadResults.Add(uploadResult);

                var productImg = new ProductImage()
                {
                    ProductId = productId,
                    ImagePath = _configuration["ApiUrl"] + "/uploads/" + untrustedFileName,
                    DateCreated = DateTime.Now,
                    FileSize = file.Length
                };
                _dataContext.ProductImages.Add(productImg);
            }

            await _dataContext.SaveChangesAsync();

            return new ServiceSuccessResponse<List<UploadResult>>(uploadResults);
        }

        public async Task<bool> DeleteProductImage(int productId)
        {
            var images = _dataContext.ProductImages.Where(i => i.Id == productId).FirstOrDefault();
            if (images == null)
                return false;
            _dataContext.Remove(images);
            await _dataContext.SaveChangesAsync();
            var GET_FILE_NAME_FROM_PATH = images.ImagePath.Replace(_configuration["ApiUrl"] + "/", "");
            var path = Path.Combine(_env.ContentRootPath, "wwwroot/uploads", GET_FILE_NAME_FROM_PATH);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            return true;
        }

        public async Task<ServiceResponse<bool>> CategoryAssign(int id, CategoryAssignRequest request)
        {
            var product = await _dataContext.Products.FindAsync(id);
            if (product == null)
            {
                return new ServiceErrorResponse<bool>($"Sản phẩm với id {id} không tồn tại");
            }
            foreach (var category in request.Categories)
            {
                var productInCategory = await _dataContext.ProductInCategories
                    .FirstOrDefaultAsync(x => x.CategoryId == int.Parse(category.Id)

                    && x.ProductId == id);
                if (productInCategory != null && category.Selected == false)
                {
                    _dataContext.ProductInCategories.Remove(productInCategory);
                }
                else if (productInCategory == null && category.Selected)
                {
                    await _dataContext.ProductInCategories.AddAsync(new ProductInCategory()
                    {
                        CategoryId = int.Parse(category.Id),
                        ProductId = id
                    });
                    product.CategoryId = int.Parse(category.Id);
                    _dataContext.Products.Update(product);
                }
            }
            await _dataContext.SaveChangesAsync();
            return new ServiceSuccessResponse<bool>();
        }
        public async Task<PagedList<Product>> GetProductsByCategory(ProductParameters productParameters, string categoryUrl)
        {
            var list_product = await (from p in _dataContext.Products
                                      join pic in _dataContext.ProductInCategories on p.Id equals pic.ProductId into ppic
                                      from pic in ppic.DefaultIfEmpty()
                                      join c in _dataContext.Categories on pic.CategoryId equals c.Id into picc
                                      from c in picc.DefaultIfEmpty()
                                      where c.Url == categoryUrl
                                      select p).Include(x => x.ProductImages).ToListAsync();
            return PagedList<Product>
                            .ToPagedList(list_product, productParameters.PageNumber, productParameters.PageSize);
        }
        public async Task<List<ProductSize>> GetSizeProduct(int productId)
        {
            var result = new List<string>();
            var temp = new List<int>();
            var list_quantity = await _dataContext.ProductQuantities.Where(x => x.ProductId == productId)
                .ToListAsync();
            list_quantity.Select(x => x.Size).ToList();
            foreach (var item in list_quantity)
            {
                temp.Add(item.SizeId);
            }
            return await _dataContext.ProductSizes.Where(x => temp.Contains(x.Id)).ToListAsync();
        }

        public async Task<List<ProductSize>> GetAllSizesProduct()
        {
            var query = from p in _dataContext.ProductSizes.Include(x => x.ProductQuantities)
                        select new { p };
            return await query.Select(x => new ProductSize()
            {
                Id = x.p.Id,
                Size = x.p.Size,
                ProductQuantities = null,
            }).ToListAsync();
        }

        public async Task<ServiceResponse<bool>> SizeAssign(int id, SizeAssignRequest request)
        {
            try
            {
                var product = await _dataContext.ProductQuantities.FirstOrDefaultAsync(x => x.ProductId == id);
                foreach (var item in request.Sizes)
                {
                    var productQuantity = await _dataContext.ProductQuantities
                        .FirstOrDefaultAsync(x => x.SizeId == int.Parse(item.Id)
                        && x.ProductId == id);

                    if (productQuantity != null && item.Selected == false)
                    {
                        _dataContext.ProductQuantities.Remove(productQuantity);
                    }
                    else if (productQuantity == null && item.Selected)
                    {
                        await _dataContext.ProductQuantities.AddAsync(new ProductQuantity()
                        {
                            SizeId = int.Parse(item.Id),
                            ColorId = 1,
                            ProductId = id,
                            Quantity = item.Qty
                        });
                    }
                    else if(productQuantity != null && item.Selected == true && productQuantity.Quantity != item.Qty)
                    {
                        productQuantity.Quantity = item.Qty;
                        _dataContext.Update(productQuantity);
                    }
                }
                await _dataContext.SaveChangesAsync();
                return new ServiceSuccessResponse<bool>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<PagedList<Product>> SearchProducts(ProductParameters productParameters, string searchText)
        {
            using (_dataContext)
            {
                var list_product = await _dataContext.Products
                                .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(searchText.ToLower()))
                                .Include(p => p.ProductImages)
                                .ToListAsync();
                return PagedList<Product>
                            .ToPagedList(list_product, productParameters.PageNumber, productParameters.PageSize);
            }
        }

        public async Task<ServiceResponse<List<string>>> GetProductSearchSuggestions(string searchText)
        {
            var products = await FindProductsBySearchText(searchText);

            List<string> result = new List<string>();

            foreach (var product in products)
            {
                if (product.Title.Contains(searchText, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(product.Title);
                }

                if (product.Description != null)
                {
                    var punctuation = product.Description.Where(char.IsPunctuation)
                        .Distinct().ToArray();
                    var words = product.Description.Split()
                        .Select(s => s.Trim(punctuation));

                    foreach (var word in words)
                    {
                        if (word.Contains(searchText, StringComparison.OrdinalIgnoreCase)
                            && !result.Contains(word))
                        {
                            result.Add(word);
                        }
                    }
                }
            }

            return new ServiceResponse<List<string>> { Data = result };
        }
        private async Task<List<Product>> FindProductsBySearchText(string searchText)
        {
            return await _dataContext.Products
                                .Where(p => p.Title.ToLower().Contains(searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(searchText.ToLower()))
                                .ToListAsync();
        }

        public async Task<bool> SeedProduct()
        {
            try
            {
                var pmax = _dataContext.Products.Max(x => x.Id);
                var pmin = _dataContext.Products.Min(x => x.Id);
                var smax = _dataContext.ProductSizes.Max(x => x.Id);
                var smin = _dataContext.ProductSizes.Min(x => x.Id);
                for (int i = pmin; i <= pmax; i++)
                {
                    for (int j = smin; j <= smax; j++)
                    {
                        var product = new ProductQuantity()
                        {
                            ProductId = i,
                            SizeId = j,
                            ColorId = 1,
                            Quantity = 1000
                        };
                        _dataContext.ProductQuantities.Add(product);
                    }
                }
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch { }
            return false;
        }

        public int GetQuantityOfProduct(int sizeId, int prodId)
        {

            var query = _dataContext.ProductQuantities.FirstOrDefault(x => x.ProductId == prodId && x.SizeId == sizeId);
            if (query != null)
            {
                var quantity = query.Quantity;
                return quantity;
            }
            return 0;

        }

    }
}
