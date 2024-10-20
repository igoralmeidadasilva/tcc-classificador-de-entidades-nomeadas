using Classificador.Api.Application.Queries.GetAllSpecialties;
using Classificador.Api.SharedKernel.Shared.Results;

namespace Classificador.Api.Presentation.Controllers;

[AllowAnonymous]
[Route("[controller]")]
public sealed class AuthController : WebController<AuthController>
{
    public AuthController(ILogger<AuthController> logger, IMediator mediator) : base(logger, mediator)
    {}

    [HttpGet("sign-up")]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel)
    {
        Result<IEnumerable<SpecialtySignUpViewDto>> response = await Mediator.Send(new GetAllSpecialtiesQuery());

        viewModel.Specialties = new SelectList(response.Value, nameof(SpecialtySignUpViewDto.Id), nameof(SpecialtySignUpViewDto.Name));
        return View(viewModel);
    }

    // [ValidateAntiForgeryToken]
    // [HttpPost(nameof(SignUp))]
    // public async Task<IActionResult> PostSignUp(SignUpViewModel viewModel, [FromServices] ISpecialtyReadOnlyRepository _repo)
    // {
    //     CreateUserCommand command = viewModel;
    //     Result response = await _mediator.Send(command);

    //     if (!response.IsSuccess)
    //     {
    //         GenerateErrorMessage(response.Error.Message);
    //         var validationError = response.Error as ValidationError;

    //         if(validationError != null)
    //         {
    //             TempData["EmailFailures"] = validationError!.ExtractValidationErrors("CreateUser.Email");
    //             TempData["PasswordFailures"] = validationError!.ExtractValidationErrors("CreateUser.Password");
    //             TempData["ConfirmPasswordFailures"] = validationError!.ExtractValidationErrors("CreateUser.ConfirmPassword");
    //             TempData["NameFailures"] = validationError!.ExtractValidationErrors("CreateUser.Name");
    //             TempData["ContactFailures"] = validationError!.ExtractValidationErrors("CreateUser.Contact");
    //             TempData["SpecialtyFailures"] = validationError!.ExtractValidationErrors("CreateUser.Specialty");
    //         }

    //         await LoadSpecialtiesAsync(viewModel, _repo);
    //         return View(nameof(SignUp), viewModel);
    //     }

    //     GenerateSuccessMessage(Constants.Messages.SignUpSuccess);
    //     return RedirectToAction(nameof(Login));
    // }

    // private static async Task LoadSpecialtiesAsync(SignUpViewModel viewModel, ISpecialtyReadOnlyRepository repo)
    // {
    //     var specialties = await repo.GetAllAsync();
    //     viewModel.Specialties = new SelectList(specialties, nameof(Specialty.Id), nameof(Specialty.Name));
    // }

    
    // [HttpGet(nameof(Login))]
    // public IActionResult Login(string returnUrl = null!)
    // {
    //     if(returnUrl != null)
    //     {
    //         GenerateErrorMessage(Constants.Messages.AccessDenied);
    //     }
    //     return View();
    // }

    // [HttpPost(nameof(Login))]
    // [ValidateAntiForgeryToken]
    // public async Task<IActionResult> Login(LoginViewModel viewModel, string returnUrl = null!)
    // {
    //     LoginUserCommand command = viewModel;
    //     Result response = await _mediator.Send(command);

    //     if(!response.IsSuccess)
    //     {
    //         ValidationError? validationError = response.Error as ValidationError;

    //         if(validationError != null)
    //         {
    //             TempData["EmailFailures"] = validationError!.ExtractValidationErrors("LoginUser.Email");
    //             TempData["PasswordFailures"] = validationError!.ExtractValidationErrors("LoginUser.Password");
    //         }
    //         else
    //         {
    //             GenerateErrorMessage(response.Error.Message);
    //         }
    //         return View();
    //     }

    //     Result<ClaimsIdentity>? valueResponse = response as Result<ClaimsIdentity> 
    //         ?? throw new ResultConvertionException();

    //     AuthenticationProperties authProperties = new();

    //     await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(valueResponse!.Value!), authProperties);
    //     if(Url.IsLocalUrl(returnUrl))
    //     {
    //         return Redirect(returnUrl);
    //     }
    //     return RedirectToAction(nameof(Index));
    // }

}
