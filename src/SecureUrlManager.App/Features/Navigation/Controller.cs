using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SecureUrlManager.App.Models;

namespace SecureUrlManager.App.Features.Navigation;

public class NavigationController : Controller
{
    //// TODO, this page will have a big box to "shorten" or a big button to log in if you're not
    //public IActionResult Index() => Redirect("/");

    [HttpGet, Route("/{creator}/{hash}")]
    public IActionResult Index(string hash)
    {
        // TODO check if ! hash exists, redirect to 404 if no
        // TODO get hash from db, map => Success = redirect, Unknown = preview page, Unsafe = error page.
        return View();
    }
}
