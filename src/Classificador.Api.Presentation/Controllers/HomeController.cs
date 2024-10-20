using System.Text;
using Classificador.Api.Application.Commands.SendEmailToAdmins;
using Classificador.Api.Application.Queries.ClassifiedPrescribingInformation;
using Classificador.Api.Application.Queries.DownloadSpacyModel;
using Classificador.Api.Application.Queries.GetAllClassificationByVotes;
using Classificador.Api.SharedKernel.Shared.Results;
using Newtonsoft.Json;

namespace Classificador.Api.Presentation.Controllers;

[AllowAnonymous]
public sealed class HomeController : WebController<HomeController>
{
    public HomeController(ILogger<HomeController> logger, IMediator mediator) : base(logger, mediator)
    { }

    [HttpGet("/")]
    public IActionResult Index() => View();

    [HttpGet("/error")]
    public IActionResult Error() => View();

    [HttpGet("/page-not-found")]
    public IActionResult PageNotFound() => View();

    [HttpGet("/access-denied")]
    public IActionResult AccessDenied() => View();

    [HttpGet("/about")]
    public IActionResult About() => View();

    [HttpGet("/where-to-start")]
    public IActionResult WhereToStart() => View();

    [HttpGet("/contact")]
    public IActionResult Contact() => View();

    [HttpPost(nameof(PostSendEmailToAdmin))]
    public async Task<IActionResult> PostSendEmailToAdmin(SendEmailToAdminsCommand command)
    {
        Result response = await Mediator.Send(command);

        if(response.IsFailure)
        {
            TempData["ContactNameFailures"] = response.GetErrorsByCode("SendEmailToAdmins.ContactName").ExtractErrorsMessages().ToList();
            TempData["EmailForContactFailures"] = response.GetErrorsByCode("SendEmailToAdmins.EmailForContact").ExtractErrorsMessages().ToList();
            TempData["MessageSubjectFailures"] = response.GetErrorsByCode("SendEmailToAdmins.MessageSubject").ExtractErrorsMessages().ToList();
            TempData["MessageBodyFailures"] = response.GetErrorsByCode("SendEmailToAdmins.MessageBody").ExtractErrorsMessages().ToList();

            return RedirectToAction(nameof(Contact));
        }

        GenerateSuccessMessage(Domain.Constants.SuccessMessages.SendSuccessfully);
        return RedirectToAction(nameof(Contact));
    }

    [HttpGet("/classified-prescribing-information")]
    public async Task<IActionResult> ClassifiedPrescribingInformation(ClassifiedPrescribingInformationViewModel viewModel)
    {
        Result<IEnumerable<PrescribingInformationClassifiedDto>> response = await Mediator.Send(new ClassifiedPrescribingInformationQuery());

        if(response.IsFailure)
        {
            GenerateWarningMessage(response.FirstError().Message);
            return View(viewModel);
        }
        
        viewModel.PrescribingInformations = response.Value;

        return View(viewModel);
    }

    [HttpGet("/classified-prescribing-information/{idPrescribingInformation:guid}")]
    public async Task<IActionResult> ClassifiedPrescribingInformationDetails(
        ClassifiedPrescribingInformationDetailsViewModel viewModel, 
        Guid idPrescribingInformation)
    {
        Result<GetAllClassificationByVotesQueryResponse> response = await Mediator.Send(new GetAllClassificationByVotesQuery(idPrescribingInformation));

        if(response.IsFailure)
        {
            GenerateWarningMessage(response.FirstError().Message);
            return RedirectToAction(nameof(ClassifiedPrescribingInformation));
        }

        viewModel.NamePrescribingInformation = response.Value!.Name;
        viewModel.IdPrescribingInformation = idPrescribingInformation;
        viewModel.Classifications = response.Value!.Classifications;

        return View(viewModel);
    }

    [HttpGet(nameof(DownloadClassificationSpacyModel))]
    public async Task<IActionResult> DownloadClassificationSpacyModel(Guid idPrescribingInformation)
    {
        Result<DownloadSpacyModelQueryResponse> response = await Mediator.Send(new DownloadSpacyModelQuery(idPrescribingInformation));

        if(!response.IsSuccess)
        {
            GenerateWarningMessage(response.FirstError().Message);
            return RedirectToAction(nameof(ClassifiedPrescribingInformation));
        }

        string serializeEntities = JsonConvert.SerializeObject(response.Value, Formatting.Indented);

        byte[] bytes = Encoding.UTF8.GetBytes(serializeEntities);
        return File(bytes, "text/plain", $"Taggers.{response.Value!.Name}.json");
    }
}