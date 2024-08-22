namespace Classificador.Api.Tests.Unit.Application.Queries.GetNamedEntityByPrescribingInformationId;

public sealed class GetNamedEntityByPrescribingInformationIdQueryValidatorTests
{
    private readonly GetNamedEntityByPrescribingInformationIdQueryValidator _validator;

    public GetNamedEntityByPrescribingInformationIdQueryValidatorTests()
    {
        _validator = new GetNamedEntityByPrescribingInformationIdQueryValidator();
    }

    [Fact]
    public void Validator_IdPrescribingInformationIsEmpty_ShouldHaveError()
    {
        // Arrange
        string idPrescribingInformation = Guid.Empty.ToString();
        string idUser = Guid.NewGuid().ToString();

        var query = new GetNamedEntityByPrescribingInformationIdQuery(idPrescribingInformation, idUser);

        // Act & Assert
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.IdPrescribingInformation)
            .WithErrorCode(QueryErrors.GetNamedEntityByPrescribingInformationIdFailures.PrescribingInformationIdIsRequired.Failure)
            .WithErrorMessage(QueryErrors.GetNamedEntityByPrescribingInformationIdFailures.PrescribingInformationIdIsRequired.Description);
    }

    [Fact]
    public void Validator_IdUserIsEmpty_ShouldHaveError()
    {
        // Arrange
        string idPrescribingInformation = Guid.NewGuid().ToString();
        string idUser = Guid.Empty.ToString();

        var query = new GetNamedEntityByPrescribingInformationIdQuery(idPrescribingInformation, idUser);

        // Act & Assert
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.IdUser)
            .WithErrorCode(QueryErrors.GetNamedEntityByPrescribingInformationIdFailures.UserIdIsRequired.Failure)
            .WithErrorMessage(QueryErrors.GetNamedEntityByPrescribingInformationIdFailures.UserIdIsRequired.Description);
    }

    [Fact]
    public void Validator_ValidQuery_ShouldNotHaveErrors()
    {
        // Arrange
        string idPrescribingInformation = Guid.NewGuid().ToString();
        string idUser = Guid.NewGuid().ToString();

        var query = new GetNamedEntityByPrescribingInformationIdQuery(idPrescribingInformation, idUser);

        // Act & Assert
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
