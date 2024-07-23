using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pmm.server.Models;

namespace pmm.server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> ReceiveMobileData([FromBody] MobileDataDto data)
        {
            return Ok();
        }
    }
}
