namespace Airline.Api.Models.DTO
{
    public class LoginResponseDTO
    {
        public User APIUser { get; set; }

        public string Token { get; set; }
    }
}
