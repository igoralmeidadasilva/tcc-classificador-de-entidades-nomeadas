using Classificador.Api.Application.Queries.GetAllSpecialties;
using Classificador.Api.Domain;
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

        if(response.IsFailure)
        {
            GenerateErrorMessage(response.FirstError().Message);
            return View(viewModel);
        }

        viewModel.Specialties = new SelectList(response.Value, nameof(SpecialtySignUpViewDto.Id), nameof(SpecialtySignUpViewDto.Name));
        return View(viewModel);
    }

    [ValidateAntiForgeryToken]
    [HttpPost]
    public async Task<IActionResult> PostCreateUser(SignUpViewModel viewModel)
    {
        if (viewModel.CreateUserForm == null)
        {
            GenerateErrorMessage(Constants.Messages.InvalidForm);
            return RedirectToAction(nameof(SignUp));
        }

        CreateUserCommand command = viewModel.CreateUserForm;
        Result response = await Mediator.Send(command);

        if(response.IsFailure)
        {
            if(response.FirstErrorTypeOf(ErrorType.Conflict))
                TempData["EmailFailures"] = response.GetErrorsByCode("User.Email").ExtractErrorsMessages().ToList();
            
            if(response.FirstErrorTypeOf(ErrorType.Validation))
            {
                TempData["EmailFailures"] = response.GetErrorsByCode("CreateUser.Email").ExtractErrorsMessages().ToList();
                TempData["PasswordFailures"] = response.GetErrorsByCode("CreateUser.Password").ExtractErrorsMessages().ToList();
                TempData["ConfirmPasswordFailures"] = response.GetErrorsByCode("CreateUser.ConfirmPassword").ExtractErrorsMessages().ToList();
                TempData["NameFailures"] = response.GetErrorsByCode("CreateUser.Name").ExtractErrorsMessages().ToList();
                TempData["ContactFailures"] = response.GetErrorsByCode("CreateUser.Contact").ExtractErrorsMessages().ToList();
                TempData["SpecialtyFailures"] = response.GetErrorsByCode("CreateUser.Specialty").ExtractErrorsMessages().ToList();
            }

            return RedirectToAction(nameof(SignUp));
        }

        GenerateSuccessMessage(Constants.Messages.SignUpSuccess);
        return RedirectToAction(nameof(Login));
    }
    
    [HttpGet("login")]
    public IActionResult Login(string returnUrl = null!)
    {
        if(returnUrl != null)
        {
            GenerateErrorMessage(Constants.Messages.AccessDenied);
        }
        return View();
    }

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
