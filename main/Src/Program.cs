using Microsoft.EntityFrameworkCore;
using PicPay.Application.Ports;
using PicPay.Application.Usecase;
using PicPay.Infrastructure.Adapters;
using PicPay.Infrastructure.Http.Middlewares;
using PicPay.Infrastructure.Persistence;
using PicPay.Infrastructure.Persistence.Repository;
using Scalar.AspNetCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
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

    app.UseGlobalErrorHandler();
    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();
    app.MapControllers();
}

app.Run();