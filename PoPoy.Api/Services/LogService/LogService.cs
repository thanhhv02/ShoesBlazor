using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PoPoy.Api.Data;
using PoPoy.Shared.Entities;
using PoPoy.Shared.ViewModels;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace PoPoy.Api.Services.LogService
{
    public class LogService : ILogService
    {

        private readonly DataContext _dataContext;

        public LogService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Logs>> GetAll()
        {

            var list = await _dataContext.Logs.AsNoTracking().OrderByDescending(p => p.LogId).ToListAsync();
            return list;
        }

        public async Task<Logs> GetLogById(int id)
        {
            var model = await _dataContext.Logs.FirstOrDefaultAsync(p => p.LogId == id);
            return model;
        }

        public async Task<bool> Clear()
        {
            await  _dataContext.Database.ExecuteSqlRawAsync("delete from logs");
            return true;
        }

    }
}
