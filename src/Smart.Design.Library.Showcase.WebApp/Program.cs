using Microsoft.AspNetCore.Localization;
using Smart.Design.Library.Extensions;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions {WebRootPath = "wwwroot"});
builder.Services.AddKendo();

// Add services to the container.
builder.Services.AddRazorPages()
    // Maintain the property names casing globally. Mandatory to ensure a smooth communication between the Kendo widgets and their data source methods.
    .AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddSmartDesign();
builder.Services.AddKendo();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

builder.WebHost.UseStaticWebAssets();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.UseRequestLocalization(l => {
    l.DefaultRequestCulture = new RequestCulture("fr-BE");
    l.AddSupportedCultures("fr-BE", "nl-BE");
    l.AddSupportedUICultures("fr-BE", "nl-BE");
});

app.Run();
