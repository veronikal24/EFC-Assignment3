using Microsoft.AspNetCore.Components.Web;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ReditBeforeGlowUp;
using ReditBeforeGlowUp.Auth;
using ReditBeforeGlowUp.Services;
using ReditBeforeGlowUp.Services.Http;
using ReditBeforeGlowUp.Services.Http.Implementations;
using SharedFolder.Auth;
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthProvider>();
builder.Services.AddScoped<IAuthService, JwtAuthService>();

//builder.Services.AddScoped<IUserService, UserHttpClient>();
AuthorizationPolicies.AddPolicies(builder.Services);
builder.Services.AddScoped(sp => new HttpClient{BaseAddress = new Uri("http://localhost:5104/")});
builder.Services.AddScoped<IPostService, PostHttpClient>();
builder.Services.AddScoped<IUserService, UserHttpClient>();
// builder.Services.AddScoped(sp => new HttpClient() { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();


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

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

await app.RunAsync();