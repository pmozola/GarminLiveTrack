using GarminLiveTrack.Web.Application.Service.Email;
using GarminLiveTrack.Web.Data;
using GarminLiveTrack.Web.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.Configure<EmailAccountConfiguration>(
    builder.Configuration.GetSection(EmailAccountConfiguration.EmailAccountConfigurationOptionName));
builder.Services.Configure<EmailBackgroundServiceOptions>(
    builder.Configuration.GetSection(EmailBackgroundServiceOptions.EmailBackgroundServiceOptionName));
builder.Services.AddHostedService<EmailCheckerBackgroundService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
