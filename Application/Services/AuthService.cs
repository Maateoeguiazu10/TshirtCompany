using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Requests;
using CompanyTshirt.Application.Responses;
using CompanyTshirt.Domain.Entities;
using CompanyTshirt.Domain.Interfaces;

namespace CompanyTshirt.Application.Services
{
    public class AuthService(
        IUserRepository usersRepository,
        ITokenService tokenService,
        IPasswordHasher passwordHasher) : IAuthService
    {
        public async Task<LoginResponse?> AuthenticateAsync(
            LoginRequest request,
            CancellationToken cancellationToken = default)
        {
            var user = await usersRepository.GetByEmailAsync(
                request.Email.Trim().ToLowerInvariant(),
                cancellationToken);

            if (user == null ||
                !passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                return null;
            }

            return new LoginResponse(
                tokenService.GenerateToken(
                    user.Id,
                    user.Email,
                    user.GetType().Name));
        }

        public async Task<UserResponse> RegisterAsync(
            RegisterRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.Name) ||
                string.IsNullOrWhiteSpace(request.Surname) ||
                string.IsNullOrWhiteSpace(request.Email) ||
                string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ArgumentException(
                    "Todos los campos son obligatorios.");
            }

            if (request.Password.Length < 8)
            {
                throw new ArgumentException(
                    "La contraseña debe tener al menos 8 caracteres.");
            }

            var email = request.Email.Trim().ToLowerInvariant();

            var existingUser = await usersRepository.GetByEmailAsync(
                email,
                cancellationToken);

            if (existingUser != null)
            {
                throw new InvalidOperationException(
                    "Ya existe un usuario con ese email.");
            }

            var passwordHash = passwordHasher.Hash(request.Password);

            var customer = new Customer(
                request.Name.Trim(),
                request.Surname.Trim(),
                email,
                passwordHash);

            await usersRepository.AddAsync(
                customer,
                cancellationToken);

            return new UserResponse(
                customer.Id,
                customer.Name,
                customer.Surname,
                customer.Email,
                customer.GetType().Name);
        }
    }
}