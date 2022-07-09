using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace Kruise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BotsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromServices] HandleUpdateService handleUpdateService,
                                          [FromBody] Update update)
    {
        await handleUpdateService.EchoAsync(update);
        return Ok();
    }
}
