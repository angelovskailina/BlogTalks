using System.Net;

namespace BlogTalks.Domain.Exceptions
{
    public class BlogTalksException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public BlogTalksException(string message, HttpStatusCode statusCode) : base(message) 
        {
            StatusCode = statusCode;
        }
    }
}
