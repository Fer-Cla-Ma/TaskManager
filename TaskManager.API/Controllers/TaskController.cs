using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Application.Interfaces;
using TaskManager.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaskController(ITaskItemService taskItemService) : Controller
    {
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await taskItemService.GetAllAsync();
            return Ok(tasks);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TaskItem taskItem)
        {
            if (taskItem == null)
                return BadRequest();

            var createdTask = await taskItemService.CreateAsync(taskItem);
            if (createdTask == null)
                return StatusCode(500, "A problem happened while handling your request.");

            return CreatedAtAction(nameof(GetById), new { id = createdTask.Id }, createdTask);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] TaskItem taskItem)
        {
            if (taskItem == null || id != taskItem.Id)
                return BadRequest();

            var updatedTask = await taskItemService.UpdateAsync(taskItem);
            if (updatedTask == null)
                return NotFound();

            return Ok(updatedTask);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await taskItemService.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var task = await taskItemService.GetByIdAsync(id);
            if (task == null)
                return NotFound();

            return Ok(task);
        }
    }
}
