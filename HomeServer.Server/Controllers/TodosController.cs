using HomeServer.Server.Data;
using HomeServer.Core.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Server.Controllers;

[Route("api/todos")]
[ApiController]
public class TodosController : ControllerBase {
    private readonly MainDbContext DB;

    public TodosController(MainDbContext db) {
        DB = db;
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoSection>>> GetTodos() {
        var data = await DB.TodoSections
            .Include(section => section.Todos)
            .OrderBy(section => section.Order)
            .ThenBy(section => section.Title)
            .ThenBy(section => section.Todos.Any() ? section.Todos.First().Order : 0)
            .ToListAsync();

        if (data.Any()) {
            return data;
        }
        
        return Ok();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Todo>> GetTodo(uint id) {
        Todo? todo = await DB.Todos.FindAsync(id);

        if (todo is null) {
            return NotFound(new {
                message = "No todo with that id exists."
            });
        }

        return Ok(todo);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateTodo([FromBody] Todo todo) {
        DB.Todos.Add(todo);
        await DB.SaveChangesAsync();

        return Ok(new {
            message = $"Successfully added a todo with content {todo.Content}."
        });
    }
    
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateTodo([FromBody] Todo todo) {
        DB.Todos.Update(todo);
        await DB.SaveChangesAsync();
        
        return Ok(new {
            message = $"Successfully changed todo."
        });
    }
    
    #region Sections
    [Route("sections")]
    [HttpPost]
    public async Task<ActionResult<List<Todo>>> CreateTodoSection([FromBody] TodoSection section) {
        DB.TodoSections.Add(section);
        await DB.SaveChangesAsync();

        return Ok(new {
            message = $"Successfully added a todo section with id {section.Id} and title \"{section.Title}\""
        });
    }
    
    [HttpPatch]
    public async Task<ActionResult<List<Todo>>> UpdateTodoSection([FromBody] TodoSection section) {
        DB.TodoSections.Update(section);
        await DB.SaveChangesAsync();

        return Ok(new {
            message = $"Successfully updated todo section with title \"{section.Title}\"."
        });
    }
    #endregion
}