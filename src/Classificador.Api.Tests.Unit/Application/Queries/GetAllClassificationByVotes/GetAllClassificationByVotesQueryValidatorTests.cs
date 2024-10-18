using Classificador.Api.Application.Core.Errors;

namespace Classificador.Api.Tests.Unit.Application.Queries.GetAllClassificationByVotes;

public sealed class GetAllClassificationByVotesQueryValidatorTests
{
    private readonly GetAllClassificationByVotesQueryValidator _validator;

    public GetAllClassificationByVotesQueryValidatorTests()
    {
        _validator = new GetAllClassificationByVotesQueryValidator();
    }

    [Fact]
    public void Should_HaveValidationError_WhenIdPrescribingInformationIsEmpty()
    {
        // Arrange
        var query = new GetAllClassificationByVotesQuery
        {
            IdPrescribingInformation = Guid.Empty
        };

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.IdPrescribingInformation)
            .WithErrorCode(QueryErrors.GetAllClassificationByVotesFailures.PrescribingInformationIdIsRequired.Failure)
            .WithErrorMessage(QueryErrors.GetAllClassificationByVotesFailures.PrescribingInformationIdIsRequired.Description);
    }

    [Fact]
    public void Should_NotHaveValidationError_WhenIdPrescribingInformationIsValid()
    {
        // Arrange
        var query = new GetAllClassificationByVotesQuery(Guid.NewGuid().ToString());

        // Act
        var result = _validator.TestValidate(query);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.IdPrescribingInformation);
    }
}
