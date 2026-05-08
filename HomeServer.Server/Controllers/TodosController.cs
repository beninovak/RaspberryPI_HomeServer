using HomeServer.Core.DTOs;
using HomeServer.Core.Models;
using HomeServer.Server.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HomeServer.Server.Controllers;

[Route("api/todos")]
[ApiController]
public class TodosController(ITodoRepository todoRepository) : ControllerBase {
    
    #region Todos
    [HttpGet]
    public async Task<ActionResult<List<TodoByCategoryDTO>>> GetAllTodos() {
        return Ok(await todoRepository.GetAllTodos());
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<TodoDTO>> GetTodoById(uint id) {
        TodoDTO? todoDto = await todoRepository.GetTodoById(id);
    
        if (todoDto is null) {
            return NotFound(new {
                message = "No todo with that id exists.",
            });
        }
    
        return Ok(todoDto);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateTodo([FromBody] TodoDTO todoDto) {
        await todoRepository.CreateTodo(todoDto);
        return Ok(new {
            message = $"Successfully added a todo with content {todoDto.Content}.",
        });
    }
    
    [HttpPatch("{id:int}")]
    public async Task<ActionResult> UpdateTodo([FromBody] TodoDTO todoDto) {
        await todoRepository.UpdateTodo(todoDto);
        return Ok(new {
            message = $"Successfully changed todo.",
        });
    }
    #endregion
    
    #region Categories
    [Route("categories")]
    [HttpPost]
    public async Task<ActionResult> CreateTodoCategory([FromBody] TodoCategoryDTO category) {
        await todoRepository.CreateTodoCategory(category);
        return Ok(new {
            message = $"Successfully added todo category with title \"{category.Title}\"",
        });
    }
    
    [HttpPatch]
    public async Task<ActionResult> UpdateTodoCategory([FromBody] TodoCategoryDTO category) {
        await todoRepository.UpdateTodoCategory(category);
        return Ok(new {
            message = $"Successfully updated todo category with title \"{category.Title}\".",
        });
    }
    #endregion
}