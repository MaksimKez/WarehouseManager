namespace WarehouseManager.BusinessLogic.Auth.Interfaces;

public interface IPasswordHasher
{
    string Generate(string password);
    bool Verify(string password, string hashedPassword);
}