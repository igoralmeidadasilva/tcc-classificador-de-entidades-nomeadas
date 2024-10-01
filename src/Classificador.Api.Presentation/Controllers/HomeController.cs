using System.Text;
using Classificador.Api.Application.Queries.GetDownloadSpacyModel;
using Classificador.Api.Application.Queries.GetGetPrescribingInformation;
using Newtonsoft.Json;

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
    public async Task<IActionResult> Classifications(ClassificationsViewModel viewModel)
    {
        var response = await _mediator.Send(new GetPrescribingInformationQuery());

        if(!response.IsSuccess)
        {
            GenerateErrorMessage(response.Error.Message);
            return View(viewModel);
        }

        Result<List<PrescribingInformationClassificationViewDto>> valueResponse = response as  Result<List<PrescribingInformationClassificationViewDto>>
            ?? throw new ResultConvertionException(); 
        
        viewModel.PrescribingInformations = valueResponse.Value;

        return View(viewModel);
    }

    [HttpGet(nameof(PrecribingInformationClassifications))]
    public async Task<IActionResult> PrecribingInformationClassifications(
        PrecribingInformationClassificationsViewModel viewModel, 
        string idPrescribingInformation, 
        string namePrescribingInformation)
    {
        viewModel.IdPrescribingInformation = new Guid(idPrescribingInformation);
        viewModel.NamePrescribingInformation = namePrescribingInformation;
        
        var response = await _mediator.Send(new GetAllClassificationByVotesQuery(idPrescribingInformation));

        if(!response.IsSuccess)
        {
            GenerateErrorMessage(response.Error.Message);
            return View(nameof(Classifications));
        }

        var responseValue = response as Result<List<CountVoteForNamedEntity>> 
            ?? throw new ResultConvertionException();

        viewModel.Classifications = responseValue.Value!;

        return View(viewModel);
    }

    [HttpGet(nameof(DownloadClassificationSpacyModel))]
    public async Task<IActionResult> DownloadClassificationSpacyModel(string idPrescribingInformation, string namePrescribingInformation)
    {
        var response = await _mediator.Send(new GetDownloadSpacyModelQuery(idPrescribingInformation, namePrescribingInformation));

        if(!response.IsSuccess)
        {
            GenerateErrorMessage(response.Error.Message);
            return View(nameof(Classifications));
        }

        var responseValue = response as Result<GetDownloadSpacyModelQueryResponse> 
            ?? throw new ResultConvertionException(); 
        
        var serializeEntities = JsonConvert.SerializeObject(responseValue.Value, Formatting.Indented);

        byte[] bytes = Encoding.UTF8.GetBytes(serializeEntities);
        return File(bytes, "text/plain", $"Taggers.{namePrescribingInformation}.json");
    }

    [HttpGet(nameof(SignUp))]
    public async Task<IActionResult> SignUp(SignUpViewModel viewModel, [FromServices]ISpecialtyReadOnlyRepository _repo)
    {
        await LoadSpecialtiesAsync(viewModel, _repo);
        return View(viewModel);
    }

    [ValidateAntiForgeryToken]
    [HttpPost(nameof(SignUp))]
    public async Task<IActionResult> PostSignUp(SignUpViewModel viewModel, [FromServices] ISpecialtyReadOnlyRepository _repo)
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
                TempData["SpecialtyFailures"] = validationError!.ExtractValidationErrors("CreateUser.Specialty");
            }

            await LoadSpecialtiesAsync(viewModel, _repo);
            return View(nameof(SignUp), viewModel);
        }

        GenerateSuccessMessage(Constants.Messages.SignUpSuccess);
        return RedirectToAction(nameof(Login));
    }

    private static async Task LoadSpecialtiesAsync(SignUpViewModel viewModel, ISpecialtyReadOnlyRepository repo)
    {
        var specialties = await repo.GetAllAsync();
        viewModel.Specialties = new SelectList(specialties, nameof(Specialty.Id), nameof(Specialty.Name));
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
            ValidationError? validationError = response.Error as ValidationError;

            if(validationError != null)
            {
                TempData["EmailFailures"] = validationError!.ExtractValidationErrors("LoginUser.Email");
                TempData["PasswordFailures"] = validationError!.ExtractValidationErrors("LoginUser.Password");
            }
            else
            {
                GenerateErrorMessage(response.Error.Message);
            }
            return View();
        }

        Result<ClaimsIdentity>? valueResponse = response as Result<ClaimsIdentity> 
            ?? throw new ResultConvertionException();

        AuthenticationProperties authProperties = new();

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(valueResponse!.Value!), authProperties);
        if(Url.IsLocalUrl(returnUrl))
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
        viewModel.EmailToContact = option.EmailAddress!;
        return View(viewModel);
    }  

    [HttpPost(nameof(Contact))]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PostSendEmailToContact(ContactViewModel viewModel, [FromServices] IOptions<EmailOptions> emailOptions)
    {
        SendEmailToContactCommand command = viewModel;
        Result response = await _mediator.Send(command);

        var option = emailOptions.Value;
        viewModel.EmailToContact = option.EmailAddress!;

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

            return RedirectToAction(nameof(Contact), viewModel);
        }

        GenerateSuccessMessage(Constants.Messages.SendSuccessfully);
        return RedirectToAction(nameof(Contact), viewModel);
    } 

    [HttpGet(nameof(WhereToStart))]
    public IActionResult WhereToStart()
    {
        return View();
    }

}