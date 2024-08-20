using Classificador.Api.Domain.Interfaces.Services;

// FIXME: This class is being replaced by other controllers
namespace Classificador.Api.Presentation.Controllers;

[Route("/api/")]
[AllowAnonymous]
[ApiController]
[Obsolete]
public sealed class ApiController : ControllerBase
{
    private readonly ILogger<ApiController> _logger;
    private readonly IMediator _mediator;
    private readonly IEmailSenderService _emailSenderService;

    public ApiController(ILogger<ApiController> logger, IMediator mediator, IEmailSenderService emailSenderService)
    {
        _logger = logger;
        _mediator = mediator;
        _emailSenderService = emailSenderService;
    }

    [HttpPost(nameof(PostUser))]
    public async Task<IActionResult> PostUser(CreateUserCommand command)
    {
        Result response = await _mediator.Send(command);
        if(!response.IsSuccess)
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

        if(!response.IsSuccess)
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

        if(!response.IsSuccess)
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

    // [HttpPost(nameof(PostClassifyNamedEntity))]
    // public async Task<IActionResult> PostClassifyNamedEntity(CreateClassificationCommand command)
    // {
    //     Result response = await _mediator.Send(command);

    //     if (!response.IsSuccess)
    //     {
    //         return BadRequest(response);
    //     }

    //     Result<Guid>? valueResponse = response as Result<Guid>;

    //     return Created("", valueResponse!.Value);
    // }

    [HttpPost(nameof(PostClassifyNamedEntity))]
    public async Task<IActionResult> PostClassifyNamedEntity(string idUser, string idNamedEntity, string idCategory, string comment = "")
    {

        Result response = await _mediator.Send(new CreateClassificationCommand(new Guid(idUser), new Guid(idNamedEntity), new Guid(idCategory), comment));

        if (!response.IsSuccess)
        {
            return BadRequest(response);
        }

        Result<Guid>? valueResponse = response as Result<Guid>;

        return Created("", valueResponse!.Value);
    }

    [HttpPost(nameof(PostEmailSender))]
    public async Task<IActionResult> PostEmailSender(string from, string name, string subject, string body)
    {
        bool isSend = await _emailSenderService.SendEmailAsync(from, name, subject, body);
        if(isSend)
        {
            return Ok();
        }
        return BadRequest();
    }

}
