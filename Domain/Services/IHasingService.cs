
namespace Domain.Services
{
    public interface IHasingService
    {
        string HashPassword(string password);
        bool ValidatePassword(string password, string hashedPassword);
    }
}
