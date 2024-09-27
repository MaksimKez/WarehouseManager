using WarehouseManager.Database.Entities;

namespace WarehouseManager.Dtos;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public PositionEnum Position { get; set; }
    public bool IsFired { get; set; }
    public DateTime CreatedAt { get; set; }
}