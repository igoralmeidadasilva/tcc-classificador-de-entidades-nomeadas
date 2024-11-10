using Classificador.Api.Domain;
using Classificador.Api.Domain.Core.Enums;

namespace Classificador.Api.Presentation.Controllers;

[Route("[controller]")]
[Authorize(Roles = nameof(UserRole.Admin))]
public sealed class AdminController(ILogger<AdminController> logger, IMediator mediator) : WebController<AdminController>(logger, mediator)
{
    [HttpGet("admin-panel")]
    public IActionResult AdminPanel() => View();

    [HttpGet("create-prescribing-information")]
    public IActionResult CreatePrescribingInformation(CreatePrescribingInformationViewModel viewModel) => View(viewModel);

    [HttpPost(nameof(PostCreatePrescribingInformation))]
    public async Task<IActionResult> PostCreatePrescribingInformation(CreatePrescribingInformationViewModel viewModel)
    {
        Result response = await Mediator.Send(viewModel.ToCommand());

        if(response.IsFailure)
        {
            if(response.FirstErrorTypeOf(ErrorType.Conflict))
            {
                GenerateErrorMessage(response.FirstError().Message);
                return View(nameof(CreatePrescribingInformation));
            }
            TempData["FileFailures"] = response.GetErrorsByCode("CreatePrescribingInformationTxt.File").ExtractErrorsMessages().ToList();

            return View(nameof(CreatePrescribingInformation));
        }

        GenerateSuccessMessage(Constants.Messages.CreatePrescribingInformationSuccessfully);
        return View(nameof(CreatePrescribingInformation));
    }
}