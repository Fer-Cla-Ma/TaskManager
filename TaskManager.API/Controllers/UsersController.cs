using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Domain.Entities;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(UserManager<ApplicationUser> userManager) : Controller
    {
        [HttpGet]
        public IActionResult GetAll()
        {
            var users = userManager.Users.ToList();
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = userManager.FindByIdAsync;
            if (user == null)
                return NotFound();
            return Ok(user);
        }

        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] ApplicationUser user)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest(ModelState);
        //    try
        //    {
        //        var createdUser = await userService.CreateAsync(user);
        //        return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        //    }
        //    catch (InvalidOperationException ex)
        //    {
        //        return Conflict(new { message = ex.Message });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, new { message = "A problem happened while handling your request.", detail = ex.Message });
        //    }
        //}

        //[HttpPut("{id:guid}")]
        //public async Task<IActionResult> Update(String id, [FromBody] ApplicationUser user)
        //{
        //    if (user == null || id != user.Id)
        //        return BadRequest("User data is invalid.");
        //    var updatedUser = await userService.UpdateAsync(user);
        //    if (updatedUser == null)
        //        return NotFound("User not found.");
        //    return Ok(updatedUser);
        //}

        //[HttpDelete("{id:guid}")]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    var deleted = await userService.DeleteAsync(id);
        //    if (!deleted)
        //        return NotFound("User not found.");
        //    return NoContent();
        //}
    }
}
