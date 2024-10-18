using Classificador.Api.Domain.Core.Errors;
using Classificador.Api.Domain.Core.Interfaces.Repositories.Persistence;
using Classificador.Api.Domain.Core.Interfaces.Repositories.ReadOnly;

namespace Classificador.Api.Tests.Unit.Application.Commands.CreateSpecialty;

public sealed class CreateSpecialtyCommandHandlerTests
{
    private readonly Mock<ILogger<CreateSpecialtyCommandHandler>> _loggerMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<ISpecialtyReadOnlyRepository> _specialtyReadOnlyRepositoryMock;
    private readonly Mock<ISpecialtyPersistenceRepository> _specialtyPersistenceRepositoryMock;
    private readonly CreateSpecialtyCommandHandler _handler;

    public CreateSpecialtyCommandHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreateSpecialtyCommandHandler>>();
        _mapperMock = new Mock<IMapper>();
        _specialtyReadOnlyRepositoryMock = new Mock<ISpecialtyReadOnlyRepository>();
        _specialtyPersistenceRepositoryMock = new Mock<ISpecialtyPersistenceRepository>();
        
        _handler = new CreateSpecialtyCommandHandler
        (
            _loggerMock.Object,
            _mapperMock.Object,
            _specialtyReadOnlyRepositoryMock.Object,
            _specialtyPersistenceRepositoryMock.Object
        );
    }

    [Fact]
    public async Task Handle_SpecialtyNameAlreadyExists_ReturnsFailureResult()
    {
        // Arrange
        var command = new CreateSpecialtyCommand("Existing Specialty", string.Empty);
        _specialtyReadOnlyRepositoryMock.Setup(repo => repo.ExistsByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsSuccess);
        Assert.Equal(DomainErrors.Specialty.NameAlredyExists, result.Error);
    }

    [Fact]
    public async Task Handle_SpecialtySuccessfullyCreated_ReturnsSuccessResult()
    {
        // Arrange
        var command = new CreateSpecialtyCommand("New Specialty", string.Empty);
        _specialtyReadOnlyRepositoryMock.Setup(repo => repo.ExistsByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var specialty = new Specialty(command.Name, command.Description);
        _mapperMock.Setup(mapper => mapper.Map<Specialty>(It.IsAny<CreateSpecialtyCommand>()))
            .Returns(specialty);

        _specialtyPersistenceRepositoryMock.Setup(repo => repo.AddAsync(It.IsAny<Specialty>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(specialty.Id);

        // Act
        Result<Guid>? result = await _handler.Handle(command, CancellationToken.None) as Result<Guid>;

        // Assert
        Assert.True(result!.IsSuccess);
    }

}