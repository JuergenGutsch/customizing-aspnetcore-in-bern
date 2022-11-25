using System.Net;
using Microsoft.AspNetCore.HttpOverrides;

var builder = WebApplication.CreateBuilder(args);


// builder.WebHost.UseKestrel((host, options) =>
// {
//     var config = host.Configuration.GetValue();
//     options.Listen(IPAddress.Loopback, 5000);
//     options.Listen(IPAddress.Loopback, 5001,
//     listenOption =>
//     {
//         listenOption.UseHttps("cert.pfx", "topsecret");
//     });
// });


// builder.Services.Configure<ForwardedHeadersOptions>(options =>
// {
//     options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
// });

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// app.UseForwardedHeaders(new ForwardedHeadersOptions
// {
//     ForwardedHeaders = ForwardedHeaders.XForwardedFor |
//     ForwardedHeaders.XForwardedProto
// });

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
