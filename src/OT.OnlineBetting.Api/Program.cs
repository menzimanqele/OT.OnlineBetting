using System.Reflection;
using Microsoft.AspNetCore.Diagnostics;
using OT.OnlineBetting.Api.Middleware;
using OT.OnlineBetting.Application.Extensions.IoC;
using OT.OnlineBetting.Application.Queries.Wager;
using OT.OnlineBetting.Shared.IoC;
using Serilog;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger(); // Early logger for startup

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckl
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSwaggerGen(options =>
    {
        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
    builder.Services.AddSingleton<IExceptionHandler, ErrorHandlerMiddleware>();
    builder.AddLoggingSupport();
    builder.Services.RegisterSharedServices(builder.Configuration);
    builder.Services.AddApplicationConfigurations();
    builder.Services.AddMediatR(cfg =>
        cfg.RegisterServicesFromAssembly(typeof(GetWagersByPlayerQuery).Assembly)
    );
    var app = builder.Build();

// Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI(opts =>
        {
            opts.EnableTryItOutByDefault();
            opts.DocumentTitle = "OT OnlineBetting App";
            opts.DisplayRequestDuration();
        });
    }

    app.UseHttpsRedirection();
    app.UseAuthorization();
    app.MapControllers();
    Log.Information("Host is starting");
    await app.RunAsync();
    Log.Information("Host is shutting down");
}
catch (Exception exception)
{
    Console.WriteLine(exception.Message);
    Log.Fatal(exception, "Host terminated unexpectedly! '{ErrorMessage}'", exception.Message);
}
finally
{
    Log.CloseAndFlush();
}