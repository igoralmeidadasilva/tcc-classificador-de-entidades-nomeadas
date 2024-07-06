using Classificador.Api.Application.Commands.CreateUser;
using MediatR;

namespace Classificador.Api.Presentation.Controllers;

[Route("/api/")]
[ApiController]
public sealed class MainController : ControllerBase
{
    private readonly ILogger<MainController> _logger;
    private readonly IMediator _mediator;

    public MainController(ILogger<MainController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost(nameof(PostCriarUsuario))]
    public async Task<IActionResult> PostCriarUsuario(CreateUserCommand command)
    {
        _ = await _mediator.Send(command);
        return Created();
    }
}
