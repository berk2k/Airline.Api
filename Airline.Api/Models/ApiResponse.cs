using System.Net;

namespace Airline.Api.Models
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }

        public string Status { get; set; } = "SUCCESS";
        public bool IsSuccess { get; set; } = true;

        public string ErrorMessage { get; set; }

        public object Result { get; set; }
    }
}
