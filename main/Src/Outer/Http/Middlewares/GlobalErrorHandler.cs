using Microsoft.AspNetCore.Diagnostics;

namespace PicPay.Outer.Http.Middlewares;

public static class GlobalErrorHandler
{
    public static void UseGlobalErrorHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.ContentType = "application/json";

                var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;

                if (exception is null) return;

                var stCode = exception switch
                {
                    ArgumentException => StatusCodes.Status400BadRequest,
                    KeyNotFoundException => StatusCodes.Status404NotFound,
                    _ => StatusCodes.Status500InternalServerError
                };

                context.Response.StatusCode = stCode;

                var reponse = new
                {
                    error = exception.Message,
                    stCode 
                };

                await context.Response.WriteAsJsonAsync(reponse);
            });
        });
    }
}