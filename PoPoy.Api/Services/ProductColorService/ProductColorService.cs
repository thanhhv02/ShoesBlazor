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

namespace PoPoy.Api.Services.ProductColorService
{
    public class ProductColorService : IProductColorService
    {
        private readonly DataContext _dataContext;
        private readonly IMapper _mapper;

        public ProductColorService(DataContext dataContext, IMapper mapper)
        {
            _dataContext = dataContext;
            _mapper = mapper;
        }

        public async Task<List<ProductColorDto>> GetAll()
        {
       
            var list = await _mapper.ProjectTo<ProductColorDto>(_dataContext.ProductColors.AsNoTracking()).ToListAsync();
            return list;
        }

        public async Task<ProductColorDto> GetProductColorById(int id)
        {
            var query = _dataContext.ProductColors.Where(p => p.Id == id);
            var model = await _mapper.ProjectTo<ProductColorDto>(query).FirstOrDefaultAsync();
            return model;
        }

        public async Task<bool> CreateProductColor(ProductColorDto productColorDto)
        {
            productColorDto.Id = null;
            var productColor = _mapper.Map<ProductColor>(productColorDto);
            await _dataContext.AddAsync(productColor);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateProductColor(ProductColorDto productColorDto)
        {
            var productColorFound = await _dataContext.ProductColors.FindAsync(productColorDto.Id);
            if (productColorFound == null) { return false; }

            var productColor = _mapper.Map(productColorDto, productColorFound);
            _dataContext.Update(productColor);
            return await _dataContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProductColorById(int id)
        {
            var model = await _dataContext.ProductColors.FindAsync(id);
            _dataContext.Remove(model);

            return await _dataContext?.SaveChangesAsync() > 0;
        }

    }
}
