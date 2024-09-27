using WarehouseManager.BusinessLogic.Models;

namespace WarehouseManager.BusinessLogic.Auth.Interfaces;

public interface IJwtProvider
{ 
    string GenerateToken(Employee employee);
    string GenerateToken(Boss boss);

}