using Microsoft.EntityFrameworkCore;
using PicPay.Inner.Usecase;
using PicPay.Outer.Persistence;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddOpenApi();
    builder.Services.AddDbContext<DbConnection>(opt => 
        opt.UseNpgsql(builder.Configuration.GetConnectionString("URI")));
    builder.Services.AddScoped<UserUc>();
    builder.Services.AddScoped<PaymentUc>();

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
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();
    app.UseSerilogRequestLogging();
}

app.Run();

