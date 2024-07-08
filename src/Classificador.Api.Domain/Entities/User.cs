namespace Classificador.Api.Domain.Entities;

public sealed class User : Entity<User>, IAggregateRoot
{
    public string Email { get; private set; } = string.Empty;
    public string HashedPassword { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string? Contact { get; private set; }
    public UserRole Role { get; private set; }
    public Guid? IdSpecialty { get; private set; }
    public Specialty? Specialty{ get; init; }
    public ICollection<Classification>? Classifications { get; init; } = [];

    public User(string email, string hashedPassword, string name, UserRole role, Guid idSpecialty, string? contact) : base()
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(email, nameof(email));
        ArgumentValidator.ThrowIfNullOrWhitespace(hashedPassword, nameof(hashedPassword));
        ArgumentValidator.ThrowIfNullOrWhitespace(name, nameof(name));
        ArgumentValidator.ThrowIfNull(role, nameof(role));
        ArgumentValidator.ThrowIfNull(idSpecialty, nameof(idSpecialty));
        ArgumentValidator.ThrowIfNull(contact!, nameof(contact));

        Email = email;
        HashedPassword = hashedPassword;
        Name = name;
        Role = role;
        IdSpecialty = idSpecialty;
        Contact = contact;
    }

    public User( ){ }

    public override User Update(User entity)
    {
        Email = entity.Email;
        Name = entity.Name;
        IdSpecialty = entity.IdSpecialty;
        Contact = entity.Contact;

        return this;
    }

    public User UpdateHashedPassword(string password)
    {
        throw new NotImplementedException();
    }

}
