using HomeServer.Server.Data;
using HomeServer.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Server.Controllers;

[Route("api/todo_sections")]
[ApiController]
public class TodoSectionsController : ControllerBase {
    private readonly MainDbContext DB;

    public TodoSectionsController(MainDbContext db) {
        DB = db;
    }

    // GET: api/TodoSections
    [HttpGet]
    public async Task<ActionResult<List<TodoSection>>> GetTodoSections() {
        return await DB.TodoSections.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TodoSection>> GetTodoSection(uint id) {
        TodoSection? section = await DB.TodoSections.FirstOrDefaultAsync(t => t.Id == id);

        if (section is null) {
            return NotFound(new {
                message = "No todo section with that id exists."
            });
        }

        return Ok(section);
    }

    [HttpPost]
    public async Task<ActionResult<List<Todo>>> GetTodoSections([FromBody] TodoSection section) {
        DB.TodoSections.Add(section);
        await DB.SaveChangesAsync();

        return Ok(new {
            message = $"Successfully added a todo section with id {section.Id} and title \"{section.Title}\""
        });
    }
}