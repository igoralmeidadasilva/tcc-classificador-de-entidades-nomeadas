namespace Classificador.Api.Presentation.Controllers;

[Route("[controller]")]
[AllowAnonymous]
public sealed class HomeController : WebController<HomeController>
{
    public HomeController(ILogger<HomeController> logger, IMediator mediator) : base(logger, mediator)
    { }

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

    [HttpGet(nameof(AccessDenied))]
    public IActionResult AccessDenied()
    {
        return View();
    }

    [HttpGet(nameof(Classifications))]
    public IActionResult Classifications()
    {
        return View();
    }

    [HttpGet(nameof(SignUp))]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel,[FromServices]ISpecialtyReadOnlyRepository _repo)
    {
        viewModel.Specialties = new SelectList(await _repo.GetAllAsync(), nameof(Specialty.Id), nameof(Specialty.Name));
        return View(viewModel);
    }

    [HttpPost(nameof(SignUp))]
    [ValidateAntiForgeryToken]
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
                TempData["SpecialtyFailures"] = validationError!.ExtractValidationErrors("CreateUser.IdSpecialty");
            }

            return View();
        }

        GenerateSuccessMessage(Constants.Messages.SignUpSuccess);
        return RedirectToAction(nameof(Login));
    }

    
    [HttpGet(nameof(Login))]
    public IActionResult Login(string returnUrl = null!)
    {
        if(returnUrl != null)
        {
            GenerateErrorMessage(Constants.Messages.AccessDenied);
        }
        return View();
    }

    [HttpPost(nameof(Login))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel viewModel, string returnUrl = null!)
    {
        LoginUserCommand command = viewModel;
        Result response = await _mediator.Send(command);

        if(!response.IsSuccess)
        {
            GenerateErrorMessage(response.Error.Message);
            ValidationError? validationError = response.Error as ValidationError;

            if(validationError != null)
            {
                TempData["EmailFailures"] = validationError!.ExtractValidationErrors("LoginUser.Email");
                TempData["PasswordFailures"] = validationError!.ExtractValidationErrors("LoginUser.Password");
            }
            return View();
        }

        Result<ClaimsIdentity>? valueResponse = response as Result<ClaimsIdentity> 
            ?? throw new ResultConvertionException();

        AuthenticationProperties authProperties = new();

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(valueResponse!.Value!), authProperties);
        if (Url.IsLocalUrl(returnUrl))
        {
            return Redirect(returnUrl);
        }
        return RedirectToAction(nameof(Index));
    }

    [HttpGet(nameof(About))]
    public IActionResult About()
    {
        return View();
    }

    [HttpGet(nameof(Contact))]
    public IActionResult Contact(ContactViewModel viewModel, [FromServices] IOptions<EmailOptions> emailOptions)
    {
        var option = emailOptions.Value;
        ViewBag.ContactEmail = option.EmailAddress;
        return View(viewModel);
    }  

    [HttpPost(nameof(Contact))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Contact(ContactViewModel viewModel)
    {
        SendEmailToContactCommand command = viewModel;
        Result response = await _mediator.Send(command);

        if(!response.IsSuccess)
        {
            GenerateErrorMessage(response.Error.Message);
            ValidationError? validationError = response.Error as ValidationError;

            if(validationError != null)
            {
                TempData["NameFailures"] = validationError!.ExtractValidationErrors("SendEmailToContact.Name");
                TempData["EmailFailures"] = validationError!.ExtractValidationErrors("SendEmailToContact.Email");
                TempData["SubjectFailures"] = validationError!.ExtractValidationErrors("SendEmailToContact.Subject");
                TempData["MessageFailures"] = validationError!.ExtractValidationErrors("SendEmailToContact.Message");
            }

            return View(viewModel);
        }

        GenerateSuccessMessage(Constants.Messages.MessageSendSuccessfully);
        return View(viewModel);
    } 

    [HttpGet(nameof(WhereToStart))]
    public IActionResult WhereToStart()
    {
        return View();
    }

}