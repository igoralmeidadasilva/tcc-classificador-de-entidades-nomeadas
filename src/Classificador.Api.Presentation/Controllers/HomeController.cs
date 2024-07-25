using Classificador.Api.Application.Extensions;
using Classificador.Api.Application.Models;

namespace Classificador.Api.Presentation.Controllers;

[Route("[controller]")]
[AllowAnonymous]
public sealed class HomeController : WebController<HomeController>
{
    public HomeController(ILogger<HomeController> logger, IMediator mediator) : base(logger, mediator)
    {
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [HttpGet(nameof(Error))]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet(nameof(PageNotFound))]
    public IActionResult PageNotFound()
    {
        return View();
    }

    [HttpGet(nameof(About))]
    public IActionResult About()
    {
        return View();
    }

    [HttpGet(nameof(Contact))]
    public IActionResult Contact()
    {
        return View();
    }

    [HttpPost(nameof(SignUp))]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        CreateUserCommand command = viewModel;
        Result response = await _mediator.Send(command);

        if (!response.IsSuccess)
        {
            GenerateErrorMessage(response.Error.Message);

            var validationError = response.Error as ValidationError;
            if(validationError != null)
            {
                TempData["EmailFailures"] = validationError!.ExtractValidationErrors("CreateUser.Email");
                TempData["PasswordFailures"] = validationError!.ExtractValidationErrors("CreateUser.Password");
                TempData["ConfirmPasswordFailures"] = validationError!.ExtractValidationErrors("CreateUser.ConfirmPassword");
                TempData["NameFailures"] = validationError!.ExtractValidationErrors("CreateUser.Name");
                TempData["ContactFailures"] = validationError!.ExtractValidationErrors("CreateUser.Contact");
            }

            return View();
        }

        GenerateSuccessMessage("Cadastro realizado com sucesso.");
        return RedirectToAction("Login");
    }

    [HttpGet(nameof(SignUp))]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpGet(nameof(Login))]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet(nameof(Classifications))]
    public IActionResult Classifications()
    {
        return View();
    }

}