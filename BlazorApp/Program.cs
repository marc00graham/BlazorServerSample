using BlazorApp.Data;
using BlazorApp.Shared;
using Microsoft.EntityFrameworkCore;
using Syncfusion.Blazor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSyncfusionBlazor();

builder.Services.AddDbContextFactory<EmployeeManagerDbContext>(
    opt => opt.UseSqlServer(
        builder.Configuration.GetConnectionString("EmployeeManagerDb")));
builder.Services.AddScoped<StateContainer>();

var app = builder.Build();

// read key from appsettings.json
Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(app.Configuration["SyncfusionLicenseKey"]);

#if DEBUG
// Ensure db is update when developing
await EnsureDatabaseIsMigrated(app.Services);

async Task EnsureDatabaseIsMigrated(IServiceProvider services)
{
    using var scope = services.CreateScope();
    using var ctx = scope.ServiceProvider.GetService<EmployeeManagerDbContext>();

    if (ctx is not null)
    {
        await ctx.Database.MigrateAsync();
    }
}
#endif

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

app.Run();
