
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Middelware.Test;
using System.Text;

namespace Middleware.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "your-issuer",         // Replace with your issuer
        ValidAudience = "your-audience",     // Replace with your audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ax77Gavz+MUOTziFa0e+OOz8jbCATvC52tieTMNRH1A=")) // Replace with your key
    };
});


            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "An ASP.NET Core Web API for managing ToDo items",
                    TermsOfService = new Uri("https://example.com/terms"),
                });
            });
            builder.Services.AddOpenApi();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            app.UseCustomProblemResponse();
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
