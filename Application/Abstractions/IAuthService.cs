using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;

namespace CompanyTshirt.Application.Abstractions
{
    public interface IAuthService
    {
        Task<LoginResponse?> AuthenticateAsync(
            LoginRequest request,
            CancellationToken cancellationToken = default);

        Task<UserResponse> RegisterAsync(
            RegisterRequest request,
            CancellationToken cancellationToken = default);
    }
}