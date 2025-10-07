using Ceilapp.Components;
using Ceilapp.Data;
using Ceilapp.Data.Seeders;
using Ceilapp.Models;
using Ceilapp.Models.ceilapp;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OData.ModelBuilder;
using Radzen;
using QuestPDF.Infrastructure;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
QuestPDF.Settings.License = LicenseType.Community; // Set QuestPDF license
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddRazorComponents().AddInteractiveServerComponents().AddHubOptions(options => options.MaximumReceiveMessageSize = 10 * 1024 * 1024);
builder.Services.AddControllers();
builder.Services.AddRadzenComponents();
builder.Services.AddRadzenCookieThemeService(options =>
{
    options.Name = "CeilappTheme";
    options.Duration = TimeSpan.FromDays(365);
});
builder.Services.AddHttpClient();
builder.Services.AddScoped<Ceilapp.ceilappService>();
builder.Services.AddDbContext<Ceilapp.Data.ceilappContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ceilappConnection"));
});
builder.Services.AddHttpClient("Ceilapp").ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler { UseCookies = false }).AddHeaderPropagation(o => o.Headers.Add("Cookie"));
builder.Services.AddHeaderPropagation(o => o.Headers.Add("Cookie"));
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
builder.Services.AddScoped<Ceilapp.SecurityService>();
builder.Services.AddScoped<Ceilapp.ReportService>();
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("ceilappConnection"));
});
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireDigit = false;
}).AddEntityFrameworkStores<ApplicationIdentityDbContext>().AddDefaultTokenProviders();
builder.Services.AddControllers().AddOData(o =>
{
    var oDataBuilder = new ODataConventionModelBuilder();
    oDataBuilder.EntitySet<ApplicationUser>("ApplicationUsers");
    var usersType = oDataBuilder.StructuralTypes.First(x => x.ClrType == typeof(ApplicationUser));
    usersType.AddProperty(typeof(ApplicationUser).GetProperty(nameof(ApplicationUser.Password)));
    usersType.AddProperty(typeof(ApplicationUser).GetProperty(nameof(ApplicationUser.ConfirmPassword)));
    oDataBuilder.EntitySet<ApplicationRole>("ApplicationRoles");
    o.AddRouteComponents("odata/Identity", oDataBuilder.GetEdmModel()).Count().Filter().OrderBy().Expand().Select().SetMaxTop(null).TimeZone = TimeZoneInfo.Utc;
});
builder.Services.AddScoped<AuthenticationStateProvider, Ceilapp.ApplicationAuthenticationStateProvider>();
builder.Services.AddLocalization();
var app = builder.Build();
// Remove the following invalid lines from the ForwardedHeadersOptions object initializer:
//     KnownNetworks.Clear(),
//     KnownProxies.Clear(),
// These are not valid in an object initializer and cause CS0747 and CS0103.
// Instead, call Clear() on the properties after the object is created, as already done below.

var forwardingOptions = new ForwardedHeadersOptions()
{

    ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                      ForwardedHeaders.XForwardedProto |
                      ForwardedHeaders.XForwardedHost,

    // Clear default values and trust all proxies (use with caution)
    KnownProxies = { },
    KnownNetworks = { },

    // This tells the middleware to trust any proxy
    RequireHeaderSymmetry = false,
    ForwardLimit = null

};
// Clear known networks and proxies to accept from any
forwardingOptions.KnownNetworks.Clear();
forwardingOptions.KnownProxies.Clear();

app.UseForwardedHeaders(forwardingOptions);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.MapControllers();
app.UseHeaderPropagation();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapRazorComponents<App>().AddInteractiveServerRenderMode();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
});
// Run migrations
app.Services.CreateScope().ServiceProvider.GetRequiredService<ApplicationIdentityDbContext>().Database.Migrate();

// Seed Algerian locations (states and municipalities)
DBSeeder.SeedAlgerianLocations(app);
DBSeeder.SeedAppRoles(app);
await DBSeeder.SeedAppData(app);

app.Run("http://localhost:5111");