using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using API.Entities;
using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController(AppDbContext dbContext, ITokenService tokenService) : BaseApiController
    {
        [HttpPost("register")] // api/account/register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await EmailExists(registerDto.Email)) return BadRequest("Email taken");

            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            dbContext.AppUsers.Add(user);
            await dbContext.SaveChangesAsync();

            return user.ToUserDto(tokenService);
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await dbContext.AppUsers.SingleOrDefaultAsync(q => q.Email.ToLower() == loginDto.Email.ToLower());

            if (user == null) return Unauthorized("Invalid email or password.");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < computedHash.Length; i++)
            {
                if (computedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid email or password.");
            }

            return user.ToUserDto(tokenService);
        }

        private async Task<bool> EmailExists(string email)
        {
            return await dbContext.AppUsers.AsNoTracking().AnyAsync(q => q.Email.ToLower() == email.ToLower());
        }
    }
}
