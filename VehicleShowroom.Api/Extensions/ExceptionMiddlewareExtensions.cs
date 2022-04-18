using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using VehicleShowroom.Api.Models;

namespace VehicleShowroom.Api.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureUseExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(err =>
            {
                err.Run(async context =>
                {

                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError; //500
                    context.Response.ContentType = "application/json";

                    var features = context.Features.Get<IExceptionHandlerFeature>();
                    if (features != null)
                    {
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            ErrorMessage = "Internal Server Error from Global Error Handling"
                        }.ToString());
                    }
                });

            });
        }
    }
}
