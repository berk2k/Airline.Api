using System.ComponentModel.DataAnnotations;

namespace Airline.Api.Models.DTO
{
    public class CheckInRequestDTO
    {
        [Required]
        public int FlightId { get; set; }

        [Required]
        public int UserId { get; set; }
    }
}
