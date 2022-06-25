using Microsoft.AspNetCore.Mvc;

namespace Kruise.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ModelsController : ControllerBase
{
    private readonly ILogger<ModelsController> _logger;

    public ModelsController(ILogger<ModelsController> logger, ModelServiceA serviceA, ModelServiceB serviceB)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok();
    }
}

public class ModelServiceA
{
    private readonly string _id = Guid.NewGuid().ToString();

    public ModelServiceA(ILogger<ModelServiceA> logger, ModelRepositoryA repositoryA, ModelRepositoryB repositoryB)
    {
        logger.LogInformation(_id);
    }
}

public class ModelServiceB
{
    private readonly string _id = Guid.NewGuid().ToString();

    public ModelServiceB(ILogger<ModelServiceB> logger, ModelRepositoryA repositoryA, ModelRepositoryB repositoryB)
    {
        logger.LogInformation(_id);

    }
}

public class ModelRepositoryA
{
    private readonly string _id = Guid.NewGuid().ToString();

    public ModelRepositoryA(ILogger<ModelRepositoryA> logger)
    {
        logger.LogInformation(_id);

    }
}

public class ModelRepositoryB
{
    private readonly string _id = Guid.NewGuid().ToString();

    public ModelRepositoryB(ILogger<ModelRepositoryB> logger)
    {
        logger.LogInformation(_id);

    }
}
