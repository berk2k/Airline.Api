namespace Airline.Api.Models.DTO
{
    public class QueryFlightDTO
    {
        public DateTime DepartureDate { get; set; }  
        public string Departure { get; set; }        
        public string Destination { get; set; }     
        public int FlightNumber { get; set; }        
        public int AvailableSeats { get; set; }
    }

}
