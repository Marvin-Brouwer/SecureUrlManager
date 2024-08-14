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
