namespace Classificador.Api.Presentation.Controllers;

[Route("[controller]")]
public sealed class HomeController : Controller
{

    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/")]
    public IActionResult Index()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [HttpGet(nameof(Error))]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    [HttpGet(nameof(PageNotFound))]
    public IActionResult PageNotFound()
    {
        return View();
    }

    [HttpGet(nameof(About))]
    public IActionResult About()
    {
        return View();
    }

    [HttpGet(nameof(Contact))]
    public IActionResult Contact()
    {
        return View();
    }

    [HttpGet(nameof(SignUp))]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpGet(nameof(Login))]
    public IActionResult Login()
    {
        return View();
    }

    [HttpGet(nameof(Classifications))]
    public IActionResult Classifications()
    {
        return View();
    }

}