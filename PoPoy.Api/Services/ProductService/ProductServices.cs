using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Polly;
using PoPoy.Api.Data;
using PoPoy.Api.Extensions;
using PoPoy.Api.Helpers;
using PoPoy.Api.Services.SortService;
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
        private readonly ISortHelper<Product> _sortHelper;

        public ProductServices(DataContext dataContext,
                                IWebHostEnvironment env,
                                IConfiguration configuration,
                                ISortHelper<Product> sortHelper)
        {
            _dataContext = dataContext;
            _env = env;
            this._configuration = configuration;
            this._sortHelper = sortHelper;
        }

        public async Task<PagedList<Product>> GetAll(ProductParameters productParameters)
        {
            using (_dataContext)
            {
                var list_product = new List<Product>();

                list_product = await _dataContext.Products
                    .Search(productParameters.searchText)
                    .Sort(productParameters.OrderBy)//sort by product coloumn 
                    .SortByPrice(productParameters.OrderBy, _dataContext)//sort by product quantity column
                    .Include(x => x.ProductImages)
                    .Include(x => x.ProductQuantities)
                    .Where(x=>x.IsDeleted == false)
                    .ToListAsync();

                if(productParameters.ColorId != null)
                {
                    list_product = (from p in list_product
                                    join pq in _dataContext.ProductQuantities
                                    on p.Id equals pq.ProductId
                                    select pq).Where(x => productParameters.ColorId.Contains(x.ColorId)).Select(x => x.Product).ToList();
                }

                if(productParameters.SizeId is not null)
                {
                    list_product = (from p in list_product
                                    join pq in _dataContext.ProductQuantities
                                    on p.Id equals pq.ProductId
                                    select pq).Where(x => productParameters.SizeId.Contains(x.SizeId)).Select(x => x.Product).ToList();
                }

                list_product = list_product.DistinctBy(x => x.Id).ToList();
                //list_product.Shuffle();
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
                        where p.IsDeleted == false
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
                //OriginalPrice = x.p.OriginalPrice,
                //Price = x.p.Price,
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
                //OriginalPrice = product.OriginalPrice,
                //Price = product.Price,

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
                //Price = request.Price,
                Title = request.Title,
                //OriginalPrice = request.OriginalPrice,
                Description = request.Description,
                Views = 0,
                DateCreated = AppExtensions.GetDateTimeNow()
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
            //product.Price = request.Price;
            //product.OriginalPrice = request.OriginalPrice;

            _dataContext.Products.Update(product);
            var result = await _dataContext.SaveChangesAsync();
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<int> DeleteProduct(int productId)
        {
            var product = await _dataContext.Products.FirstOrDefaultAsync(x => x.Id == productId);
            product.IsDeleted = true;
            if (product == null) throw new Exception($"Cannot find image: {productId}");


            _dataContext.Products.Update(product);

            return await _dataContext.SaveChangesAsync();
        }

        public async ValueTask<List<ProductQuantity>> FilterAllByIdsAsync(int[] ids, int[] sizes, int[] color)
        {
            using (_dataContext)
            {
                List<ProductQuantity> productQuantities = new List<ProductQuantity>();
                var sortedList = new List<GetProductToCart>();

                for (int index = 0; index < ids.Length; index++)
                {
                    sortedList.Add(new GetProductToCart()
                    {
                        Id = ids[index],
                        SizeId = sizes[index],
                        ColorId = color[index]
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
                                .Include(x => x.Product)
                                .ThenInclude(x => x.ProductQuantities)
                            .Where(x => x.SizeId == p.SizeId)
                                .Include(x => x.Size)
                            .Where(x => x.ColorId == p.ColorId)
                                .Include(x => x.Color)
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
                //Price = x.p.Price,
                //OriginalPrice = x.p.OriginalPrice,
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
                    DateCreated = AppExtensions.GetDateTimeNow(),
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
            var GET_FILE_NAME_FROM_PATH = images.ImagePath.Replace(_configuration["ApiUrl"] + "/uploads/", "");
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
                                      select p)
                                      .Sort(productParameters.OrderBy)
                                      .SortByPrice(productParameters.OrderBy, _dataContext)
                                      .Include(x => x.ProductQuantities)
                                      .Include(x => x.ProductImages).ToListAsync();
            if (productParameters.ColorId != null)
            {
                list_product = (from p in list_product
                                join pq in _dataContext.ProductQuantities
                                on p.Id equals pq.ProductId
                                select pq).Where(x => productParameters.ColorId.Contains(x.ColorId)).Select(x => x.Product).ToList();
            }

            if (productParameters.SizeId is not null)
            {
                list_product = (from p in list_product
                                join pq in _dataContext.ProductQuantities
                                on p.Id equals pq.ProductId
                                select pq).Where(x => productParameters.SizeId.Contains(x.SizeId)).Select(x => x.Product).ToList();
            }

            list_product = list_product.DistinctBy(x => x.Id).ToList();
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
                SizeName = x.p.SizeName,
                ProductQuantities = null,
            }).ToListAsync();
        }

        public async Task<ServiceResponse<bool>> SizeAssign(int id, SizeAssignRequest request)
        {
            try
            {
                //var product = await _dataContext.ProductQuantities.FirstOrDefaultAsync(x => x.ProductId == id);
                foreach (var item in request.Sizes)
                {
                    var productQuantity = await _dataContext.ProductQuantities
                        .FirstOrDefaultAsync(x => x.SizeId == int.Parse(item.Id)
                        && x.ProductId == id && x.ColorId == int.Parse(item.ColorId));

                    //if (productQuantity != null && item.Selected == false)
                    //{
                    //    _dataContext.ProductQuantities.Remove(productQuantity);
                    //}
                    if (productQuantity == null && item.Selected)
                    {
                        await _dataContext.ProductQuantities.AddAsync(new ProductQuantity()
                        {
                            SizeId = int.Parse(item.Id),
                            ColorId = Convert.ToInt32(item.ColorId),
                            ProductId = id,
                            Quantity = item.Qty,
                            Price = item.Price
                        });
                    }
                    else if (productQuantity != null && item.Selected == true
                        && (productQuantity.Quantity != item.Qty || productQuantity.Price != item.Price))
                    {
                        productQuantity.Quantity = item.Qty;
                        productQuantity.Price = item.Price;
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
        public async Task<PagedList<Product>> SearchProducts(ProductParameters productParameters)
        {
            using (_dataContext)
            {
                var list_product = await _dataContext.Products
                                .Where(p => p.Title.ToLower().Contains(productParameters.searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(productParameters.searchText.ToLower()))
                                .Include(p => p.ProductImages)
                                .Where(x=>x.IsDeleted == false)
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
                                .Where(p => (p.Title.ToLower().Contains(searchText.ToLower()) ||
                                    p.Description.ToLower().Contains(searchText.ToLower()) && p.IsDeleted == false))
                                .ToListAsync();
        }

        public async Task<bool> SeedProduct()
        {
            try
            {
                var productsTable = _dataContext.Products.ToList();
                var productSizeTable = _dataContext.ProductSizes.ToList();
                var productColorTable = _dataContext.ProductSizes.ToList();
                var pmax = productsTable.Max(x => x.Id);
                var pmin = productsTable.Min(x => x.Id);
                var smax = productSizeTable.Max(x => x.Id);
                var smin = productSizeTable.Min(x => x.Id);
                var cmax = productColorTable.Max(x => x.Id);
                var cmin = productColorTable.Min(x => x.Id);
                for (int i = pmin; i <= pmax; i++)
                {
                    for (int j = smin; j <= 3; j++)
                    {
                        var random = new Random();
                        if (productsTable.Where(x => x.Id == i) != null
                                && productSizeTable.Where(x => x.Id == j) != null)
                        {
                            var product = new ProductQuantity()
                            {
                                ProductId = i,
                                SizeId = j,
                                ColorId = j,
                                Quantity = 1000,
                                Price = random.Next(2000000, 3000000)
                            };
                            _dataContext.ProductQuantities.Add(product);
                        }
                        else
                        {
                            continue;
                        }

                    }

                }
                await _dataContext.SaveChangesAsync();
                return true;
            }
            catch { }
            return false;
        }

        public async Task<string> GetProductQuantityAndPrice(int sizeId, int prodId, int colorId)
        {

            var query = await _dataContext.ProductQuantities.FirstOrDefaultAsync(x => x.ProductId == prodId && x.SizeId == sizeId && x.ColorId == colorId);
            if (query != null)
            {
                object results = new
                {
                    Quantity = query.Quantity,
                    Price = query.Price
                };
                return JsonConvert.SerializeObject(results, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
            }
            object result = new
            {
                Quantity = 0,
                Price = 0
            };
            return JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

        }

        public async Task<List<ProductColor>> GetAllColorProduct()
        {
            var query = from p in _dataContext.ProductColors.Include(x => x.ProductQuantities)
                        select new { p };
            return await query.Select(x => new ProductColor()
            {
                Id = x.p.Id,
                ColorName = x.p.ColorName,
                ProductQuantities = null,
            }).ToListAsync();
        }
        public async Task<string> GetProductVariants(int productId)
        {
            //var result = new List<string>();
            var temp = new List<int>();
            var list_quantity = await _dataContext.ProductQuantities.Where(x => x.ProductId == productId)
                .Include(x => x.Color)
                .Include(x => x.Size)
                .ToListAsync();

            object result = new
            {
                variants = list_quantity
            };
            return JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
        }

        public async Task<int> DeleteProductVariant(int variantId)
        {
            var product = await _dataContext.ProductQuantities.FirstOrDefaultAsync(x => x.Id == variantId);
            _dataContext.ProductQuantities.Remove(product);

            return await _dataContext.SaveChangesAsync();
        }
    }
}
