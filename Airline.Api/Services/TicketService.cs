using Airline.Api.Context;
using Airline.Api.Models;
using Airline.Api.Models.DTO;
using Airline.Api.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace Airline.Api.Services
{
    public class TicketService : ITicketService
    {
        private readonly AirlineDbContext _context;

        public TicketService(AirlineDbContext context)
        {
            _context = context;
        }


        public async Task<string> BuyTicket(int flightId, int userId)
        {
            try
            {

                var flight = await _context.Flights.FirstOrDefaultAsync(f => f.FlightId == flightId);
                if (flight == null)
                {
                    return "Flight not found.";
                }

                if (flight.AvailableSeats <= 0)
                {
                    return "No available seats on this flight.";
                }

               
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (user == null)
                {
                    return "User not found.";
                }

               
                var ticket = new Ticket
                {
                    FlightId = flightId,
                    PassengerFullName = user.Name,
                    UserId = userId,
                    BookingDate = DateTime.UtcNow,
                    IsCheckedIn = false
                };

                _context.Tickets.Add(ticket);

               
                flight.AvailableSeats--;

                await _context.SaveChangesAsync();

                return "Ticket successfully booked.";
            }
            catch (DbUpdateException ex)
            {
                
                return $"An error occurred while booking the ticket: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"An unexpected error occurred: {ex.Message}";
            }
        }

        public async Task<string> CheckInAsync(int flightId, int userId)
        {
            try
            {
                
                var ticket = await _context.Tickets
                    .Include(t => t.Flight)
                    .FirstOrDefaultAsync(t => t.FlightId == flightId && t.UserId == userId);

                if (ticket == null)
                {
                    return "No ticket found for this flight and user.";
                }

                
                if (ticket.IsCheckedIn)
                {
                    return "User has already checked in.";
                }

                
                ticket.IsCheckedIn = true;

                await _context.SaveChangesAsync();

                return "Check-in successful.";
            }
            catch (DbUpdateException ex)
            {
                
                return $"An error occurred while checking in: {ex.Message}";
            }
            catch (Exception ex)
            {
                return $"An unexpected error occurred: {ex.Message}";
            }
        }


        public async Task<List<TicketDTO>> GetMyTickets(int userId)
        {
            try
            {
                
                var tickets = await _context.Tickets
                    .Where(t => t.UserId == userId)
                    .Select(t => new TicketDTO
                    {
                        FlightNumber = t.Flight.FlightNumber,
                        DepartureDate = t.Flight.DepartureDate,
                        Departure = t.Flight.Departure,
                        Destination = t.Flight.Destination,
                        PassengerFullName = t.PassengerFullName,
                        BookingDate = t.BookingDate,
                        IsCheckedIn = t.IsCheckedIn
                    })
                    .ToListAsync();

                if (tickets == null || tickets.Count == 0)
                {
                    return new List<TicketDTO>(); 
                }

                return tickets;
            }
            catch (Exception ex)
            {
                
                Console.WriteLine($"An error occurred while fetching the tickets: {ex.Message}");
                return new List<TicketDTO>();
            }
        }


    }
}
