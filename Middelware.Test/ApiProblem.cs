namespace Middelware.Test
{
    public class ApiProblem
    {
        public string Type { get; set; }
        public int Status { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public string User { get; set; }
        public string HttpMethod { get; set; }
        public string Path { get; set; }
    }

    public class BadRequest : ApiProblem
    {
        public string SearchedBy { get; set; }
        public string SearchedFor { get; set; }

        public BadRequest(string user, string httpMethod, string path, string searchedBy, string searchedFor, string detail)
        {
            Type = "https://httpstatuses.com/400";
            Status = 400;
            Title = "Bad Request";
            Detail = detail;
            User = user;
            HttpMethod = httpMethod;
            Path = path;
            SearchedBy = searchedBy;
            SearchedFor = searchedFor;
        }
    }

    public class UnauthorizedProblem : ApiProblem
    {
        public UnauthorizedProblem(string user, string httpMethod, string path, string detail)
        {
            Type = "https://httpstatuses.com/401";
            Status = 401;
            Title = "Unauthorized";
            Detail = detail;
            User = user;
            HttpMethod = httpMethod;
            Path = path;
        }
    }

    public class ForbiddenProblem : ApiProblem
    {
        public ForbiddenProblem(string user, string httpMethod, string path, string detail)
        {
            Type = "https://httpstatuses.com/403";
            Status = 403;
            Title = "Forbidden";
            Detail = detail;
            User = user;
            HttpMethod = httpMethod;
            Path = path;
        }
    }

    public class NotFoundProblem : ApiProblem
    {
        public string Resource { get; set; }

        public NotFoundProblem(string user, string httpMethod, string path, string resource, string detail)
        {
            Type = "https://httpstatuses.com/404";
            Status = 404;
            Title = "Not Found";
            Detail = detail;
            User = user;
            HttpMethod = httpMethod;
            Path = path;
            Resource = resource;
        }
    }

    public class InternalServerErrorProblem : ApiProblem
    {
        public InternalServerErrorProblem(string user, string httpMethod, string path, string detail)
        {
            Type = "https://httpstatuses.com/500";
            Status = 500;
            Title = "Internal Server Error";
            Detail = detail;
            User = user;
            HttpMethod = httpMethod;
            Path = path;
        }
    }
}
