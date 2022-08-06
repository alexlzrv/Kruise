using System.Net.WebSockets;
using System.Security.Claims;
using JWT.Algorithms;
using JWT.Builder;
using Kruise.API.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Kruise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;

    public UsersController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpPost("token")]
    public async Task<IActionResult> CreareToken(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return NotFound($"User with Email: {request.Email} not found");
        }

        var isSuccess = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!isSuccess)
        {
            return BadRequest("Invalid password");
        }

        var token = JwtBuilder.Create()
                      .WithAlgorithm(new HMACSHA256Algorithm())
                      .WithSecret(AuthOptions.Secret)
                      .ExpirationTime(DateTimeOffset.UtcNow.AddHours(1).ToUnixTimeSeconds())
                      .AddClaim(ClaimTypes.NameIdentifier, user.Id)
                      .WithVerifySignature(true)
                      .Encode();
        return Ok(token);
    }

    [Authorize]
    [HttpGet("token")]
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

    [HttpPost]
    public async Task<IActionResult> Create(CreateUsersRequest request)
    {
        var user = new IdentityUser(request.Email) { Email = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }
}
