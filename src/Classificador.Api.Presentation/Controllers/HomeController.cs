namespace Classificador.Api.Presentation.Controllers;

[Route("/api/[controller]")]
[Authorize(Roles = nameof(UserRole.Padrao))]
public sealed class HomeController(ILogger<HomeController> logger, IMediator mediator) : ApiController<HomeController>(logger, mediator)
{

}
