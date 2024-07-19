namespace Classificador.Api.Presentation.Controllers;

[Route("/api/[controller]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public sealed class AdminController(ILogger<AdminController> logger, IMediator mediator) : ApiController<AdminController>(logger, mediator)
{
    [HttpPatch(nameof(PatchUserRoleToAdmin))]
    public async Task<IActionResult> PatchUserRoleToAdmin(UpdateUserRoleToAdminCommand command)
    {
        Result response = await _mediator.Send(command);

        if(!response.IsSuccess)
        {
            return NotFound(response);
        }

        return NoContent();
    }

    [HttpPatch(nameof(PatchUserRoleToStandard))]
    public async Task<IActionResult> PatchUserRoleToStandard(UpdateUserRoleToStandardCommand command)
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

    [HttpPost(nameof(PostSpecialty))]
    public async Task<IActionResult> PostSpecialty(CreateSpecialtyCommand command)
    {
        Result response = await _mediator.Send(command);

        if(!response.IsSuccess)
        {
            return BadRequest(response);
        }

        Result<Guid>? valueResponse = response as Result<Guid>;

        return Created("", valueResponse!.Value);
    }

    [HttpPost(nameof(PostCategory))]
    public async Task<IActionResult> PostCategory(CreateCategoryCommand command)
    {
        Result response = await _mediator.Send(command);

        if(!response.IsSuccess)
        {
            return BadRequest(response);
        }

        Result<Guid>? valueResponse = response as Result<Guid>;

        return Created("", valueResponse!.Value);
    }

}