using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using SecureUrlManager.App.Features.Registration;
using SecureUrlManager.App.Features.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

//var storageAccount = CloudStorageAccount.Parse(StorageConnectionString);
var storageAccount = CloudStorageAccount.DevelopmentStorageAccount;
var tableClient = storageAccount.CreateCloudTableClient();
var shortUrlTable = tableClient
    .GetTableReference("SecureUrlManagerShortUrls");

builder.Services.AddKeyedScoped("SecureUrlManagerShortUrls", (_ , _) => shortUrlTable);
builder.Services.AddSingleton(tableClient);

builder.Services.AddSingleton<HashGenerator>();
builder.Services.AddTransient<UrlShortener>();

// Configure MVC for AOD
builder.Services.ConfigureAll<RazorViewEngineOptions>(options =>
{
    options.ViewLocationFormats.Clear();
    options.ViewLocationFormats.Add("/Features/{2}/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Features/Shared/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
    options.ViewLocationFormats.Add("/Views/_ViewStart.cshtml" + RazorViewEngine.ViewExtension);

    options.AreaViewLocationFormats.Clear();
    options.AreaViewLocationFormats.Add("/Features/{2}/{1}/{0}" + RazorViewEngine.ViewExtension);
    options.AreaViewLocationFormats.Add("/Features/Shared/{0}" + RazorViewEngine.ViewExtension);
    options.AreaViewLocationFormats.Add("/Views/Shared/{0}" + RazorViewEngine.ViewExtension);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // TODO Error controller
    //app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Registration}/{action=Index}/{id?}"
);


await shortUrlTable
    .CreateIfNotExistsAsync();

await app
    .RunAsync();
