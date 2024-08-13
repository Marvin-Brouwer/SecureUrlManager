using Microsoft.AspNetCore.Mvc;
using SecureUrlManager.App.Features.Application;

namespace SecureUrlManager.App.Features.OpenGraph;

public class OpenGraphController : Controller
{
    private readonly UrlShortener _urlShortener;

    public OpenGraphController(UrlShortener urlShortener)
    {
        _urlShortener = urlShortener;
    }

    // TODO, this page will have a big box to "shorten" or a big button to log in if you're not
    public IActionResult Index() => View();

    [HttpPost("og-display")]
    public async Task<IActionResult> Index([FromForm] OpenGraphRequest request)
    {
        // todo vaidate request
        try
        {
            var graph = await OpenGraphNet.OpenGraph.ParseUrlAsync(request.Url);
            return View("Index", graph);
        }
        catch
        {
            return BadRequest();
        }
    }
}
