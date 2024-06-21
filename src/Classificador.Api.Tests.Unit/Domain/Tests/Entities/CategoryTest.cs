using Moq;

namespace Classificador.Api.Tests.Unit.Domain.Tests.Entities;

public sealed class CategoryTest
{
    [Fact]
    public void Constructor_ShouldInitializeProperties()
    {
        // Arrange
        var name = "Test Category";
        var description = "Test Description";

        // Act
        var category = new Category(name, description);

        // Assert
        Assert.Equal(name, category.Name);
        Assert.Equal(description, category.Description);
        Assert.Empty(category.Classifications!);
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenNameIsNull()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Category(null!, "Description"));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenNameIsEmpty()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new Category(string.Empty, "Description"));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenDescriptionIsNull()
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Category("Name", null!));
    }

    [Fact]
    public void Update_ShouldUpdateProperties()
    {
        // Arrange
        var initialCategory = new Category("Initial Name", "Initial Description");
        var updatedCategory = new Category("Updated Name", "Updated Description");

        // Act
        initialCategory.Update(updatedCategory);

        // Assert
        Assert.Equal("Updated Name", initialCategory.Name);
        Assert.Equal("Updated Description", initialCategory.Description);
    }

}
