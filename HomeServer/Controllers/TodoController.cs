using HomeServer.DTOs.Todos;
using HomeServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeServer.Controllers;

[Route("api/todos")]
[ApiController]
public class TodoController(ITodoService todoService) : ControllerBase {
    
    #region Todos
    [HttpGet]
    public async Task<ActionResult<List<TodoCategoryReadDTO>>> GetAllAsync() {
        return Ok(await todoService.GetAllAsync());
    }

    [HttpGet("{guid:guid}")]
    public async Task<ActionResult<TodoReadDTO>> GetByGuidAsync([FromRoute] Guid guid) {
        TodoReadDTO todoDto = await todoService.GetByGuidAsync(guid);
        return Ok(todoDto);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] TodoCreateDTO todoCreateDto) {
        Guid createdGuid = await todoService.CreateAsync(todoCreateDto);
        return CreatedAtRoute(nameof(GetByGuidAsync), new { guid = createdGuid });
    }

    [HttpPatch("{guid:guid}")]
    public async Task<ActionResult> UpdateAsync([FromRoute] Guid guid, [FromBody] TodoUpdateDTO todoUpdateDto) {
        await todoService.UpdateAsync(guid, todoUpdateDto);
        return Ok("Successfully changed todo.");
    }

    [HttpDelete("{guid:guid}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] Guid guid) {
        await todoService.DeleteAsync(guid);
        return NoContent();
    }
    #endregion
    
    #region Categories
    [HttpPost("categories")]
    public async Task<ActionResult> CreateCategoryAsync([FromBody] TodoCategoryCreateDTO todoCategoryCreateDto) {
        Guid createdGuid = await todoService.CreateCategoryAsync(todoCategoryCreateDto);
        return CreatedAtRoute(nameof(GetByGuidAsync), new { guid = createdGuid });
    }
    
    [HttpPatch("categories/{guid:guid}")]
    public async Task<ActionResult> UpdateCategoryAsync([FromRoute] Guid guid, [FromBody] TodoCategoryUpdateDTO todoCategoryUpdateDto) {
        await todoService.UpdateCategoryAsync(guid, todoCategoryUpdateDto);
        return Ok(new {
            message = $"Successfully updated todo category with title \"{todoCategoryUpdateDto.Title}\".",
        });
    }
    
    [HttpDelete("categories/{guid:guid}")]
    public async Task<ActionResult> DeleteCategoryAsync([FromRoute] Guid guid) {
        await todoService.DeleteCategoryAsync(guid);
        return NoContent();
    }
    #endregion
}