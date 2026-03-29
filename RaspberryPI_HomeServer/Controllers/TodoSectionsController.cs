using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaspberryPI_HomeServer.Data;
using RaspberryPI_HomeServer.Models;

namespace RaspberryPI_HomeServer.Controllers
{
    [Route("api/todo_sections")]
    [ApiController]
    public class TodoSectionsController : ControllerBase
    {
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
                NotFound(new {
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
}
