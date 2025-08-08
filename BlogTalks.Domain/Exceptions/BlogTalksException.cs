using System.Net;

namespace BlogTalks.Domain.Exceptions
{
    public class BlogTalksException : Exception
    {
        public HttpStatusCode StatusCode { get; set; }

        public BlogTalksException(string message, HttpStatusCode statusCode) : base(message) 
        {
            StatusCode = statusCode;
        }
    }
}
