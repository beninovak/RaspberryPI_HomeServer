using HomeServer.Server.Data;
using HomeServer.Server.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HomeServer.Server.Controllers
{
    [Route("api/todos")]
    [ApiController]
    public class TodosController : ControllerBase {
        private readonly MainDbContext DB;

        public TodosController(MainDbContext db) {
            DB = db;
        }

        [HttpGet]
        public async Task<ActionResult<List<Todo>>> GetTodos() {
            return await DB.Todos.ToListAsync();
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(uint id) {
             Todo? todo = await DB.Todos.FindAsync(id);
            
            if (todo is null) {
                NotFound(new {
                    message = "No todo with that id exists."
                });
            }
            
            return Ok(todo);
        }
        
        [HttpPost]
        public async Task<ActionResult> CreateTodo([FromBody] Todo todo) {
            DB.Todos.Add(todo);
            await DB.SaveChangesAsync();
            
            return Ok(new { // TODO - Baje bi mogu uporabt CreatedAtRoute()??? Check...
                message = $"Successfully added a todo with id {todo.Id}"
            });
        }

        // [HttpPatch("{id}")] 
        // public async Task<ActionResult> UpdateTodo(uint id) {
        //     
        // }
    }
}
