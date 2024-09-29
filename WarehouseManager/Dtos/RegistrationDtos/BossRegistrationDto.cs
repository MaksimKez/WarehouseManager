using WarehouseManager.Database.Entities;

namespace WarehouseManager.Dtos.RegistrationDtos;

public class BossRegistrationDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}