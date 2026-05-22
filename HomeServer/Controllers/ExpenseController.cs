using HomeServer.DTOs.Expenses;
using HomeServer.Repositories;
using HomeServer.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeServer.Controllers;

[Route("api/expenses")]
[ApiController]
public class ExpenseController(IExpenseService expenseService) : ControllerBase {
    
    #region Expenses
    [HttpGet]
    public async Task<ActionResult<List<ExpenseCategoryReadDTO>>> GetAllAsync() {
        return Ok(await expenseService.GetAllAsync());
    }
    
    [HttpGet("{guid:guid}")]
    public async Task<ActionResult<ExpenseReadDTO>> GetByGuidAsync([FromRoute] Guid guid) {
        ExpenseReadDTO expenseDto = await expenseService.GetByGuidAsync(guid);
        return Ok(expenseDto);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateAsync([FromBody] ExpenseCreateDTO createDto) {
        Guid createdGuid = await expenseService.CreateAsync(createDto);
        return CreatedAtRoute(nameof(GetByGuidAsync), new { guid = createdGuid });
    }

    [HttpPatch("{guid:guid}")]
    public async Task<ActionResult> UpdateAsync([FromRoute] Guid guid, [FromBody] ExpenseUpdateDTO updateDto) {
        await expenseService.UpdateAsync(guid, updateDto);
        return Ok("Successfully changed expense.");
    }

    [HttpDelete("{guid:guid}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] Guid guid) {
        await expenseService.DeleteAsync(guid);
        return NoContent();
    }
    #endregion
    
    #region Categories
    [HttpGet("categories/{guid:guid}")]
    public async Task<ActionResult<ExpenseCategoryReadDTO>> GetCategoryByGuidAsync([FromRoute] Guid guid) {
        ExpenseCategoryReadDTO categoryDto = await expenseService.GetCategoryByGuidAsync(guid);
        return Ok(categoryDto);
    }
    
    [HttpPost("categories")]
    public async Task<ActionResult> CreateCategoryAsync([FromBody] ExpenseCategoryCreateDTO categoryCreateDto) {
        Guid createdGuid = await expenseService.CreateCategoryAsync(categoryCreateDto);
        return CreatedAtRoute(nameof(GetCategoryByGuidAsync), new { guid = createdGuid });
    }
    
    [HttpPatch("categories/{guid:guid}")]
    public async Task<ActionResult> UpdateCategoryAsync([FromRoute] Guid guid, [FromBody] ExpenseCategoryUpdateDTO categoryUpdateDto) {
        await expenseService.UpdateCategoryAsync(guid, categoryUpdateDto);
        return Ok("Successfully changed category.");
    }
    
    [HttpDelete("categories/{guid:guid}")]
    public async Task<ActionResult> DeleteCategoryAsync([FromRoute] Guid guid) {
        await expenseService.DeleteCategoryAsync(guid);
        return NoContent();
    }
    #endregion

    #region Targets
    [HttpGet("targets/{guid:guid}")]
    public async Task<ActionResult<ExpenseTargetReadDTO>> GetTargetByGuidAsync([FromRoute] Guid guid) {
        ExpenseTargetReadDTO targetDto = await expenseService.GetTargetByGuidAsync(guid);
        return Ok(targetDto);
    }
    
    [HttpPost("targets")]
    public async Task<ActionResult> CreateTargetAsync([FromBody] ExpenseTargetCreateDTO targetCreateDto) {
        Guid createdGuid = await expenseService.CreateTargetAsync(targetCreateDto);
        return CreatedAtRoute(nameof(GetTargetByGuidAsync), new { guid = createdGuid });
    }
    
    [HttpPatch("targets/{guid:guid}")]
    public async Task<ActionResult> UpdateTargetAsync([FromRoute] Guid guid, [FromBody] ExpenseTargetUpdateDTO targetUpdateDto) {
        await expenseService.UpdateTargetAsync(guid, targetUpdateDto);
        return Ok("Successfully changed target.");
    }
    
    [HttpDelete("targets/{guid:guid}")]
    public async Task<ActionResult> DeleteTargetAsync([FromRoute] Guid guid) {
        await expenseService.DeleteTargetAsync(guid);
        return NoContent();
    }
    #endregion
}