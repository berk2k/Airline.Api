using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Airline.Api.Models
{
    public class Ticket
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TicketId { get; set; }

        [ForeignKey("Flight")]
        public int FlightId { get; set; }

        public Flight Flight { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; } 

        public User User { get; set; }

        public string PassengerFullName { get; set; } 

        public DateTime BookingDate { get; set; } 

        public bool IsCheckedIn { get; set; } 
    }
}
