using Classificador.Api.Application.Queries.GetAllSpecialties;
using Classificador.Api.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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

    [HttpPost(nameof(PostCreateUser))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PostCreateUser(SignUpViewModel viewModel)
    {
        if (viewModel.CreateUserForm is null)
        {
            GenerateErrorMessage(Constants.Messages.InvalidForm);
            return RedirectToAction(nameof(SignUp));
        }

        Result response = await Mediator.Send(viewModel.CreateUserForm.ToCommand());

        if(response.IsFailure)
        {
            if(response.FirstErrorTypeOf(ErrorType.Conflict))
            {
                TempData["EmailFailures"] = response.GetErrorsByCode("User.Email").ExtractErrorsMessages().ToList();
                return RedirectToAction(nameof(SignUp));
            }
        
            TempData["EmailFailures"] = response.GetErrorsByCode("CreateUser.Email").ExtractErrorsMessages().ToList();
            TempData["PasswordFailures"] = response.GetErrorsByCode("CreateUser.Password").ExtractErrorsMessages().ToList();
            TempData["ConfirmPasswordFailures"] = response.GetErrorsByCode("CreateUser.ConfirmPassword").ExtractErrorsMessages().ToList();
            TempData["NameFailures"] = response.GetErrorsByCode("CreateUser.Name").ExtractErrorsMessages().ToList();
            TempData["ContactFailures"] = response.GetErrorsByCode("CreateUser.Contact").ExtractErrorsMessages().ToList();
            TempData["SpecialtyFailures"] = response.GetErrorsByCode("CreateUser.Specialty").ExtractErrorsMessages().ToList();
            
            return RedirectToAction(nameof(SignUp));
        }

        GenerateSuccessMessage(Constants.Messages.SignUpSuccess);
        return RedirectToAction(nameof(Login));
    }
    
    [HttpGet("login")]
    public IActionResult Login(string returnUrl = "") => View();
    
    [HttpPost(nameof(Login))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel viewModel, string returnUrl = "")
    {
        if (viewModel is null)
        {
            GenerateErrorMessage(Constants.Messages.InvalidForm);
            return RedirectToAction(nameof(Login));
        }

        Result<ClaimsIdentity> response = await Mediator.Send(viewModel.ToCommand());

        if(response.IsFailure)
        {
            if(response.FirstErrorTypeOf(ErrorType.NotFound))
            {
                TempData["EmailFailures"] = response.GetErrorsByCode("User.NotFound").ExtractErrorsMessages().ToList();
                return View();
            }

            if(response.FirstErrorTypeOf(ErrorType.Unauthorized))
            {
                TempData["PasswordFailures"] = response.GetErrorsByCode("User.AuthenticationPassword").ExtractErrorsMessages().ToList();
                return View();
            }

            TempData["EmailFailures"] = response.GetErrorsByCode("LoginUser.Email").ExtractErrorsMessages().ToList();
            TempData["PasswordFailures"] =  response.GetErrorsByCode("LoginUser.Password").ExtractErrorsMessages().ToList();
            return View();
        }
        AuthenticationProperties authProperties = new();
        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Value!), authProperties);
        
        return RedirectToAction("Index", "Home");
    }
}
