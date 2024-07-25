using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pmm.core.Models;
using pmm.core.Services.Interfaces;

namespace pmm.server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController(IMobileDataService mobileDataService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ReceiveMobileData([FromBody] MobileDataDto data)
        {
            await mobileDataService.SaveMobileDataAsync(data);

            return NoContent();
        }
    }
}
