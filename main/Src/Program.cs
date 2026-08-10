using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using PicPay.Application.Ports;
using PicPay.Application.Usecase;
using PicPay.Infrastructure.Adapters;
using PicPay.Infrastructure.Http.Middlewares;
using PicPay.Infrastructure.Persistence;
using PicPay.Infrastructure.Persistence.Repository;
using PicPay.Infrastructure.Telemetry;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.AddMyOtel();
    builder.Services.AddOpenApi();
    builder.Services.AddDbContext<DbConnection>(opt => opt.UseNpgsql(builder.Configuration["ConnectionStrings:URL"]));
    builder.Services.AddScoped<HttpClient>();
    builder.Services.AddScoped<IHttpServices, HttpServices>();
    builder.Services.AddScoped<IUserRepo, UserRepo>();
    builder.Services.AddScoped<IPaymentRepo, PaymentRepo>();
    builder.Services.AddScoped<UserUsecase>();
    builder.Services.AddScoped<PaymentUsecase>();
    builder.Services.AddControllers();

    Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .CreateLogger();
    builder.Host.UseSerilog();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.MapScalarApiReference();
        app.MapOpenApi();
    }

    app.MapHealthChecks("/health",new HealthCheckOptions
    {
        Predicate = _ => true,
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json";
            var result = System.Text.Json.JsonSerializer.Serialize(new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(entry => new
                {
                    name = entry.Key,
                    status = entry.Value.Status.ToString(),
                    exception = entry.Value.Exception?.Message,
                    duration = entry.Value.Duration.ToString()
                })
            });
            await context.Response.WriteAsync(result);
        }
    });

    

    app.UseGlobalErrorHandler();
    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();
    app.MapControllers();
}

app.Run();