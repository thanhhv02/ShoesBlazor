using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Polly;
using PoPoy.Api.Data;
using PoPoy.Api.Extensions;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Paging;
using PoPoy.Shared.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.ProductSizeService
{
    public class ProductSizeService : IProductSizeService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ProductSizeService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<List<ProductSizeDto>> GetAll()
        {
       
            var list = await _mapper.ProjectTo<ProductSizeDto>(_dataContext.ProductSizes.AsNoTracking()).ToListAsync();
            return list;
        }

        public async Task<ProductSizeDto> GetProductSizeById(int id)
        {
            var query = _dataContext.ProductSizes.Where(p => p.Id == id);
            var model = await _mapper.ProjectTo<ProductSizeDto>(query).FirstOrDefaultAsync();
            return model;
        }

        public async Task<bool> CreateProductSize(ProductSizeDto productSizeDto)
        {
            productSizeDto.Id = null;
            var productSize = _mapper.Map<ProductSize>(productSizeDto);
            await _dataContext.AddAsync(productSize);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductSize(ProductSizeDto productSizeDto)
        {
            var productSizeFound = await _dataContext.ProductSizes.FindAsync(productSizeDto.Id);
            if (productSizeFound == null) { return false; }

            var productSize = _mapper.Map(productSizeDto , productSizeFound);

            _dataContext.Update(productSize);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductSizeById(int id)
        {
            var model = await _dataContext.ProductSizes.FindAsync(id);
            _dataContext.Remove(model);

            return await _dataContext?.SaveChangesAsync() > 0;
        }

    }
}
