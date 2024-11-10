using Classificador.Api.Application.Queries.GetPrescribingInformationById;
using Classificador.Api.Domain.Core.Enums;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

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

    [HttpGet("choose-prescribing-information")]
    public async Task<IActionResult> ChoosePrescribingInformation(
        ChoosePrescribingInformationViewModel viewModel)
    {
        Guid idUser = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        Result<IEnumerable<ChoosePrescribingInformationViewDto>> response = 
            await Mediator.Send(new GetPrescribingInformationByIdQuery(viewModel.SearchTerm, idUser));
        
        if(response.IsFailure)
        {
            GenerateErrorMessage(response.FirstError().Message);
            return View();
        }     

        viewModel.PrescribingInformations = response.Value;
        return View(viewModel);
    }

    // [HttpGet("[action]/{idPrescribingInformation}")]
    // public async Task<IActionResult> ClassifyNamedEntity(
    //     ClassifyNamedEntityViewModel viewModel, 
    //     string idPrescribingInformation, 
    //     string namePrescribingInformation, 
    //     int entityIndex)
    // {
    //     viewModel.NamePrescribingInformation = namePrescribingInformation;
    //     string idUser = User.FindFirstValue(ClaimTypes.NameIdentifier)!;

    //     var responseGetCategoriesQuery = await _mediator.Send(new GetCategoriesQuery());
    //     if(!responseGetCategoriesQuery.IsSuccess)
    //     {
    //         GenerateErrorMessage(responseGetCategoriesQuery.Error.Message);
    //         return RedirectToAction(nameof(ChoosePrescribingInformation));
    //     }
    //     var valueOfGetCategoriesQuery = responseGetCategoriesQuery as Result<IEnumerable<ClassifyNamedEntityViewCategoryDto>> 
    //         ?? throw new ResultConvertionException();

    //     var responseGetNamedEntity = await _mediator.Send(new GetNamedEntityByPrescribingInformationIdQuery(idPrescribingInformation, idUser));
    //     if(!responseGetNamedEntity.IsSuccess)
    //     {
    //         GenerateErrorMessage(responseGetNamedEntity.Error.Message);
    //         return RedirectToAction(nameof(ChoosePrescribingInformation));
    //     }
    //     var valueOfGetNamedEntity = responseGetNamedEntity as Result<List<ClassifyNamedEntityViewNamedEntityDto>> 
    //         ?? throw new ResultConvertionException();

    //     var responseGetPendingClassifications = await _mediator.Send(new GetPendingClassificationsQuery(idUser, idPrescribingInformation));
    //     if(!responseGetPendingClassifications.IsSuccess)
    //     {
    //         GenerateErrorMessage(responseGetPendingClassifications.Error.Message);
    //         return RedirectToAction(nameof(ChoosePrescribingInformation));
    //     }
    //     var valueOfGetPendingClassifications = responseGetPendingClassifications as Result<List<ClassifyNamedEntityViewPendingClassificationDto>> 
    //         ?? throw new ResultConvertionException();

    //     viewModel.IdPrescribingInformation = new Guid(idPrescribingInformation);
    //     viewModel.Categories = valueOfGetCategoriesQuery.Value!.ToList();
    //     viewModel.NamedEntityIndex = entityIndex;

    //     ViewData["NamedEntitiesList"] = valueOfGetNamedEntity.Value;
    //     ViewData["PendingClassificationsList"] = valueOfGetPendingClassifications.Value;
    //     ViewBag.ReturnUrl = Request.Path + Request.QueryString;

    //     return View(viewModel);
    // }

    // [HttpPost(nameof(PostCreateClassification))]
    // public async Task<IActionResult> PostCreateClassification(
    //     CreateClassificationViewModel viewModel, 
    //     string idPrescribingInformation, 
    //     string namePrescribingInformation)
    // {
    //     CreateClassificationCommand command = viewModel;

    //     ClassifyNamedEntityViewModel classifyNamedEntityViewModel = new()
    //     {
    //         IdPrescribingInformation = new Guid(idPrescribingInformation),
    //         NamePrescribingInformation = namePrescribingInformation
    //     };

    //     Result response = await _mediator.Send(command);

    //     if (!response.IsSuccess)
    //     {
    //         GenerateErrorMessage(response.Error.Message);
    //         return RedirectToAction(nameof(ClassifyNamedEntity), classifyNamedEntityViewModel);
    //     }

    //     GenerateSuccessMessage(Constants.Messages.ClassificationSuccessfully);
    //     return RedirectToAction(nameof(ClassifyNamedEntity), classifyNamedEntityViewModel);
    // }
        
    // [HttpPost(nameof(PostUpdateClassificationToCompleted))]
    // public async Task<IActionResult> PostUpdateClassificationToCompleted(PatchClassificationToCompletedViewModel viewModel, string returnUrl = "")
    // {
    //     UpdateClassificationToCompletedCommand command = viewModel;

    //     Result response = await _mediator.Send(command);

    //     if(!response.IsSuccess)
    //     {
    //         GenerateWarningMessage(response.Error.Message);

    //         return string.IsNullOrEmpty(returnUrl) 
    //             ? RedirectToAction(nameof(ChoosePrescribingInformation)) 
    //             : Redirect(returnUrl);
    //     }

    //     return RedirectToAction(nameof(ThanksForTheClassifications));
    // }

    // [HttpPost(nameof(PostDeletePendingClassification))]
    // public async Task<IActionResult> PostDeletePendingClassification(
    //     DeletePendingClassificationViewModel viewModel,
    //     string idPrescribingInformation, 
    //     string namePrescribingInformation)
    // {
    //     DeletePendingClassificationCommand command = viewModel;

    //     ClassifyNamedEntityViewModel classifyNamedEntityViewModel = new()
    //     {
    //         IdPrescribingInformation = new Guid(idPrescribingInformation),
    //         NamePrescribingInformation = namePrescribingInformation
    //     };

    //     Result response = await _mediator.Send(command);

    //     if(!response.IsSuccess)
    //     {
    //         GenerateErrorMessage(response.Error.Message);
    //         return RedirectToAction(nameof(ClassifyNamedEntity), classifyNamedEntityViewModel);
    //     }

    //     GenerateSuccessMessage(Constants.Messages.DeletePendingClassificationSuccessfully);
    //     return RedirectToAction(nameof(ClassifyNamedEntity), classifyNamedEntityViewModel);
    // }

    // [HttpGet(nameof(ThanksForTheClassifications))]
    // public IActionResult ThanksForTheClassifications()
    // {
    //     return View();
    // }

    // [HttpGet(nameof(YourClassifications))]
    // public IActionResult YourClassifications()
    // {
    //     return View();
    // }

}


