using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    options.CacheProfiles.TryAdd("MyProfile",
    new CacheProfile
    {
        Duration = 30,
        VaryByHeader = "User-Agent",
        Location = ResponseCacheLocation.Client
    });
});
builder.Services.AddResponseCaching();
builder.Services.AddMemoryCache();
builder.Services.AddOutputCache();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = context =>
    {
        context.Context.Response.Headers.TryAdd(
            "Cache-Control", $"public, max-age=90000"
        );
    }
});

app.UseRouting();

app.UseAuthorization();

app.UseResponseCaching();
app.UseOutputCache();

app.MapGet("/time", () =>
{
    return DateTime.Now.AddSeconds(5);
}).CacheOutput(options => {
    
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
