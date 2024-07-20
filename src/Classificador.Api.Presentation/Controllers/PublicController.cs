namespace Classificador.Api.Presentation.Controllers;

[Route("/api/")]
[AllowAnonymous]
public sealed class PublicController(ILogger<PublicController> logger, IMediator mediator) : ApiController<PublicController>(logger, mediator)
{
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

}
