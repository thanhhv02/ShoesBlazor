using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PoPoy.Api.Helpers;
using PoPoy.Api.Services.LogService;
using PoPoy.Shared.Enum;
using System.Threading.Tasks;

namespace PoPoy.Api.Controllers
{
    [AuthorizeToken(RoleName.Admin)]
    [ApiController]
    [Route("api/[controller]/[action]")]

    public class LogController : Controller
    {
        private readonly ILogService logService;

        public LogController(ILogService logService)
        {
            this.logService = logService;
        }

        [HttpGet]

        public async Task<IActionResult> GetLogs()
        {
            var logs = await logService.GetAll();
            return Ok(logs);
        }
        [HttpGet]

        public async Task<IActionResult> GetLogById(int id)
        {
            var logs = await logService.GetLogById(id);
            return Ok(logs);
        }
        [HttpDelete]

        public async Task<IActionResult> ClearLog()
        {
            var logs = await logService.Clear();
            return Ok(logs);
        }
    }
}
