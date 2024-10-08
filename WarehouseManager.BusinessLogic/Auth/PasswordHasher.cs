using WarehouseManager.BusinessLogic.Auth.Interfaces;

namespace WarehouseManager.BusinessLogic.Auth;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password) => BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool Verify(string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.Verify(password, hashedPassword);
}