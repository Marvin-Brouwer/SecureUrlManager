using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Table;
using SecureUrlManager.App.Features.Shared;

namespace SecureUrlManager.App.Features.Registration;

public class RegistrationController : Controller
{
    private readonly UrlShortener _urlShortener;
    private readonly CloudTable _urlTable;

    public RegistrationController(UrlShortener urlShortener, [FromKeyedServices("SecureUrlManagerShortUrls")] CloudTable urlTable)
    {
        _urlShortener = urlShortener;
        _urlTable = urlTable;
    }

    [HttpGet]
    public IActionResult Index() => View();

    [HttpPost("shorten")]
    public async Task<IActionResult> ApplyForShortening([FromForm] ShortenUrlRequest request)
    {
        // TODO get from auth context
        var userId = "marvin.brouwer";

        // TODO validate request.

        var result = _urlShortener.Shorten(userId, request.Url);
        if (result.IsSuccess) await result.Match(StoreUrlRecord, _ => Task.CompletedTask);

        return result.Match(
            urlRecord => View("RegistrationSummary", urlRecord),
            error => View("RegistrationError", error)
        );
    }

    private async Task StoreUrlRecord(UrlRecord urlRecord)
    {
        var tableEntity = urlRecord.ToTabelEntity();
        var operation = TableOperation.InsertOrMerge(tableEntity);
        await _urlTable.ExecuteAsync(operation);
    }
}
