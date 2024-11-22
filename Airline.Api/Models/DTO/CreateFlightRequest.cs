namespace Airline.Api.Models.DTO
{
    public class CreateFlightRequest
    {
        public string From { get; set; }
        public string To { get; set; }

        public DateTime DepartureDate { get; set; }
        public string DaysOfWeek { get; set; } 
        public int Capacity { get; set; }
        public double Price { get; set; }
    }
}
