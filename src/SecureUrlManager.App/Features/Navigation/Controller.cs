using Microsoft.AspNetCore.Mvc;
using SecureUrlManager.App.Features.Shared;
using System.Net;

namespace SecureUrlManager.App.Features.Navigation;

public class NavigationController : Controller
{
    //// TODO, this page will have a big box to "shorten" or a big button to log in if you're not
    //public IActionResult Index() => Redirect("/");

    [HttpGet, Route("/{creator}/{hash}")]
    public IActionResult Index(string creator, string hash)
    {
        // TODO check if ! hash exists, redirect to 404 if no
        // TODO get hash from db, map => Success = redirect, Unknown = preview page, Unsafe = error page.

        // https://localhost:7180/marvin.brouwer/C1B6E261
        // https://localhost:7180/marvin.brouwer/6F34F935
        // https://localhost:7180/marvin.brouwer/73E940F4

        if (hash == "73E940F4")
        {
            var record = new UrlRecord
            {
                Hash = hash,
                Id = hash,
                ShortUrl = new Uri("http://tempuri.com"),
                StoredBy = creator,
                Original = new Uri("https://www.conclusion.nl")
            };
            return View("Preview", record);
        }
        if (hash == "C1B6E261")
        {
            var record = new UrlRecord
            {
                Hash = hash,
                Id = hash,
                ShortUrl = new Uri("http://tempuri.com"),
                StoredBy = creator,
                Original = new Uri("http://www.google.nl/")
            };
            return View("Blocked", record);
        }
        if (hash == "6F34F935")
        {
            return Redirect("https://www.google.nl");
        }

        var notFoundViewResult = View("NotFound");
        notFoundViewResult.StatusCode = (int)HttpStatusCode.NotFound;

        return notFoundViewResult;
    }
}
