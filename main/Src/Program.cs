using Microsoft.EntityFrameworkCore;
using PicPay.Inner.Usecase;
using PicPay.Outer.Persistence;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddOpenApi();
    builder.Services.AddDbContext<DbConnection>(opt => 
        opt.UseNpgsql(builder.Configuration.GetConnectionString("URI")));
    builder.Services.AddScoped<UserUc>();
    builder.Services.AddScoped<PaymentUc>();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();
}

app.Run();

