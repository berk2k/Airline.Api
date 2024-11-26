namespace Airline.Api.Models.DTO
{
    public class TicketDTO
    {
        public int FlightNumber { get; set; }
        public DateTime DepartureDate { get; set; }
        public string Departure { get; set; }
        public string Destination { get; set; }
        public string PassengerFullName { get; set; }
        public DateTime BookingDate { get; set; }
        public bool IsCheckedIn { get; set; }
    }
}
