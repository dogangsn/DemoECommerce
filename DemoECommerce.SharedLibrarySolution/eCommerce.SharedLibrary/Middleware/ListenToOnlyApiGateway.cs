﻿using Microsoft.AspNetCore.Http;  
namespace eCommerce.SharedLibrary.Middleware
{
    public class ListenToOnlyApiGateway(RequestDelegate next)
    {
        public async Task InvokeAsync(Microsoft.AspNetCore.Http.HttpContext context)
        {
            // Extract specific header from the request
            var signedHeader = context.Request.Headers["Api-Gateway"];

            // NULL means, the request is not coming from the Api Gateway
            if (signedHeader.FirstOrDefault() is null) 
            {
                context.Response.StatusCode = StatusCodes.Status503ServiceUnavailable;
                await context.Response.WriteAsync("Sorry, service is unavaible");
                return;
            }
            else
            {
                await next(context);
            } 
        }

    }
}
