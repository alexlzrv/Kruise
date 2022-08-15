using Kruise.API.Contracts;
using Kruise.Domain;
using Kruise.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kruise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountsController : ControllerBase
{
    private readonly ILogger<AccountsController> _logger;
    private readonly IAccountRepository _repository;

    public AccountsController(ILogger<AccountsController> logger, IAccountRepository repository)
    {
        _logger = logger;
        _repository = repository;
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateAccountRequest request)
    {
        var account = AccountModel.Create(request.Name);
        if (account.IsFailure)
        {
            _logger.LogError(account.Error);
            return Problem(account.Error);
        }

        var accountId = await _repository.Add(account.Value);
        return Ok(accountId);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var accounts = await _repository.Get();
        return Ok(accounts);
    }

    [HttpGet("{accountId}")]
    public async Task<IActionResult> Get(long accountId)
    {
        var account = await _repository.Get(accountId);
        if (account == null)
        {
            return NotFound($"Posts with Id:{accountId} not found");
        }

        return Ok(account);
    }

    [HttpDelete("{accountId}")]
    public async Task<IActionResult> Delete(long accountId)
    {
        await _repository.Remove(accountId);
        return Ok();
    }

    [HttpPut("{accountId}")]
    public async Task<IActionResult> Update(long accountId, UpdateAccountRequest request)
    {
        var updatedAccount = AccountModel.Create(request.Name);
        if (updatedAccount.IsFailure)
        {
            _logger.LogError(updatedAccount.Error);
            return Problem(updatedAccount.Error);
        }

        var updatedResult = await _repository.Update(accountId, updatedAccount.Value);
        if (updatedResult.IsFailure)
        {
            _logger.LogError(updatedResult.Error);
            return Problem(updatedResult.Error);
        }

        return Ok();
    }
}
