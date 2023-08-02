
namespace Application.Ports
{
    public interface ICryptographyService
    {
        string GenerateSalt();
        string HashPassword(string password, string salt);
    }
}
