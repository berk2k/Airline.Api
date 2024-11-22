using System.ComponentModel.DataAnnotations;

namespace Airline.Api.Models.DTO
{
    public class FlightReportDTO
    {

        public DateTime DepartureDate { get; set; }
        public int Capacity { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

    }
}
