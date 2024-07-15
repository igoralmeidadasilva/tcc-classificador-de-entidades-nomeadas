namespace Classificador.Api.Tests.Unit.Application.Commands.CreatePrescribingInformationTxt;

public sealed class CreatePrescribingInformationTxtCommandValidatorTests
{
    private readonly CreatePrescribingInformationTxtCommandValidator _validator;

    public CreatePrescribingInformationTxtCommandValidatorTests()
    {
        _validator = new CreatePrescribingInformationTxtCommandValidator();
    }


    [Fact]
    public void Should_Have_Error_When_File_Is_Null()
    {
        // Arrange
        var command = new CreatePrescribingInformationTxtCommand 
        { 
            File = null 
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.File)
            .WithErrorCode(ValidationErrors.CreatePrescribingInformationTxt.FileIsRequired.Code)
            .WithErrorMessage(ValidationErrors.CreatePrescribingInformationTxt.FileIsRequired.Message);
    }

    [Fact]
    public void Should_Have_Error_When_File_Is_Wrong_Extension()
    {
        // Arrange
        var fileMock = new Mock<IFormFile>();
        fileMock.Setup(f => f.ContentType).Returns("application/pdf");
        fileMock.Setup(f => f.FileName).Returns("test.pdf");

        var command = new CreatePrescribingInformationTxtCommand 
        { 
            File = fileMock.Object 
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldHaveValidationErrorFor(x => x.File)
            .WithErrorCode(ValidationErrors.CreatePrescribingInformationTxt.FileExtension.Code)
            .WithErrorMessage(ValidationErrors.CreatePrescribingInformationTxt.FileExtension.Message);
    }

    [Fact]
    public void Should_Not_Have_Error_When_Valid_Command()
    {
        // Arrange
        var fileMock = new Mock<IFormFile>();
        fileMock.Setup(f => f.ContentType).Returns("text/plain");
        fileMock.Setup(f => f.FileName).Returns("test.txt");

        var command = new CreatePrescribingInformationTxtCommand 
        { 
            File = fileMock.Object 
        };

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveValidationErrorFor(x => x.File);
    }


}
