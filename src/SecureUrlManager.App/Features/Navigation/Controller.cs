using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.WindowsAzure.Storage.Table;
using SecureUrlManager.App.Features.Shared;
using System.Net;

namespace SecureUrlManager.App.Features.Navigation;

public class NavigationController : Controller
{
    private readonly CloudTable _urlTable;

    public NavigationController([FromKeyedServices("SecureUrlManagerShortUrls")] CloudTable urlTable)
    {
        _urlTable = urlTable;
    }

    [HttpGet, Route("/{creator}/{hash}")]
    public async Task<IActionResult> Index(string creator, string hash)
    {
        var result = await FindUrlRecord(creator, hash);

        return result.Match(
            VerifyUrlRecord,
            NotFoundView,
            // TODO what do we do when an error occurs?
            // Just log and blow up?
            _ => NotFoundView()
        );
    }

    private ViewResult NotFoundView()
    {
        var notFoundViewResult = View("NotFound");
        notFoundViewResult.StatusCode = (int)HttpStatusCode.NotFound;

        return notFoundViewResult;
    }

    private IActionResult VerifyUrlRecord(UrlRecord urlRecord)
    {

        // TODO check if ! hash exists, redirect to 404 if no
        // TODO get hash from db, map => Success = redirect, Unknown = preview page, Unsafe = error page.

        // https://localhost:7180/marvin.brouwer/C1B6E261
        // https://localhost:7180/marvin.brouwer/6F34F935
        // https://localhost:7180/marvin.brouwer/73E940F4

        // TODO uknown
        if (urlRecord.Hash == "73E940F4")
        {
            return View("Preview", urlRecord);
        }
        // TODO blocked
        if (urlRecord.Hash == "C1B6E261")
        {
            return View("Blocked", urlRecord);
        }

        return Redirect(urlRecord.Original.ToString());
    }

    private async Task<OptionalResult<UrlRecord>> FindUrlRecord(string creator, string hash)
    {
        try
        {
            var operation = new TableQuery<UrlRecord.Entity>()
                .Where(TableQuery.GenerateFilterCondition(nameof(UrlRecord.Entity.PartitionKey), QueryComparisons.Equal, creator))
                .Where(TableQuery.GenerateFilterCondition(nameof(UrlRecord.Entity.RowKey), QueryComparisons.Equal, hash))
                .Take(1);

            var tableEntity = await _urlTable.ExecuteQuerySegmentedAsync(operation, new TableContinuationToken());
            if (tableEntity.Results.Count != 1) return new OptionalResult<UrlRecord>();
            var urlRecord = tableEntity.Results.First();
            return urlRecord.ToRecord();
        }
        catch (Exception ex)
        {
            return new OptionalResult<UrlRecord>(ex);
        }
    }
}
