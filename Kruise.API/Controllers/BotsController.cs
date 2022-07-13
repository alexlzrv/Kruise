using Kruise.API.Telegram;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace Kruise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BotsController : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Post([FromServices] HandleUpdateService handleUpdateService)
    {
        await handleUpdateService.SendPost();
        return Ok();
    }

    [HttpPost("Exception")]
    public async Task<IActionResult> PostException([FromServices] HandleUpdateService handleUpdateService)
    {
        await handleUpdateService.SendPostException();
        return Ok();
    }
}
