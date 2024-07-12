using Classificador.Api.Application.Commands.CreateUser;
using Classificador.Api.Application.Models.Options;
using Classificador.Api.SharedKernel.Shared.Result;
using MediatR;
using Microsoft.Extensions.Options;

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
        Result response = await _mediator.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        Result<Guid>? valueResponse = response as Result<Guid>;
        return Created("", valueResponse!.Value);
    }
}
