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

    [HttpGet("[action]/{prescribingInformationId}")]
    public async Task<IActionResult> ClassifyNamedEntity(ClassifyNamedEntityViewModel viewModel, string prescribingInformationId, int entityIndex)
    {
        var responseGetCategoriesQuery = await _mediator.Send(new GetCategoriesQuery()) as Result<IEnumerable<ClassifyNamedEntityViewCategoryDto>>
            ?? throw new InvalidOperationException("Error converting value from Result to ResultT");;
        
        var responseGetNamedEntityByPrescribingInformationId = 
            await _mediator.Send(new GetNamedEntityByPrescribingInformationIdQuery(prescribingInformationId)) as Result<List<ClassifyNamedEntityViewNamedEntityDto>>
                ?? throw new InvalidOperationException("Error converting value from Result to ResultT");

        viewModel.PrescribingInformationId = new Guid(prescribingInformationId);
        viewModel.Categories = responseGetCategoriesQuery.Value!.ToList();
        viewModel.NameEntityIndex = entityIndex;
       
        ViewData["NamedEntities"] = responseGetNamedEntityByPrescribingInformationId.Value;
        return View(viewModel);
    }

    [HttpPost("[action]/{prescribingInformationId}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ClassifyNamedEntity(ClassifyNamedEntityViewModel viewModel, string prescribingInformationId)
    {
        CreateClassificationCommand command = viewModel;
        Result response = await _mediator.Send(command);

        if (!response.IsSuccess)
        {
            GenerateErrorMessage(response.Error.Message);
            return RedirectToAction(nameof(ClassifyNamedEntity));
        }

        GenerateSuccessMessage(Constants.Messages.MessageClassificationSuccessfully);
        return RedirectToAction(nameof(ClassifyNamedEntity), viewModel);
    }

    [HttpPost(nameof(PatchClassificationToCompleted))]
    public async Task<IActionResult> PatchClassificationToCompleted(UpdateClassificationToCompletedCommand command)
    {
        var response = await _mediator.Send(command);

        if(!response.IsSuccess)
        {
            GenerateWarningMessage(response.Error.Message);
            // TODO: Mudar para ClassifyNamedEntity com um return url para ela
            return RedirectToAction(nameof(ChoosePrescribingInformation));
        }

        return RedirectToAction(nameof(ThanksForTheClassifications));
    }

    [HttpGet(nameof(ThanksForTheClassifications))]
    public IActionResult ThanksForTheClassifications()
    {
        return View();
    }

    [HttpGet(nameof(YourClassifications))]
    public IActionResult YourClassifications()
    {
        return View();
    }

}


