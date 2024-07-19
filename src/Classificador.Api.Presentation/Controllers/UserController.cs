namespace Classificador.Api.Presentation.Controllers;

[Route("/api/[controller]")]
[Authorize(Roles = nameof(UserRole.Padrao))]

public sealed class UserController(ILogger<UserController> logger, IMediator mediator) : ApiController<UserController>(logger, mediator)
{
    [HttpPost(nameof(PostClassifyNamedEntity))]
    public async Task<IActionResult> PostClassifyNamedEntity(CreateClassificationCommand command)
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
