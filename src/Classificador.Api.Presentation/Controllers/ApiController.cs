namespace Classificador.Api.Presentation.Controllers;

[ApiController]
public abstract class ApiController<T>(ILogger<T> logger, IMediator mediator) : ControllerBase
{
    protected readonly ILogger<T> _logger = logger;
    protected readonly IMediator _mediator = mediator;
   
}
