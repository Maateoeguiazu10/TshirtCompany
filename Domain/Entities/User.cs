namespace CompanyTshirt.Domain.Entities
{
    public abstract class User
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public string Surname { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;

        protected User()
        {
        }

        protected User(string name, string surname, string email, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("The email is mandatory.", nameof(email));

            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("The hash of password is mandatory.", nameof(passwordHash));

            Id = Guid.NewGuid();
            Name = name;
            Surname = surname;
            Email = email;
            PasswordHash = passwordHash;
        }

        public void UpdateProfile(string name, string surname, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("El apellido es obligatorio.");

            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("El email es obligatorio.");

            Name = name.Trim();
            Surname = surname.Trim();
            Email = email.Trim().ToLowerInvariant();
        }
    }
}