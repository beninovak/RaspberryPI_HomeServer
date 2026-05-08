namespace HomeServer.Core.DTOs;

public class TodoByCategoryDTO {
    public required string Title { get; set; }
    public List<TodoDTO>? Todos { get; set; } 
}