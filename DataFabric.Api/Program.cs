using Microsoft.AspNetCore.SpaServices.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// SPA proxy: in development, proxy to the Angular dev server configured in the .csproj
app.UseSpa(spa =>
{
    spa.Options.SourcePath = builder.Configuration["SpaRoot"] ?? "..\\datafabric.client";
    if (app.Environment.IsDevelopment())
    {
        // Use the configured server URL to proxy requests. SpaProxy will launch the
        // configured `SpaProxyLaunchCommand` (npm start) from the project file if needed.
        var proxyUrl = builder.Configuration["SpaProxyServerUrl"] ?? "http://localhost:4200";
        spa.UseProxyToSpaDevelopmentServer(proxyUrl);
    }
    else
    {
        app.MapFallbackToFile("index.html");
    }
});

app.Run();
