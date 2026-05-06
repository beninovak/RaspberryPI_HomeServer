using HomeServer.Core.Models;
using HomeServer.Server.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HomeServer.Server.Controllers;

[Route("api/todos")]
[ApiController]
public class TodosController(ITodoRepository todoRepository) : ControllerBase {
    [HttpGet]
    public async Task<ActionResult<List<TodoCategory>>> GetAllTodos() {
        List<TodoCategory> data = await todoRepository.GetAllTodos();
        return Ok(data);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Todo>> GetTodoById(uint id) {
        Todo? todo = await todoRepository.GetTodoById(id);
    
        if (todo is null) {
            return NotFound(new {
                message = "No todo with that id exists.",
            });
        }
    
        return Ok(todo);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateTodo([FromBody] Todo todo) {
        await todoRepository.CreateTodo(todo);
        return Ok(new {
            message = $"Successfully added a todo with content {todo.Content}.",
        });
    }
    
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateTodo([FromBody] Todo todo) {
        await todoRepository.UpdateTodo(todo);
        return Ok(new {
            message = $"Successfully changed todo.",
        });
    }
    
    #region Sections
    [Route("sections")]
    [HttpPost]
    public async Task<ActionResult> CreateTodoSection([FromBody] TodoCategory category) {
        await todoRepository.CreateTodoSection(category);
        return Ok(new {
            message = $"Successfully added a todo category with id {category.Id} and title \"{category.Title}\"",
        });
    }
    
    [HttpPatch]
    public async Task<ActionResult> UpdateTodoSection([FromBody] TodoCategory category) {
        await todoRepository.UpdateTodoSection(category);
        return Ok(new {
            message = $"Successfully updated todo category with title \"{category.Title}\".",
        });
    }
    #endregion
}