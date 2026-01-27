using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplication1.EntityFramework;
using WebApplication1.Models;
using WebApplication1.Requests;

namespace WebApplication1.Services
{
    public class AuthService
    {
        private readonly LibraryDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthService(LibraryDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public string? Login(string name, string password)
        {
            var user = _context.Users
                .FirstOrDefault(u => u.Name == name);

            if (user == null)
                return null;

            if (user.PasswordHash != password)
                return null;

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };
            Console.WriteLine("JWT KEY (AuthService): " + _configuration["Jwt:Key"]);

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])
            );

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        public bool Register(CreateUserRequest createUserRequest)
        {
            if (_context.Users.Any(u => u.Name == createUserRequest.Name))
                return false;

            var user = new User
            {
                Name = createUserRequest.Name,
                Email = createUserRequest.Email,
                PasswordHash = createUserRequest.Password,
                Role = createUserRequest.Role
            };

            _context.Users.Add(user);
            _context.SaveChanges();
            return true;
        }

    }
}
