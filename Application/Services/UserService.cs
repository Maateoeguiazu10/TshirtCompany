using CompanyTshirt.Application.Abstractions;
using CompanyTshirt.Application.Responses;
using CompanyTshirt.Domain.Interfaces;


namespace CompanyTshirt.Application.Services
{
    public class UserService(IUserRepository usersRepository) : IUserService
    {
        public async Task<IReadOnlyList<UserResponse>> GetAllAsync(
            CancellationToken cancellationToken = default)
        {
            var users = await usersRepository.GetAllAsync(cancellationToken);

            return users.Select(user => new UserResponse(
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.GetType().Name
            )).ToList();
        }

        public async Task<UserResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default)
        {
            var user = await usersRepository.GetByIdAsync(id, cancellationToken);

            if (user == null)
                return null;

            return new UserResponse(
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.GetType().Name);
        }

        public async Task<UserResponse?> UpdateAsync(
            Guid id,
            string name,
            string surname,
            string email,
            CancellationToken cancellationToken = default)
        {
            var user = await usersRepository.GetByIdAsync(id, cancellationToken);

            if (user == null)
                return null;

            var normalizedEmail = email.Trim().ToLowerInvariant();

            var existingUser = await usersRepository.GetByEmailAsync(
                normalizedEmail, cancellationToken);

            if (existingUser != null && existingUser.Id != user.Id)
                throw new InvalidOperationException("Ese email ya está registrado.");

            user.UpdateProfile(name, surname, normalizedEmail);

            await usersRepository.UpdateAsync(user, cancellationToken);

            return new UserResponse(
                user.Id,
                user.Name,
                user.Surname,
                user.Email,
                user.GetType().Name);
        }
    }
}