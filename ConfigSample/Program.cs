using ConfigSample;

var builder = WebApplication.CreateBuilder(args);

var env = builder.Environment;
builder.Configuration.SetBasePath(env.ContentRootPath);
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);

builder.Configuration.AddIniFile("appsettings.ini", optional: false, reloadOnChange: true);
builder.Configuration.AddIniFile($"appsettings.{env.EnvironmentName}.ini", optional: true, reloadOnChange: true);

builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<AppSettings>(
    builder.Configuration.GetSection("AppSettings"));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

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
