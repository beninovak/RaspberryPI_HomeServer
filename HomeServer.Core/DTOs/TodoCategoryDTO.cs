namespace HomeServer.Core.DTOs;

public class TodoCategoryDTO {
    public required Guid Guid { get; init; }
    public required uint Order { get; init; }
    public required string Title { get; init; }
    
    public Guid? CreatedBy { get; init; }
    public required DateTime CreatedAt { get; init; } = DateTime.UtcNow;
    
    public Guid? UpdatedBy { get; init; }
    public DateTime? UpdatedAt { get; init; }
}