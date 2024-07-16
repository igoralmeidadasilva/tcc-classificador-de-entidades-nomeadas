namespace Classificador.Api.Presentation.Controllers;

[Route("/api/[controller]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public sealed class AdminController(ILogger<AdminController> logger, IMediator mediator) : ApiController<AdminController>(logger, mediator)
{
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

    [HttpPost(nameof(PostPrescribingInformationEntityTxt))]
    public async Task<IActionResult> PostPrescribingInformationEntityTxt(CreatePrescribingInformationTxtCommand command)
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