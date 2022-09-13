using Microsoft.EntityFrameworkCore;
using PoPoy.Api.Data;
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
                        select new { c };
            return await query.Select(x => new CateVM()
            {
                Id = x.c.Id,
                Name = x.c.Name
            }).ToListAsync();
        }

        public async Task<CateVM> GetCateById(int id)
        {
            var query = from c in _context.Categories
                        select new { c };
            return await query.Select(x => new CateVM()
            {
                Id = x.c.Id,
                Name = x.c.Name
            }).FirstOrDefaultAsync();
        }
    }
}
