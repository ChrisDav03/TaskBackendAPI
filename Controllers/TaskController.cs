using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskBackendAPI.Custom;
using TaskBackendAPI.Data;
using TaskBackendAPI.Models;
using TaskBackendAPI.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Threading.Tasks;
using System;

namespace TaskBackendAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly TaskDb _dbContext;
        public TaskController(TaskDb dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List()
        {
            var list = await _dbContext.Tasks.ToListAsync();
            return StatusCode(StatusCodes.Status200OK, new {value = list});
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(TaskDTO obj)
        {
            var modelTask = new Models.Task
            {
                title = obj.Title,
                description = obj.Description,
                status = obj.Status,
                userAsignedId = obj.UserAsignedId,
                createdAt = DateTime.UtcNow,
                updatedAt = DateTime.UtcNow
            };
            await _dbContext.Tasks.AddAsync(modelTask);
            await _dbContext.SaveChangesAsync();

            if (modelTask.id != 0)
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
            else
                return StatusCode(StatusCodes.Status200OK, new { isSuccess = false });
        }
        // GET: api/task/{id}
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.id == id);
            if (task == null)
                return NotFound(new { message = "Task not found" });

            return Ok(task);
        }

        // PUT: api/task/update/{id}
        [HttpPut]
        [Route("Update/{id}")]
        public async Task<IActionResult> Update(int id, TaskDTO obj)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.id == id);
            if (task == null)
                return NotFound(new { message = "Task not found" });

            task.title = obj.Title;
            task.description = obj.Description;
            task.status = obj.Status;
            task.userAsignedId = obj.UserAsignedId;
            task.updatedAt = DateTime.UtcNow;

            _dbContext.Tasks.Update(task);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
        }

        // DELETE: api/task/delete/{id}
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var task = await _dbContext.Tasks.FirstOrDefaultAsync(t => t.id == id);
            if (task == null)
                return NotFound(new { message = "Task not found" });

            _dbContext.Tasks.Remove(task);
            await _dbContext.SaveChangesAsync();

            return StatusCode(StatusCodes.Status200OK, new { isSuccess = true });
        }
    }
}
    