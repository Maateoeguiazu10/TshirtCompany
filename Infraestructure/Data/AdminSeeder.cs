using CompanyTshirt.Domain.Entities;
using CompanyTshirt.Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace CompanyTshirt.Infrastructure.Data
{
	public class AdminSeeder(
		IUserRepository usersRepository,
		IPasswordHasher passwordHasher,
		IConfiguration configuration)
	{
		public async Task SeedAsync(
			CancellationToken cancellationToken = default)
		{
			var enabled = bool.TryParse(configuration["AdminSeed:Enabled"],out var result) && result;

			if (!enabled)
				return;

			var name = configuration["AdminSeed:Name"];
			var surname = configuration["AdminSeed:Surname"];
			var email = configuration["AdminSeed:Email"];
			var password = configuration["AdminSeed:Password"];

			if (string.IsNullOrWhiteSpace(name) ||
				string.IsNullOrWhiteSpace(surname) ||
				string.IsNullOrWhiteSpace(email) ||
				string.IsNullOrWhiteSpace(password))
			{
				throw new InvalidOperationException(
					"Falta la configuración del administrador inicial.");
			}

			email = email.Trim().ToLowerInvariant();

			var existingUser = await usersRepository.GetByEmailAsync(
				email,
				cancellationToken);

			if (existingUser != null)
				return;

			var admin = new Admin(
				name.Trim(),
				surname.Trim(),
				email,
				passwordHasher.Hash(password));

			await usersRepository.AddAsync(
				admin,
				cancellationToken);
		}
	}
}