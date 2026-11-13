namespace CompanyTshirt.Application.Responses
{
    public record UserResponse(
        Guid Id,
        string Name,
        string Surname,
        string Email,
        string Role);
}
