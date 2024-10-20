namespace Classificador.Api.Presentation.Controllers;

public abstract class WebController<T> : Controller
{
    protected readonly ILogger<T> Logger;
    protected readonly IMediator Mediator;

    public WebController(ILogger<T> logger, IMediator mediator)
    {
        Logger = logger;
        Mediator = mediator;
    }

    protected virtual void GenerateSuccessMessage(string message)
    {
        GenerateMessage("success", message);
    }

    protected virtual void GenerateWarningMessage(string message)
    {
        GenerateMessage("warning", message);
    }

    protected virtual void GenerateErrorMessage(string message)
    {
        GenerateMessage("error", message);
    }

    private void GenerateMessage(string key, string value)
    {
        Dictionary<string, string> dict = [];
        dict.Add(key, value);
        TempData["Message"] = dict;
    }

}