namespace ProjektAPBD.WebApi.Interfaces
{
    public interface IAuthRepository
    {
        Task<(string Access, string Refresh)> Login(string username, string password, CancellationToken cancellationToken = default);
        Task<(string Access, string Refresh)> Refresh(string refreshToken, CancellationToken cancellationToken = default);
    }
}
