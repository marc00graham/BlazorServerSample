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

Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Mgo+DSMBaFt/QHRqVVhkVFpFdEBBXHxAd1p/VWJYdVt5flBPcDwsT3RfQF5jSn9Sd0RhWH1XcHdcQQ==;Mgo+DSMBPh8sVXJ0S0J+XE9AflRDX3xKf0x/TGpQb19xflBPallYVBYiSV9jS31TdURiWH5ccXZcRmVYVg==;ORg4AjUWIQA/Gnt2VVhkQlFacldJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkZiWH5fcnVXT2BaVkM=;MTMwMzQyOUAzMjMwMmUzNDJlMzBZajJPSW1kaitjekNoUHJDN3ZGSnI2UzVFQXRRTld4bzNwL1I0cWx1OXU0PQ==;MTMwMzQzMEAzMjMwMmUzNDJlMzBCaFhDNWtwRFBiVHlOTDlrcXlXR0NtVG5rUDJnTjF1UkJ2SVNGZGdaKzdBPQ==;NRAiBiAaIQQuGjN/V0Z+WE9EaFtKVmJLYVB3WmpQdldgdVRMZVVbQX9PIiBoS35RdUVgWH5fcXZURGldVUZx;MTMwMzQzMkAzMjMwMmUzNDJlMzBVQnJjRWk1RXQ4aDBIWTladzFRbUw0dENQeE5kVDk5NHdaT08xZUwvN3JFPQ==;MTMwMzQzM0AzMjMwMmUzNDJlMzBYUjRaR242dVovR1BaUVpUS3JKZkNvTkhGMENpUm5NR1UyK1JoYzBST2FJPQ==;Mgo+DSMBMAY9C3t2VVhkQlFacldJXGFWfVJpTGpQdk5xdV9DaVZUTWY/P1ZhSXxQdkZiWH5fcnVXT2JcVkM=;MTMwMzQzNUAzMjMwMmUzNDJlMzBFbjQwbm1TMXNYNU9XR1NKQVYzQ1UzU0R6QWxSV2FkMGtZc3RyK2MyMndjPQ==;MTMwMzQzNkAzMjMwMmUzNDJlMzBhOTJ3V0x6TUdZa0FVV0FyeTV0TFpVK3p3TlVST1QzMHlnRXFHSW5SUlJRPQ==;MTMwMzQzN0AzMjMwMmUzNDJlMzBVQnJjRWk1RXQ4aDBIWTladzFRbUw0dENQeE5kVDk5NHdaT08xZUwvN3JFPQ==");

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
