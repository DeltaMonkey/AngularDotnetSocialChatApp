using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class MembersController(AppDbContext context) : BaseApiController
    {
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<AppUser>>> GetMembers()
        {
            var members = await context.AppUsers.ToListAsync();

            return members;
        }

        [HttpGet("{id}")] // localhost:5001/api/Members/bob-id
        public async Task<ActionResult<AppUser>> GetMember(string id)
        {
            var member = await context.AppUsers.FindAsync(id);

            if (member == null) return NotFound();

            return member;
        }
    }
}
