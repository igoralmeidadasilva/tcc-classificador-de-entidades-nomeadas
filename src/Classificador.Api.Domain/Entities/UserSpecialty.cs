namespace Classificador.Api.Domain.Entities;

public sealed record UserSpecialty
{
    public int UserId { get; init; }
    public User? User { get; init; }
    public int SpecialtyId { get; init; }
    public Specialty? Specialty { get; init; }
    public UserSpecialty(int userId, int specialtyId)
    {
        UserId = userId;
        SpecialtyId = specialtyId;
    }
}
