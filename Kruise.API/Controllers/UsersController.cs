using System.Security.Claims;
using JWT.Algorithms;
using JWT.Builder;
using Kruise.API.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kruise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    [HttpPost("token")]
    public async Task<IActionResult> CreareToken(LoginRequest request)
    {
        var token = JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm())
                      .WithSecret(AuthOptions.Secret)
                      .AddClaim(ClaimName.ExpirationTime, DateTimeOffset.UtcNow.AddMinutes(10).ToUnixTimeSeconds())
                      .AddClaim(ClaimTypes.NameIdentifier, 42)
                      .WithVerifySignature(true)
                      .Encode();
        return Ok(token);
    }

    [Authorize]
    [HttpPost("token/data")]
    public async Task<IActionResult> DecodeToken(string token)
    {
        var user = User;
        var json = JwtBuilder.Create()
                     .WithAlgorithm(new HMACSHA256Algorithm())
                     .WithSecret(AuthOptions.Secret)
                     .MustVerifySignature()
                     .Decode(token);

        return Ok(json);
    }
}
