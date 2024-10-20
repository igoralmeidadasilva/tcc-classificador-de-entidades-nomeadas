// namespace Classificador.Api.Presentation.Controllers;

// [Route("[controller]")]
// [Authorize(Roles = nameof(UserRole.Admin))]
// public sealed class AdminController(ILogger<AdminController> logger, IMediator mediator) : WebController<AdminController>(logger, mediator)
// {
//     [HttpGet(nameof(AdminPanel))]
//     public IActionResult AdminPanel()
//     {
//         return View();
//     }

//     [HttpGet(nameof(CreatePrescribingInformation))]
//     public IActionResult CreatePrescribingInformation(CreatePrescribingInformationViewModel viewModel)
//     {
//         return View(viewModel);
//     }

//     [HttpPost(nameof(PostCreatePrescribingInformation))]
//     public async Task<IActionResult> PostCreatePrescribingInformation(CreatePrescribingInformationViewModel viewModel)
//     {
//         CreatePrescribingInformationTxtCommand command = viewModel;

//         Result response = await _mediator.Send(command);


//         if(!response.IsSuccess)
//         {
//             GenerateErrorMessage(response.Error.Message);
//             var validationError = response.Error as ValidationError;

//             if (validationError != null)
//             {
//                 TempData["FileFailures"] = validationError!.ExtractValidationErrors("CreatePrescribingInformationTxt.File");
//             }

//             return View(nameof(CreatePrescribingInformation));
//         }

//         GenerateSuccessMessage(Constants.Messages.CreatePrescribingInformationSuccessfully);
//         return View(nameof(CreatePrescribingInformation));
//     }
// }