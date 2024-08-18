namespace Classificador.Api.Presentation.Controllers;

[Route("[controller]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public sealed class AdminController(ILogger<AdminController> logger, IMediator mediator) : WebController<AdminController>(logger, mediator)
{
    [HttpGet(nameof(AdminPanel))]
    public IActionResult AdminPanel()
    {
        return View();
    }
}