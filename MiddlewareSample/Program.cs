using System.Diagnostics;
using MiddlewareSample;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();

var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (!app.Environment.IsDevelopment())
// {
//     app.UseExceptionHandler("/Home/Error");
//     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
//     app.UseHsts();
// }

// app.UseHttpsRedirection();


app.UseStopwatch();


app.Use(async (context, next) =>
{
    try
    {
        await context.Response.WriteAsync(" ===");
        await next(context);
        await context.Response.WriteAsync("=== ");
    }
    catch (System.Exception)
    {

        throw;
    }
});

app.Use(async (context, next) =>
{
    await context.Response.WriteAsync(" >>>>");
    await next(context);
    await context.Response.WriteAsync("<<<< ");
});

app.MapGet("/get", async context => {

});
app.MapMethods("/methods", new [] {"DELETE", "GET", "POST"}, async context =>{

});

app.MapHealthChecks("/health");

app.Map("/", () => "Hallo Bern!");



app.Run();
