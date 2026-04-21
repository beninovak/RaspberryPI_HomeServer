using HomeServer.Server.Data;
using HomeServer.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HomeServer.Core;
using HomeServer.Server.Utils;

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
        return await DB.TodoSections
            .Include(section => section.Todos)
            .OrderBy(section => section.Order)
            .ThenBy(section => section.Title)
            .ThenBy(section => section.Todos.Any() ? section.Todos.First().Order : 0)
            .ToListAsync();
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
    
    /*[HttpGet("search/{query}")]
    public async Task<ActionResult<List<TodoDTO>>> GetTodosByQueryString(string query) {
        query = query.ToLower();

        List<Todo> todos = await DB.Todos
            .Where(todo => todo.Content.ToLower().Contains(query))
            .ToListAsync();

        if (!todos.Any()) {
            return NotFound(new {
                message = "No todos with that id exists."
            });
        }

        List<TodoDTO> todoDTOs = new();

        foreach (Todo todo in todos) {
            todoDTOs.Add(TodoUtil.TodoToDTO(todo));
        }

        return Ok(todoDTOs);
    }*/

    [HttpPost]
    public async Task<ActionResult> CreateTodo([FromBody] Todo todoDTO) {
        DB.Todos.Add(todoDTO);
        await DB.SaveChangesAsync();

        return Ok(new {
            message = $"Successfully added a todo with content {todoDTO.Content}."
        });
    }
    
    [HttpPatch("{id:int}/status")]
    public async Task<ActionResult> UpdateTodoStatus(uint id) {
        Todo? dbTodo = await DB.Todos.FindAsync(id);

        if (dbTodo is null) {
            return NotFound("No todo with that id exists!");
        }

        dbTodo.IsDone = !dbTodo.IsDone;
        DB.Todos.Update(dbTodo);
        await DB.SaveChangesAsync();
        
        return Ok(new {
            message = $"Successfully changed todo status."
        });
    }

    // [HttpPatch("{id}")] 
    // public async Task<ActionResult> UpdateTodo(TodoDTO todoDTO) {
    //     Todo? todo = DB.Todos.Find(id);
    //     if (todo is null) {
    //         return NotFound(new {
    //             message = "No todo with that id exists.",
    //         });
    //     }
    // }


    #region Sections
    
    /*[Route("sections")]
    [HttpGet]
    public async Task<ActionResult<List<TodoSection>>> GetTodoSections() {
        return await DB.TodoSections.ToListAsync();
    }*/

    /*[HttpGet("sections/{id}")]
    public async Task<ActionResult<TodoSection>> GetTodoSection(uint id) {
        TodoSection? section = await DB.TodoSections.FirstOrDefaultAsync(t => t.Id == id);

        if (section is null) {
            return NotFound(new {
                message = "No todo section with that id exists."
            });
        }

        return Ok(section);
    }*/

    /*[Route("sections")]
    [HttpPost]
    public async Task<ActionResult<List<Todo>>> GetTodoSections([FromBody] TodoSection section) {
        DB.TodoSections.Add(section);
        await DB.SaveChangesAsync();

        return Ok(new {
            message = $"Successfully added a todo section with id {section.Id} and title \"{section.Title}\""
        });
    }*/

    #endregion
}