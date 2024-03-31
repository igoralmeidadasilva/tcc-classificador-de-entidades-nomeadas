using System.Security.Claims;
using Classificador.Api.Domain.Interfaces;
using Classificador.Api.SharedKernel.Shared;

namespace Classificador.Api.Domain.Entities;

public sealed record User : BaseEntity<User>, IEntity<User>, IAggregateRoot
{
    public string Email { get; private set; }
    public string HashedPassword { get; private set; }
    public string Name { get; private set; }
    public UserRole Role { get; private set; }
    public string? Contact { get; private set; }
    public bool IsActive{ get; private set; }
    public ICollection<UserSpecialty>? Specializations { get; private set; }
    public ICollection<Claim> UserClaims { get; } = new List<Claim>();

    public User(
                    Guid id,
                    string email,
                    string hashedPassword,
                    string name,
                    string? contact = "") : base(id)
        {
            Email = email;
            HashedPassword = hashedPassword;
            Name = name;
            Role = UserRole.Standard;
            Contact = contact;
            IsActive = false;

            Validate();
        }

        public User(
                    string email,
                    string hashedPassword,
                    string name,
                    string? contact = "") : base(Guid.Empty)
        {
            Email = email;
            HashedPassword = hashedPassword;
            Name = name;
            Role = UserRole.Standard;
            Contact = contact;
            IsActive = false;

            Validate();
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

    public override void Validate()
    {
        ArgumentValidator.ThrowIfNullOrEmpty(Email, nameof(Email));
        ArgumentValidator.ThrowIfNullOrEmpty(HashedPassword, nameof(HashedPassword));
        ArgumentValidator.ThrowIfNullOrEmpty(Name, nameof(Name));
    }
}
