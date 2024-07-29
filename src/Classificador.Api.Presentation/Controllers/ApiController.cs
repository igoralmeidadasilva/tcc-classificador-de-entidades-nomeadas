namespace Classificador.Api.Presentation.Controllers;

[Route("/api/")]
[AllowAnonymous]
[ApiController]
public sealed class ApiController : ControllerBase
{
    private readonly ILogger<ApiController> _logger;
    private readonly IMediator _mediator;

    public ApiController(ILogger<ApiController> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
    }

    [HttpPost(nameof(PostUser))]
    public async Task<IActionResult> PostUser(CreateUserCommand command)
    {
        Result response = await _mediator.Send(command);
        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        Result<Guid>? valueResponse = response as Result<Guid>;
        return Created("", valueResponse!.Value);
    }

    [HttpPost(nameof(PostLoginUser))]
    public async Task<IActionResult> PostLoginUser(LoginUserCommand command)
    {
        Result response = await _mediator.Send(command);

        if (!response.IsSuccess)
        {
            return Unauthorized(response);
        }

        Result<JwtToken>? valueResponse = response as Result<JwtToken>;
        return Ok(valueResponse!.Value);
    }

    [HttpGet(nameof(GetCountVotesForNamedEntities))]
    public async Task<IActionResult> GetCountVotesForNamedEntities([FromQuery]CountingVotesForNamedEntityQuery query)
    {
        Result response = await _mediator.Send(query);

        if (!response.IsSuccess)
        {
            return NotFound(response);
        }

        Result<IEnumerable<CountVoteForNamedEntity>>? valueResponse = response as Result<IEnumerable<CountVoteForNamedEntity>>;

        return Ok(valueResponse);
    }

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
