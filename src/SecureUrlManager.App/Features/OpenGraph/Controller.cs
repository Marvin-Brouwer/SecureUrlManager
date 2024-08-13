using Microsoft.AspNetCore.Mvc;
using SecureUrlManager.App.Features.Registration;

namespace SecureUrlManager.App.Features.OpenGraph;

// TODO domain model instead of adding custom stuff to the graph data
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
            graph.AddMetadata("og", "LoadImages", request.LoadImages.ToString());
            return View("Index", graph);
        }
        catch
        {
            return BadRequest();
        }
    }
}
