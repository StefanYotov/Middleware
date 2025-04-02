using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Middelware.Test
{
    public class CustomProblemResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomProblemResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Keep the original response stream
            var originalBodyStream = context.Response.Body;

            // Use a memory stream to capture the response
            using var memoryStream = new MemoryStream();
            context.Response.Body = memoryStream;

            await _next(context); // Let the request pipeline run

            if (IsErrorStatus(context.Response.StatusCode))
            {
                // Read any content already written (if needed)
                memoryStream.Seek(0, SeekOrigin.Begin);
                string originalContent = await new StreamReader(memoryStream).ReadToEndAsync();

                // Create a new problem response based on status code
                object problemResponse = context.Response.StatusCode switch
                {
                    StatusCodes.Status400BadRequest => ProblemFactory.CreateBadRequest(context, "ExtractedUserInfo", originalContent, "Invalid request parameters."),
                    StatusCodes.Status401Unauthorized => ProblemFactory.CreateUnauthorized(context, "Authentication is required to access this resource."),
                    StatusCodes.Status403Forbidden => ProblemFactory.CreateForbidden(context, "You do not have permission to access this resource."),
                    StatusCodes.Status404NotFound => ProblemFactory.CreateNotFound(context, "Resource", "The requested resource was not found."),
                    StatusCodes.Status500InternalServerError => ProblemFactory.CreateInternalServerError(context, "An unexpected error occurred."),
                    _ => null
                };

                // Prepare a new response
                context.Response.ContentType = "application/json";
                context.Response.Body = originalBodyStream; // Restore original stream

                var jsonResponse = JsonSerializer.Serialize(problemResponse, new JsonSerializerOptions { WriteIndented = true });
                await context.Response.WriteAsync(jsonResponse);
            }
            else
            {
                // If no error, simply copy the captured response back
                memoryStream.Seek(0, SeekOrigin.Begin);
                await memoryStream.CopyToAsync(originalBodyStream);
            }
        }

        private bool IsErrorStatus(int statusCode)
        {
            return statusCode == StatusCodes.Status400BadRequest ||
                   statusCode == StatusCodes.Status401Unauthorized ||
                   statusCode == StatusCodes.Status403Forbidden ||
                   statusCode == StatusCodes.Status404NotFound ||
                   statusCode == StatusCodes.Status500InternalServerError;
        }
    }
}
