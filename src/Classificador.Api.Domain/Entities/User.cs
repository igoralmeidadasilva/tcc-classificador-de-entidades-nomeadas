using System.Security.Claims;
using Classificador.Api.Domain.Interfaces;
using Classificador.Api.SharedKernel;

namespace Classificador.Api.Domain.Entities;

public sealed record User : BaseEntity<User>, IEntity<User>,IAggregateRoot
{
    public string Email { get; private set; }
    public string HashedPassword { get; private set; }
    public string Name { get; private set; }
    public UserRole Role { get; private set; }
    public string? Contact { get; private set; }
    public bool IsActive{ get; private set; }
    public ICollection<UserSpecialty>? Specializations { get; private set; }
    public ICollection<Claim> UserClaims { get; } = new List<Claim>();

    public User(string email,
                string hashedPassword,
                string name,
                string? contact = "",
                bool isActive = false) : base(Guid.NewGuid())
    {
        Email = email;
        HashedPassword = hashedPassword;
        Name = name;
        Role = UserRole.Standard;
        Contact = contact;
        IsActive = isActive;

        Validate();
    }


    public override User Update(User entity)
    {
        throw new NotImplementedException();
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
        ArgumentValidator.ThrowIfNullOrEmpty(HashedPassword, nameof(HashedPassword));
        if(password != HashedPassword)
        {
            HashedPassword = password;
        }
        return this;
    }

    public User UpdateName(string name)
    {
        ArgumentValidator.ThrowIfNullOrEmpty(Name, nameof(Name));
        if(name != Name)
        {
            Name = name;
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

    public User UpdateContact(string contact)
    {
        throw new NotImplementedException();
    }

    public User UpdateIsActive(bool isActive)
    {
        throw new NotImplementedException();
    }

    public override void Validate()
    {
        ArgumentValidator.ThrowIfNullOrEmpty(Email, nameof(Email));
        ArgumentValidator.ThrowIfNullOrEmpty(HashedPassword, nameof(HashedPassword));
        ArgumentValidator.ThrowIfNullOrEmpty(Name, nameof(Name));
    }
}
