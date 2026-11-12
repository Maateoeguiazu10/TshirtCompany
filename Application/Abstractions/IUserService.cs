using CompanyTshirt.Application.Responses;


namespace CompanyTshirt.Application.Abstractions
{
    public interface IUserService
    {
        Task<IReadOnlyList<UserResponse>> GetAllAsync(
            CancellationToken cancellationToken = default);

        Task<UserResponse?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken = default);

        Task<UserResponse?> UpdateAsync(
            Guid id,
            string name,
            string surname,
            string email,
            CancellationToken cancellationToken = default);
    }
}