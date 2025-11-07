using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController(AppDbContext dbContext) : BaseApiController
    {
        [HttpPost("register")] // api/account/register
        public async Task<ActionResult<AppUser>> Register(RegisterDto registerDto)
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

            return user;
        }

        private async Task<bool> EmailExists(string email)
        {
            return await dbContext.AppUsers.AsNoTracking().AnyAsync(q => q.Email.ToLower() == email.ToLower());
        }
    }
}
