namespace Classificador.Api.Presentation.Controllers;

[Route("[controller]")]
[Authorize(Roles = $"{nameof(UserRole.Padrao)},{nameof(UserRole.Admin)}")]

public sealed class UserController(ILogger<UserController> logger, IMediator mediator) : WebController<UserController>(logger, mediator)
{
    [HttpPost(nameof(Logout))]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login", "Home");
    }

    [HttpGet("[action]/{prescribingInformationId}")] 
    public async Task<IActionResult> ClassifyNamedEntity(string prescribingInformationId)
    {
        var response = await _mediator.Send(new GetCategoriesQuery());

        if(!response.IsSuccess)
        {
            GenerateErrorMessage(response.Error.Message);
            return View();
        }

        var valueResponse = response as Result<IEnumerable<ClassifyNamedEntityViewCategoryDto>>
            ?? throw new InvalidOperationException("Error converting value from Result to ResultT");;

        var classifyNamedEntityViewModel = new ClassifyNamedEntityViewModel
        {
            PrescribingInformationIdId = new Guid(prescribingInformationId),
            Categories = valueResponse.Value!.ToList()
        };
        
        return View(classifyNamedEntityViewModel);
    }

    [HttpGet(nameof(ChoosePrescribingInformation))] 
    public async Task<IActionResult> ChoosePrescribingInformation([FromQuery] string prescribingInformationName = "")
    {
        var response = await _mediator.Send(new GetPrescribingInformationQuery
        {
            PrescribingInformationName = prescribingInformationName
        }); 
        
        if(!response.IsSuccess)
        {
            GenerateErrorMessage(response.Error.Message);
            return View();
        }     

        var valueResponse = response as Result<IEnumerable<ChoosePrescribingInformationViewDto>>
            ?? throw new InvalidOperationException("Error converting value from Result to ResultT");

        var choosePrescribingInformationViewModel = new ChoosePrescribingInformationViewModel
        {
            PrescribingInformations = valueResponse.Value!.ToList()
        };

        return View(choosePrescribingInformationViewModel);
    }
}
