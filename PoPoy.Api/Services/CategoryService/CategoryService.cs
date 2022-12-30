using Microsoft.EntityFrameworkCore;
using PoPoy.Api.Data;
using PoPoy.Shared.Dto;
using PoPoy.Shared.Enum;
using PoPoy.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PoPoy.Api.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;

        public CategoryService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<CateVM>> GetAllCategories()
        {
            var query = from c in _context.Categories
                        where c.Status.Equals(Status.Active) && c.IsDeleted == false
                        select new { c };
            return await query.Select(x => new CateVM()
            {
                Id = x.c.Id,
                Name = x.c.Name,
                SortOrder = x.c.SortOrder,
                Url = x.c.Url
            }).ToListAsync();
        }

        public async Task<CateVM> GetCateById(int id)
        {
            var query = from c in _context.Categories
                        where c.Status.Equals(Status.Active) && c.Id == id
                        select new { c };
            return await query.Select(x => new CateVM()
            {
                Id = x.c.Id,
                Name = x.c.Name,
                SortOrder = x.c.SortOrder,
                Url = x.c.Url,
                Status = x.c.Status
            }).FirstOrDefaultAsync();
        }

        public async Task<List<CateVM>> SearchCategory(string searchText)
        {
            var query = from c in _context.Categories
                        where c.Name.ToLower().Contains(searchText.ToLower())
                        select new { c };

            return await query.Select(x => new CateVM()
            {
                Id = x.c.Id,
                Name = x.c.Name,
                SortOrder = x.c.SortOrder,
                Url = x.c.Url,
            }).ToListAsync();
        }
        public async Task<bool> CreateCategory(CategoryCreateRequest request)
        {
            var category = new Category()
            {
                Name = request.Name,
                SortOrder = request.SortOrder,
                Status = request.Status,
                Url = request.Url,
            };

            _context.Categories.Add(category);
            var result = await _context.SaveChangesAsync();
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateCategory(CategoryUpdateRequest request)
        {
            var category = await _context.Categories.FindAsync(request.Id);

            category.Name = request.Name;
            category.Status = request.Status;
            category.SortOrder = request.SortOrder;
            category.Url = request.Url;

            _context.Categories.Update(category);
            var result = await _context.SaveChangesAsync();
            if (result == 1)
            {
                return true;
            }
            return false;
        }

        public async Task<int> DeleteCategory(int categoryId)
        {
            var category = await _context.Categories.FindAsync(categoryId);
            category.IsDeleted = true;
            if (category == null) throw new Exception($"Cannot find a product: {categoryId}");

            _context.Categories.Update(category);

            return await _context.SaveChangesAsync();
        }
    }
}
