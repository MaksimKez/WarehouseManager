using WarehouseManager.Database.Entities;

namespace WarehouseManager.Services.Models;

public class Employee
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public PositionEnum Position { get; set; }
    public bool IsFired { get; set; }
    public DateTime CreatedAt { get; set; }
}