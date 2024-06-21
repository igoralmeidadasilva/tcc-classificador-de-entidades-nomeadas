namespace Classificador.Api.Tests.Unit.Domain.Tests.Entities;

public sealed class ClassificationTest
{
    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenIdNamedEntitieIsDefault()
    {
        Assert.Throws<ArgumentNullException>(() => new Classification(default, Guid.NewGuid(), Guid.NewGuid(), "Comment"));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenIdCategoryIsDefault()
    {
        Assert.Throws<ArgumentNullException>(() => new Classification(Guid.NewGuid(), default, Guid.NewGuid(), "Comment"));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenIdUserIsDefault()
    {
        Assert.Throws<ArgumentNullException>(() => new Classification(Guid.NewGuid(), Guid.NewGuid(), default, "Comment"));
    }

    [Fact]
    public void Constructor_ShouldThrowArgumentNullException_WhenCommentIsNull()
    {
        Assert.Throws<ArgumentNullException>(() => new Classification(Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), null));
    }

    [Fact]
    public void Constructor_ShouldSetProperties_WhenValidParameters()
    {
        var idNamedEntitie = Guid.NewGuid();
        var idCategory = Guid.NewGuid();
        var idUser = Guid.NewGuid();
        var comment = "Valid Comment";

        var classification = new Classification(idNamedEntitie, idCategory, idUser, comment);

        Assert.Equal(idNamedEntitie, classification.IdNamedEntitie);
        Assert.Equal(idCategory, classification.IdCategory);
        Assert.Equal(idUser, classification.IdUser);
        Assert.Equal(comment, classification.Comment);
    }

    [Fact]
    public void Update_ShouldUpdateProperties_WhenValidEntity()
    {
        var idNamedEntitie = Guid.NewGuid();
        var idCategory = Guid.NewGuid();
        var idUser = Guid.NewGuid();
        var comment = "Original Comment";

        var classification = new Classification(idNamedEntitie, idCategory, idUser, comment);

        var newIdNamedEntitie = Guid.NewGuid();
        var newIdCategory = Guid.NewGuid();
        var newIdUser = Guid.NewGuid();
        var newComment = "Updated Comment";

        var updatedClassification = new Classification(newIdNamedEntitie, newIdCategory, newIdUser, newComment);

        classification.Update(updatedClassification);

        Assert.Equal(newIdNamedEntitie, classification.IdNamedEntitie);
        Assert.Equal(newIdCategory, classification.IdCategory);
        Assert.Equal(newIdUser, classification.IdUser);
        Assert.Equal(newComment, classification.Comment);
    }
}
