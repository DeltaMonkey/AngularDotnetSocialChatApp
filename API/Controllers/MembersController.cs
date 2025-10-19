using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")] // localhost:5001/api/Members
    [ApiController]
    public class MembersController(AppDbContext context) : ControllerBase
    {
        [HttpGet]
        public ActionResult<IReadOnlyList<AppUser>> GetMembers()
        {
            var members = context.AppUsers.ToList();

            return members;
        }

        [HttpGet("{id}")] // localhost:5001/api/Members/bob-id
        public ActionResult<AppUser> GetMember(string id)
        {
            var member = context.AppUsers.Find(id);

            if (member == null) return NotFound();

            return member;
        }
    }
}
