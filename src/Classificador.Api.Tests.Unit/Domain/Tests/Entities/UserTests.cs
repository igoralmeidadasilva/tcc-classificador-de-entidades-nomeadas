using Classificador.Api.Domain.Entities;
using Classificador.Api.Domain.Enums;

namespace Classificador.Api.Tests.Unit.Domain.Tests.Entities;

public class UserTests
{
    [Fact]
    public void CreateUser_ValidInput_SuccessfullyCreateUser()
    {
        // Arrange
        string email = "Email@tests.com";
        string hashedPassword = "password";
        string name = "userTests";
        string contact = "(00)99988-7766";
        bool isActive = true;
        
        // Act
        var user_1 = new User(email, hashedPassword, name, contact, isActive);
        var user_2 = new User(email, hashedPassword, name);

        // Assert
        Assert.Equal(email, user_1.Email);
        Assert.Equal(hashedPassword, user_1.HashedPassword);
        Assert.Equal(name, user_1.Name);
        Assert.Equal(UserRole.Standard, user_1.Role);
        Assert.Equal(contact, user_1.Contact);
        Assert.Equal(isActive, user_1.IsActive);

        Assert.Equal(email, user_2.Email);
        Assert.Equal(hashedPassword, user_2.HashedPassword);
        Assert.Equal(name, user_1.Name);
        Assert.Equal(UserRole.Standard, user_2.Role);
        Assert.Equal("", user_2.Contact);
        Assert.False(user_2.IsActive);
    }
    
    [Fact]
    public void CreateUser_InvalidInput_ThrowArgumentExceptionFailedCreateUser()
    {
        // Arrange
        string email = "Email@tests.com";
        string emailEmpty = "";
        string emailNull = null!;

        string hashedPassword = "password";
        string hashedPasswordEmpty = "";
        string hashedPasswordNull = null!;

        string name = "Name";
        string nameEmpty = "";
        string nameNull = null!;


        // Act & Assert
        // Email is Empty & null
        Assert.Throws<ArgumentException>(() => new User(emailEmpty, hashedPassword, name));
        Assert.Throws<ArgumentNullException>(() => new User(emailNull, hashedPassword, name));

        // HashedPasswrod is Empty & null
        Assert.Throws<ArgumentException>(() => new User(email, hashedPasswordEmpty, name));
        Assert.Throws<ArgumentNullException>(() => new User(email, hashedPasswordNull, name));

        // Name is Empty & null
        Assert.Throws<ArgumentException>(() => new User(email, hashedPassword, nameEmpty));
        Assert.Throws<ArgumentNullException>(() => new User(email, hashedPassword, nameNull));
    }

    [Theory]
    [InlineData("novoEmail@tests.com")]
    public void UpdateUsersEmail_ValidInput_SuccessfullyUpdateUsersEmail(string newEmail)
    {
        // Arrange
        var email = "Email@tests.com";
        var user = new User(email, "password", "userTests");
        
        // Act
        var updatedUser = user.UpdateEmail(newEmail);

        // Asserts
        Assert.Equal(updatedUser, user);
        Assert.Equal(updatedUser.Id, user.Id);
        Assert.Equal(updatedUser.Email, user.Email);
        Assert.NotEqual(updatedUser.Email, email);
    }

    [Fact]
    public void UpdateUsersEmail_SameEmailAsInput_WhenSameUserEmailIsProvided()
    {
        // Arrange
        var email = "Email@tests.com";
        var user = new User(email, "password", "userTests");
        
        // Act
        var updatedUser = user.UpdateEmail(email);

        // Asserts
        Assert.Equal(email, updatedUser.Email);
    }

    [Fact]
    public void UpdateUsersEmail_InvalidInput_ThrowArgumentExceptionFailedUpdateUser()
    {
        // Arrange
        string newemailEmpty = "";
        string newEmailNull = null!;

        var user = new User("Email@tests.com", "password", "userTests");

        // Act & Asserts
        Assert.Throws<ArgumentException>(() => user.UpdateEmail(newemailEmpty));
        Assert.Throws<ArgumentNullException>(() => user.UpdateEmail(newEmailNull));
    }

}
