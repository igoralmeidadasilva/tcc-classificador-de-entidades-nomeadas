namespace Classificador.Api.Presentation.Controllers;

[Route("/api/[controller]")]
[Authorize(Roles = nameof(UserRole.Padrao))]

public sealed class HomeController(ILogger<HomeController> logger, IMediator mediator) : ApiController<HomeController>(logger, mediator)
{
    [HttpPost(nameof(PostClassificarEntidadeNomeada))]
    public async Task<IActionResult> PostClassificarEntidadeNomeada(CreateClassificationCommand command)
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
