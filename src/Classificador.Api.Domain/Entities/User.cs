using Classificador.Api.Domain.Core.Enums;
using Classificador.Api.Domain.Core.Interfaces;
using Classificador.Api.Domain.Core.Interfaces.Services;

namespace Classificador.Api.Domain.Entities;

public sealed class User : Entity<User>, ISoftDeletableEntity, IAggregateRoot
{
    public string Email { get; private set; } = string.Empty;
    public string HashedPassword { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public string? Contact { get; private set; }
    public UserRole Role { get; private set; }
    public Guid? IdSpecialty { get; private set; }
    public Specialty? Specialty{ get; init; }
    public ICollection<Classification>? Classifications { get; init; } = [];
    public bool IsDeleted {get; private set; }
    public DateTime? DeletedOnUtc { get; private set; }

    public User() {} // ORM

    private User(Guid id, DateTime createdOnUtc, string email, string hashedPassword, string name, UserRole role, Guid? idSpecialty, string? contact) 
        : base(id, createdOnUtc)
    {
        Email = email;
        HashedPassword = hashedPassword;
        Name = name;
        Role = role;
        IdSpecialty = idSpecialty;
        Contact = contact;
    }

    public static User Create(string email, string hashedPassword, string name, Guid? idSpecialty, string? contact)
    {
        ArgumentValidator.ThrowIfNullOrWhitespace(email, nameof(Email));
        ArgumentValidator.ThrowIfNullOrWhitespace(hashedPassword, nameof(HashedPassword));
        ArgumentValidator.ThrowIfNullOrWhitespace(name, nameof(Name));
        ArgumentValidator.ThrowIfNull(contact!, nameof(Contact));

        return new(Guid.NewGuid(), DateTime.UtcNow, email, hashedPassword, name, UserRole.Padrao, idSpecialty, contact);
    }

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
        ArgumentValidator.ThrowIfNullOrWhitespace(password, nameof(password));
        HashedPassword = password;

        return this;
    }

    public User UpdateRole(UserRole role)
    {
        Role = role;

        return this;
    }

    public void Delete()
    {
        IsDeleted = true;
        DeletedOnUtc = DateTime.UtcNow;
    }

    public void Restore()
    {
        IsDeleted = false;
    }
}
