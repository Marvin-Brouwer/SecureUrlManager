using Microsoft.AspNetCore.Mvc;

namespace SecureUrlManager.App.Features.Registration;

public class RegistrationController : Controller
{
    private readonly UrlShortener _urlShortener;

    public RegistrationController(UrlShortener urlShortener)
    {
        _urlShortener = urlShortener;
    }

    // TODO, this page will have a big box to "shorten" or a big button to log in if you're not
    public IActionResult Index() => View();

    [HttpPost("shorten")]
    public IActionResult ApplyForShortening([FromForm] ShortenUrlRequest request)
    {
        // TODO get from auth context
        var userId = "marvin.brouwer";

        // TODO validate request.

        var result = _urlShortener.Shorten(userId, request.Url);

        return result.Match(
            urlRecord => View("RegistrationSummary", urlRecord),
            error => View("RegistrationError", error)
        );
    }
}
