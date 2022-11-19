using PoPoy.Shared.Entities.Area;
using System.Threading.Tasks;

namespace PoPoy.Client.Services.AreaService
{
    public interface IAreaService
    {
        public Task<Area> GetProvince();
        public Task<Area> GetDistrict(string code);
        public Task<Area> GetComune(string code);
    }
}
