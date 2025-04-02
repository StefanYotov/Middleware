using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Middelware.Test
{
    public static class ProblemFactory
    {
        private static string GetUsername(HttpContext context)
        {
            return context.User.Identity?.IsAuthenticated == true
                ? context.User.Identity.Name ?? "Unknown User"
                : "Anonymous";
        }

        public static BadRequestProblem CreateBadRequest(HttpContext context, string searchedBy, string searchedFor, string detail)
        {
            return new BadRequestProblem(
                user: GetUsername(context),
                httpMethod: context.Request.Method,
                path: context.Request.Path,
                searchedBy: searchedBy,
                searchedFor: searchedFor,
                detail: detail
            );
        }

        public static UnauthorizedProblem CreateUnauthorized(HttpContext context, string detail)
        {
            return new UnauthorizedProblem(
                user: GetUsername(context),
                httpMethod: context.Request.Method,
                path: context.Request.Path,
                detail: detail
            );
        }

        public static ForbiddenProblem CreateForbidden(HttpContext context, string detail)
        {
            return new ForbiddenProblem(
                user: GetUsername(context),
                httpMethod: context.Request.Method,
                path: context.Request.Path,
                detail: detail
            );
        }

        public static NotFoundProblem CreateNotFound(HttpContext context, string resource, string detail)
        {
            return new NotFoundProblem(
                user: GetUsername(context),
                httpMethod: context.Request.Method,
                path: context.Request.Path,
                resource: resource,
                detail: detail
            );
        }

        public static InternalServerErrorProblem CreateInternalServerError(HttpContext context, string detail)
        {
            return new InternalServerErrorProblem(
                user: GetUsername(context),
                httpMethod: context.Request.Method,
                path: context.Request.Path,
                detail: detail
            );
        }
    }
}
