namespace Classificador.Api.Presentation.Controllers;

[Route("/api/[controller]")]
[ApiController]
public sealed class AdminController : ControllerBase
{
    private readonly ILogger<AdminController> _logger;
    private readonly IMediator _mediator;

    public AdminController(ILogger<AdminController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPatch(nameof(PatchFuncaoUsuarioParaAdmin))]
    public async Task<IActionResult> PatchFuncaoUsuarioParaAdmin(UpdateUserRoleToAdminCommand command)
    {
        Result response = await _mediator.Send(command);

        if(!response.IsSuccess)
        {
            return NotFound(response);
        }

        return NoContent();
    }

    [HttpPatch(nameof(PatchFuncaoUsuarioParaPadrao))]
    public async Task<IActionResult> PatchFuncaoUsuarioParaPadrao(UpdateUserRoleToStandardCommand command)
    {
        Result response = await _mediator.Send(command);

        if(!response.IsSuccess)
        {
            return NotFound(response);
        }

        return NoContent();
    }
}