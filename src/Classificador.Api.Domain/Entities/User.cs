using Classificador.Api.Domain.Interfaces;
using Classificador.Api.SharedKernel.Shared;

namespace Classificador.Api.Domain.Entities;

public sealed record User : Entity<User>, IAggregateRoot
{
    public string Email { get; private set; }
    public string HashedPassword { get; private set; }
    public string Name { get; private set; }
    public string? Contact { get; private set; }
    public UserRole Role { get; private set; }
    public Guid IdSpecialty { get; init; }
    public Specialty? Specialty{ get; init; }
    // public ICollection<Claim> UserClaims { get; } = new List<Claim>();

    public User(string email, string hashedPassword, string name, string? contact, UserRole role, Guid idSpecialty) : base()
    {
        Email = email;
        HashedPassword = hashedPassword;
        Name = name;
        Contact = contact;
        Role = role;
        IdSpecialty = idSpecialty;
    }

    public override User Update(User user)
    {
        UpdateEmail(user.Email);
        UpdateHashedPassword(user.HashedPassword);
        UpdateName(user.Name);

        if(!string.IsNullOrEmpty(user.Contact))
        {
            UpdateContact(user.Contact!);
        }

        return this;
    }

    public User UpdateEmail(string email)
    {
        ArgumentValidator.ThrowIfNullOrEmpty(email, nameof(Email));

        if(email != Email)
        {
            Email = email;
        }
        return this;
    }

    public User UpdateHashedPassword(string password)
    {
        ArgumentValidator.ThrowIfNullOrEmpty(password, nameof(HashedPassword));

        if(password != HashedPassword)
        {
            HashedPassword = password;
        }
        return this;
    }

    public User UpdateName(string name)
    {
        ArgumentValidator.ThrowIfNullOrEmpty(name, nameof(Name));

        if(name != Name)
        {
            Name = name;
        }
        return this;
    }

    public User UpdateContact(string contact)
    {
        ArgumentValidator.ThrowIfNullOrEmpty(contact, nameof(Contact));
        if(contact != Contact)
        {
            Contact = contact;
        }
        return this;
    }

    public User UpdateRole(UserRole role)
    {
        if(role != Role)
        {
            Role = role;
        }
        return this;
    }

    public override void Validate()
    {
        ArgumentValidator.ThrowIfNullOrEmpty(Email, nameof(Email));
        ArgumentValidator.ThrowIfNullOrEmpty(HashedPassword, nameof(HashedPassword));
        ArgumentValidator.ThrowIfNullOrEmpty(Name, nameof(Name));
    }
}
