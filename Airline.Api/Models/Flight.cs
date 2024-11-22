using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.Api.Models
{
    public class Flight
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int FlightId { get; set; }

        [Required]
        public int FlightNumber { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        public int Capacity { get; set; }

        public int AvailableSeats { get; set; }

        [Required]
        public string Departure { get; set; }

        [Required]
        public string Destination { get; set; } 

        public double Price { get; set; } 

        public string DaysOfWeek { get; set; }

    }
}
