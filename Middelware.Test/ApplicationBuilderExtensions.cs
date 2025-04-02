using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Middelware.Test
{
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Adds the custom problem response middleware to the request pipeline.
        /// </summary>
        public static IApplicationBuilder UseCustomProblemResponse(this IApplicationBuilder app)
        {
            return app.UseMiddleware<CustomProblemResponseMiddleware>();
        }
    }
}
