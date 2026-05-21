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
    public async Task<ActionResult<List<ExpenseCategoryReadDTO>>> GetAllExpensesAsync() {
        return Ok(await expenseRepository.GetAllExpensesAsync());
    }
    
    [HttpGet("{guid:guid}")]
    public async Task<ActionResult<ExpenseReadDTO>> GetExpenseByIdAsync([FromRoute] Guid guid) {
        ExpenseReadDTO? expenseDto = await expenseRepository.GetExpenseByGuidAsync(guid);
    
        if (expenseDto is null) {
            return NotFound(new {
                message = "No expense with that guid exists.",
            });
        }
    
        return Ok(expenseDto);
    }
    
    [HttpPost]
    public async Task<ActionResult> CreateExpenseAsync([FromBody] ExpenseCreateDTO expenseCreateDto) {
        await expenseRepository.CreateExpenseAsync(expenseCreateDto);
        return Ok(new {
            message = $"Successfully added an expense with title {expenseCreateDto.Title}.",
        });
    }

    [HttpPatch("{guid:guid}")]
    public async Task<ActionResult> UpdateExpenseAsync([FromRoute] Guid guid, [FromBody] ExpenseUpdateDTO expenseUpdateDto) {
        if (await expenseRepository.UpdateExpenseAsync(guid, expenseUpdateDto)) {
            return NotFound("Could not find an expense with the provided guid. No update occurred.");
        }
        
        return Ok("Successfully changed expense.");
    }

    [HttpDelete("{guid:guid}")]
    public async Task<ActionResult> DeleteExpenseAsync([FromRoute] Guid guid) {
        if (await expenseRepository.DeleteExpenseAsync(guid)) {
            return NoContent();
        }

        return NotFound("Could not find a expense with the provided guid. No deletion occurred.");
    }
    #endregion
    
    #region Categories
    [HttpPost("categories")]
    public async Task<ActionResult> CreateExpenseCategoryAsync([FromBody] ExpenseCategoryCreateDTO expenseCategoryCreateDto) {
        await expenseRepository.CreateExpenseCategoryAsync(expenseCategoryCreateDto);
        return Ok(new {
            message = $"Successfully added an expense category with title \"{expenseCategoryCreateDto.Title}\"",
        });
    }
    
    [HttpPatch("categories/{guid:guid}")]
    public async Task<ActionResult> UpdateExpenseCategoryAsync([FromRoute] Guid guid, [FromBody] ExpenseCategoryUpdateDTO expenseCategoryUpdateDto) {
        if (await expenseRepository.UpdateExpenseCategoryAsync(guid, expenseCategoryUpdateDto)) {
            return Ok(new {
                message = $"Successfully updated an expense category with title \"{expenseCategoryUpdateDto.Title}\".",
            });
        }

        return NotFound("Could not find an expense category with the provided guid. No updates occurred.");
    }
    
    [HttpDelete("categories/{guid:guid}")]
    public async Task<ActionResult> DeleteExpenseCategoryAsync([FromRoute] Guid guid) {
        if (await expenseRepository.DeleteExpenseCategoryAsync(guid)) {
            return NoContent();
        }

        return NotFound("Could not find an expense category with the provided guid. No deletions occurred.");
    }
    #endregion

    #region Targets
    [HttpPost("targets")]
    public async Task<ActionResult> CreateExpenseTargetAsync([FromBody] ExpenseTargetCreateDTO expenseTargetCreateDto) {
        await expenseRepository.CreateExpenseTargetAsync(expenseTargetCreateDto);
        return Ok(new {
            message = $"Successfully added an expense target with title \"{expenseTargetCreateDto.Title}\"",
        });
    }
    
    [HttpPatch("targets/{guid:guid}")]
    public async Task<ActionResult> UpdateExpenseTargetAsync([FromRoute] Guid guid, [FromBody] ExpenseTargetUpdateDTO expenseTargetUpdateDto) {
        if (await expenseRepository.UpdateExpenseTargetAsync(guid, expenseTargetUpdateDto)) {
            return Ok(new {
                message = $"Successfully updated expense target with title \"{expenseTargetUpdateDto.Title}\".",
            });
        }

        return NotFound("Could not find an expense target with the provided guid. No updates occurred.");
    }
    
    [HttpDelete("targets/{guid:guid}")]
    public async Task<ActionResult> DeleteExpenseTargetAsync([FromRoute] Guid guid) {
        if (await expenseRepository.DeleteExpenseTargetAsync(guid)) {
            return NoContent();
        }

        return NotFound("Could not find an expense target with the provided guid. No deletions occurred.");
    }
    #endregion
}