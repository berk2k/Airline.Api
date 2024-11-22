using Airline.Api.Context;
using Airline.Api.Models;
using Airline.Api.Models.DTO;
using Airline.Api.Services.IServices;
using Microsoft.EntityFrameworkCore;


namespace Airline.Api.Services
{
    public class FlightService : IFlightService
    {
        private readonly AirlineDbContext _context;
        

        public FlightService(AirlineDbContext context)
        {
            _context = context;
           
        }

        public async Task<List<FlightReportDTO>> GetAllFlightsWithDestination(string destination, int pageNumber, int pageSize)
        {
            try
            {
                
                var flights = await _context.Flights
                    .Where(f => f.Destination == destination)
                    .OrderBy(f => f.DepartureDate) 
                    .Skip((pageNumber - 1) * pageSize) 
                    .Take(pageSize)
                    .Select(f => new FlightReportDTO
                    {
                        Departure = f.Departure,
                        Destination = f.Destination,
                        DepartureDate = f.DepartureDate,
                        Capacity = f.Capacity,
                        PageNumber = pageNumber,
                        PageSize = pageSize
                    })
                    .ToListAsync();

                return flights;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<FlightReportDTO>();
            }
        }



        public async Task<bool> InsertFlightAsync(CreateFlightRequest request)
        {
            try
            {
                
                var flight = new Flight
                {
                    Departure = request.From,
                    Destination = request.To,
                    DepartureDate = request.DepartureDate,
                    DaysOfWeek = request.DaysOfWeek,
                    Capacity = request.Capacity,
                    AvailableSeats = request.Capacity,
                    Price = request.Price
                };

                _context.Flights.Add(flight);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

    }
}
