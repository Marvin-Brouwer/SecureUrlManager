using Microsoft.AspNetCore.Mvc;

namespace SecureUrlManager.App.Features.Application;

public class ApplicationController : Controller
{
    private readonly UrlShortener _urlShortener;

    public ApplicationController(UrlShortener urlShortener)
    {
        _urlShortener = urlShortener;
    }

    // TODO, this page will have a big box to "shorten" or a big button to log in if you're not
    public IActionResult Index() => View();

    [HttpPost("shorten")]
    public IActionResult ApplyForShortening([FromForm] ShortenApplicationRequest request)
    {
        // TODO get from auth context
        var userId = "marvin.brouwer";

        // TODO validate request.

        var result = _urlShortener.Shorten(userId, request.Url);

        return result.Match(
            urlRecord => View("ApplicationSummary", urlRecord),
            error => View("ApplicationError", error)
        );
    }
}
