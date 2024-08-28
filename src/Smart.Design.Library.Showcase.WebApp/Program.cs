using Microsoft.AspNetCore.Http.Json;
using Newtonsoft.Json.Serialization;
using Smart.Design.Library.Extensions;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions {WebRootPath = "wwwroot"});
builder.Services.AddKendo();

// Add services to the container.
builder.Services.AddMvc()
    .AddNewtonsoftJson(options =>
                       options.SerializerSettings.ContractResolver =
                          new DefaultContractResolver());
builder.Services.AddRazorPages();
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

app.Run();
