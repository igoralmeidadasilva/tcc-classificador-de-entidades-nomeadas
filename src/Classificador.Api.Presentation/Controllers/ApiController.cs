namespace Classificador.Api.Presentation.Controllers;

[ApiController]
public abstract class ApiController<T> : ControllerBase
{
    protected readonly ILogger<T> _logger;
    protected readonly IMediator _mediator;

    protected ApiController(ILogger<T> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

}
