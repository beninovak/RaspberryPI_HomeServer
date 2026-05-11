using HomeServer.Core.DTOs;
using HomeServer.Core.DTOs.Todos;
using HomeServer.Core.Models;
using HomeServer.Server.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;

namespace HomeServer.Server.Controllers;

[Route("api/todos")]
[ApiController]
public class TodosController(ITodoRepository todoRepository) : ControllerBase {
    
    #region Todos
    [HttpGet]
    public async Task<ActionResult<List<TodoCategoryReadDTO>>> GetAllTodosAsync() {
        return Ok(await todoRepository.GetAllTodosAsync());
    }

    [HttpGet("{guid:guid}")]
    public async Task<ActionResult<TodoReadDTO>> GetTodoByIdAsync([FromRoute] Guid guid) {
        TodoReadDTO? todoDto = await todoRepository.GetTodoByGuidAsync(guid);
    
        if (todoDto is null) {
            return NotFound(new {
                message = "No todo with that guid exists.",
            });
        }
    
        return Ok(todoDto);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateTodoAsync([FromBody] TodoCreateDTO todoCreateDto) {
        await todoRepository.CreateTodoAsync(todoCreateDto);
        return Ok(new {
            message = $"Successfully added a todo with content {todoCreateDto.Content}.",
        });
    }
    
    [HttpPatch("{guid:guid}")]
    public async Task<ActionResult> UpdateTodoAsync([FromRoute] Guid guid, [FromBody] TodoUpdateDTO todoUpdateDto) {
        await todoRepository.UpdateTodoAsync(guid, todoUpdateDto);
        return Ok(new {
            message = $"Successfully changed todo.",
        });
    }

    [HttpDelete("{guid:guid}")]
    public async Task<ActionResult> DeleteTodoAsync([FromRoute] Guid guid) {
        if (await todoRepository.DeleteTodoAsync(guid)) {
            return NoContent();
        }

        return NotFound("Could not find a todo with the provided guid. No deletions occurred.");
    }
    #endregion
    
    #region Categories
    [Route("categories")]
    [HttpPost]
    public async Task<ActionResult> CreateTodoCategoryAsync([FromBody] TodoCategoryCreateDTO todoCategoryCreateDto) {
        await todoRepository.CreateTodoCategoryAsync(todoCategoryCreateDto);
        return Ok(new {
            message = $"Successfully added todo category with title \"{todoCategoryCreateDto.Title}\"",
        });
    }
    
    [HttpPatch("{guid:guid}")]
    public async Task<ActionResult> UpdateTodoCategoryAsync([FromRoute] Guid guid, [FromBody] TodoCategoryUpdateDTO todoCategoryUpdateDto) {
        await todoRepository.UpdateTodoCategoryAsync(guid, todoCategoryUpdateDto);
        return Ok(new {
            message = $"Successfully updated todo category with title \"{todoCategoryUpdateDto.Title}\".",
        });
    }
    
    [HttpDelete("{guid:guid}")]
    public async Task<ActionResult> DeleteTodoCategoryAsync([FromRoute] Guid guid) {
        if (await todoRepository.DeleteTodoCategoryAsync(guid)) {
            return NoContent();
        }

        return NotFound("Could not find a todo with the provided guid. No deletions occurred.");
    }
    #endregion
}