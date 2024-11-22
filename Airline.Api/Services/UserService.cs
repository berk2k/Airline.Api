using Airline.Api.Context;
using Airline.Api.Models.DTO;
using Airline.Api.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Airline.Api.Services.IServices;

namespace Airline.Api.Services
{
    public class UserService : IUserService
    {
        private readonly AirlineDbContext _airlineDb;
        private string secretKey;

        public UserService(AirlineDbContext airlineDb, IConfiguration configuration)
        {
            _airlineDb = airlineDb;
            secretKey = configuration.GetValue<string>("ApiSettings:Secret");

        }

        public bool IsUserUnique(string userName)
        {
            var user = _airlineDb.Users.FirstOrDefault(u => u.UserName == userName);

            if (user == null)
            {
                return true;
            }

            return false;
        }

        public LoginResponseDTO Login(LoginReguestDTO loginReguestDTO)
        {
            var user = _airlineDb.Users.FirstOrDefault(u => u.UserName == loginReguestDTO.UserName && u.Password == loginReguestDTO.Password);

            if (user == null)
            {
                return new LoginResponseDTO()
                {
                    Token = "",
                    APIUser = null
                };
            }

            var tokenHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name,user.UserId.ToString()),
                    new Claim(ClaimTypes.Role,user.Role)
                }),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            LoginResponseDTO loginResponseDTO = new LoginResponseDTO()
            {
                Token = tokenHandler.WriteToken(token),
                APIUser = user,
            };

            return loginResponseDTO;

        }

        public User Register(RegisterRequestDTO reqisterationRequestDTO)
        {
            User user = new User()
            {
                UserName = reqisterationRequestDTO.UserName,
                Name = reqisterationRequestDTO.Name,
                Password = reqisterationRequestDTO.Password,
                Role = reqisterationRequestDTO.Role,
            };

            _airlineDb.Users.Add(user);
            _airlineDb.SaveChanges();

            user.Password = "";

            return user;

        }
    }
}
