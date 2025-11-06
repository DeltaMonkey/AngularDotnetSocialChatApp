using System.Security.Cryptography;
using System.Text;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AccountController(AppDbContext dbContext) : BaseApiController
    {
        [HttpPost("register")] // api/account/register
        public async Task<ActionResult<AppUser>> Register(string email, string displayName, string password)
        {
            using var hmac = new HMACSHA512();
            var user = new AppUser
            {
                DisplayName = displayName,
                Email = email,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };

            dbContext.AppUsers.Add(user);
            await dbContext.SaveChangesAsync();

            return user;
        }
    }
}
