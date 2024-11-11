using Classificador.Api.Application.Queries.GetAllCategories;
using Classificador.Api.Application.Queries.GetNamedEntityByPrescribingInformationId;
using Classificador.Api.Application.Queries.GetPendingClassifications;
using Classificador.Api.Application.Queries.GetPrescribingInformationById;
using Classificador.Api.Domain;
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
        return RedirectToAction("Login", "Auth");
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

    [HttpGet("classify-named-entity/{idPrescribingInformation:guid}")]
    public async Task<IActionResult> ClassifyNamedEntity(
        ClassifyNamedEntityViewModel viewModel,
        Guid idPrescribingInformation,
        int entityIndex = 0)
    {
        viewModel.NamedEntityIndex = entityIndex;
        viewModel.IdPrescribingInformation = idPrescribingInformation;
        Guid idUser = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        bool[] isAllLoadSuccess = await Task.WhenAll(
            LoadCategories(viewModel),
            LoadNamedEntities(viewModel, idUser),
            LoadPendingClassification(viewModel, idUser)
        );

        if (!isAllLoadSuccess.All(success => success))
        {
            return RedirectToAction(nameof(ChoosePrescribingInformation));
        }
        
        return View(viewModel);
    }

    private async Task<bool> LoadCategories(ClassifyNamedEntityViewModel viewModel)
    {
        Result<IEnumerable<ClassifyNamedEntityViewCategoryDto>> response = await Mediator.Send(new GetAllCategoriesQuery());
        if(response.IsFailure)
        {
            GenerateErrorMessage(response.FirstError().Message);
            return response.IsSuccess;
        }
        viewModel.Categories = response.Value!.ToList();
        return response.IsSuccess;
    }

    private async Task<bool> LoadNamedEntities(ClassifyNamedEntityViewModel viewModel, Guid idUser)
    {
        Result<IEnumerable<ClassifyNamedEntityViewNamedEntityDto>> response = 
            await Mediator.Send(new GetNamedEntityByPrescribingInformationIdQuery(viewModel.IdPrescribingInformation, idUser));
        if(response.IsFailure)
        {
            GenerateErrorMessage(response.FirstError().Message);
            return response.IsSuccess;
        }
        viewModel.NamedEntities = response.Value!.ToList();
        return response.IsSuccess;
    }

    private async Task<bool> LoadPendingClassification(ClassifyNamedEntityViewModel viewModel, Guid idUser)
    {
        Result<IEnumerable<ClassifyNamedEntityViewPendingClassificationDto>> response = 
            await Mediator.Send(new GetPendingClassificationsQuery(idUser, viewModel.IdPrescribingInformation));
        if(response.IsFailure)
        {
            GenerateErrorMessage(response.FirstError().Message);
            return response.IsSuccess;
        }
        viewModel.PendingClassifications = response.Value!.ToList();
        return response.IsSuccess;
    }

    [HttpPost(nameof(PostCreateClassification))]
    public async Task<IActionResult> PostCreateClassification(
        CreateClassificationForm form, 
        Guid idPrescribingInformation)
    {
        ClassifyNamedEntityViewModel viewModel = new()
        {
            IdPrescribingInformation = idPrescribingInformation
        };
        Result response = await Mediator.Send(form.ToCommand());

        if (response.IsFailure)
        {
            GenerateErrorMessage(response.FirstError().Message);
            return RedirectToAction(nameof(ClassifyNamedEntity), viewModel);
        }

        GenerateSuccessMessage(Constants.Messages.ClassificationSuccessfully);
        return RedirectToAction(nameof(ClassifyNamedEntity), viewModel);
    }

    [HttpPost(nameof(PostDeletePendingClassification))]
    public async Task<IActionResult> PostDeletePendingClassification(
        DeletePendingClassificationForm form,
        Guid idPrescribingInformation, 
        string namePrescribingInformation)
    {
        ClassifyNamedEntityViewModel viewModel = new()
        {
            IdPrescribingInformation = idPrescribingInformation
        };

        Result response = await Mediator.Send(form.ToCommand());

        if(response.IsFailure)
        {
            GenerateErrorMessage(response.FirstError().Message);
            return RedirectToAction(nameof(ClassifyNamedEntity), viewModel);
        }

        GenerateSuccessMessage(Constants.Messages.DeletePendingClassificationSuccessfully);
        return RedirectToAction(nameof(ClassifyNamedEntity), viewModel);
    }
        
    [HttpPost(nameof(PostUpdateClassificationToCompleted))]
    public async Task<IActionResult> PostUpdateClassificationToCompleted(PatchClassificationToCompletedForm form, Guid idPrescribingInformation)
    {
        Result response = await Mediator.Send(form.ToCommand());

        ClassifyNamedEntityViewModel viewModel = new()
        {
            IdPrescribingInformation = idPrescribingInformation
        };

        if(response.IsFailure)
        {
            GenerateWarningMessage(response.FirstError().Message);
            return RedirectToAction(nameof(ClassifyNamedEntity), viewModel);
        }

        return RedirectToAction(nameof(ThanksForTheClassifications));
    }

    [HttpGet("thanks-for-the-classifications")]
    public IActionResult ThanksForTheClassifications() => View();
    
    // [HttpGet(nameof(YourClassifications))]
    // public IActionResult YourClassifications()
    // {
    //     return View();
    // }

}


