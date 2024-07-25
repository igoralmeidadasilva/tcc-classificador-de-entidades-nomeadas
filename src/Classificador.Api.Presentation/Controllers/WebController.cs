namespace Classificador.Api.Presentation.Controllers;

public abstract class WebController<T> : Controller
{
    protected readonly ILogger<T> _logger;
    protected readonly IMediator _mediator;

    public WebController(ILogger<T> logger, IMediator mediator)
    {
        _logger = logger;
        _mediator = mediator;
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