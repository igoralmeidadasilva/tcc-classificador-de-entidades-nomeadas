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
    public async Task<IActionResult> ChoosePrescribingInformation(
        ChoosePrescribingInformationViewModel viewModel, 
        [FromQuery] string prescribingInformationName = "")
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

        viewModel.PrescribingInformations = valueResponse.Value!.ToList();

        return View(viewModel);
    }

    [HttpGet("[action]/{idPrescribingInformation}")]
    public async Task<IActionResult> ClassifyNamedEntity(ClassifyNamedEntityViewModel viewModel, string idPrescribingInformation, int entityIndex)
    {
        var responseGetCategoriesQuery = await _mediator.Send(new GetCategoriesQuery()) as Result<IEnumerable<ClassifyNamedEntityViewCategoryDto>>
            ?? throw new InvalidOperationException("Error converting value from Result to ResultT");;
        
        var responseGetNamedEntity = 
            await _mediator.Send(new GetNamedEntityByPrescribingInformationIdQuery(idPrescribingInformation)) 
            as Result<List<ClassifyNamedEntityViewNamedEntityDto>> ?? throw new InvalidOperationException("Error converting value from Result to ResultT");

        // var responseGetUncompletedClassifications = await _mediator.Send();

        if(responseGetNamedEntity.Value!.Count == 0)
        {
            GenerateSuccessMessage(Constants.Messages.MessageClassificationIsDone);
            return RedirectToAction(nameof(ChoosePrescribingInformation));
        }

        viewModel.IdPrescribingInformation = new Guid(idPrescribingInformation);
        viewModel.Categories = responseGetCategoriesQuery.Value!.ToList();
        viewModel.NameEntityIndex = entityIndex;
       
        ViewData["NamedEntities"] = responseGetNamedEntity.Value;
        ViewData["UncompletedClassifications"] = "";
        ViewData["AllClassifications"] = "";
        ViewBag.ReturnUrl = Request.Path;
        
        return View(viewModel);
    }

    [HttpPost("[action]/{prescribingInformationId}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ClassifyNamedEntity(ClassifyNamedEntityViewModel viewModel, string prescribingInformationId)
    {
        CreateClassificationCommand command = viewModel;
        Result response = await _mediator.Send(command);
        viewModel.IdPrescribingInformation = new Guid(prescribingInformationId);

        if (!response.IsSuccess)
        {
            GenerateErrorMessage(response.Error.Message);
            return RedirectToAction(nameof(ClassifyNamedEntity));
        }

        GenerateSuccessMessage(Constants.Messages.MessageClassificationSuccessfully);
        return RedirectToAction(nameof(ClassifyNamedEntity), viewModel);
    }

    [HttpPost(nameof(PatchClassificationToCompleted))]
    public async Task<IActionResult> PatchClassificationToCompleted(UpdateClassificationToCompletedCommand command, string returnUrl = "")
    {
        var response = await _mediator.Send(command);
        if(!response.IsSuccess)
        {
            GenerateWarningMessage(response.Error.Message);
            ViewBag.ReturnUrl = returnUrl;
            return string.IsNullOrEmpty(returnUrl) 
                ? RedirectToAction(nameof(ChoosePrescribingInformation)) 
                : Redirect(returnUrl);
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


