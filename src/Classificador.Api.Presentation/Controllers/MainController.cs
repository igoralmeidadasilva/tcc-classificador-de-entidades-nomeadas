
namespace Classificador.Api.Presentation.Controllers;

[Route("/api/")]
[AllowAnonymous]
public sealed class MainController(ILogger<MainController> logger, IMediator mediator) : ApiController<MainController>(logger, mediator)
{
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

    [HttpPost(nameof(PostLoginUsuario))]
    public async Task<IActionResult> PostLoginUsuario(LoginUserCommand command)
    {
        Result response = await _mediator.Send(command);
        
        if (!response.IsSuccess)
        {
            return Unauthorized(response);
        }

        Result<JwtToken>? valueResponse = response as Result<JwtToken>;
        return Ok(valueResponse!.Value);
    }

}
