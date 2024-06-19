namespace Classificador.Api.Tests.Unit.Domain.Tests.Entities;

public sealed class UserTest
{
    // [Fact]
    // public void CreateUser_ValidInput_SuccessfullyCreateUser()
    // {
    //     // Arrange
    //     string email = "Email@tests.com";
    //     string hashedPassword = "password";
    //     string name = "userTests";
    //     string contact = "(00)99988-7766";
        
    //     // Act
    //     var user_1 = new User(Guid.NewGuid(), email, hashedPassword, name, contact);
    //     var user_2 = new User(Guid.NewGuid(), email, hashedPassword, name);

    //     // Assert
    //     Assert.Equal(email, user_1.Email);
    //     Assert.Equal(hashedPassword, user_1.HashedPassword);
    //     Assert.Equal(name, user_1.Name);
    //     Assert.Equal(UserRole.Standard, user_1.Role);
    //     Assert.Equal(contact, user_1.Contact);

    //     Assert.Equal(email, user_2.Email);
    //     Assert.Equal(hashedPassword, user_2.HashedPassword);
    //     Assert.Equal(name, user_1.Name);
    //     Assert.Equal(UserRole.Standard, user_2.Role);
    //     Assert.Equal("", user_2.Contact);
    //     Assert.False(user_2.IsActive);
    // }
    
    // [Fact]
    // public void CreateUser_InvalidInput_ThrowArgumentExceptionFailedCreateUser()
    // {
    //     // Arrange
    //     string email = "Email@tests.com";
    //     string emailEmpty = "";
    //     string emailNull = null!;

    //     string hashedPassword = "password";
    //     string hashedPasswordEmpty = "";
    //     string hashedPasswordNull = null!;

    //     string name = "Name";
    //     string nameEmpty = "";
    //     string nameNull = null!;


    //     // Act & Assert
    //     // Email is Empty & null
    //     Assert.Throws<ArgumentException>(() => new User(Guid.NewGuid(), emailEmpty, hashedPassword, name));
    //     Assert.Throws<ArgumentNullException>(() => new User(Guid.NewGuid(),emailNull, hashedPassword, name));

    //     // HashedPasswrod is Empty & null
    //     Assert.Throws<ArgumentException>(() => new User(Guid.NewGuid(),email, hashedPasswordEmpty, name));
    //     Assert.Throws<ArgumentNullException>(() => new User(Guid.NewGuid(),email, hashedPasswordNull, name));

    //     // Name is Empty & null
    //     Assert.Throws<ArgumentException>(() => new User(Guid.NewGuid(),email, hashedPassword, nameEmpty));
    //     Assert.Throws<ArgumentNullException>(() => new User(Guid.NewGuid(),email, hashedPassword, nameNull));
    // }

    // [Theory]
    // [InlineData("novoEmail@tests.com")]
    // public void UpdateUsersEmail_ValidInput_SuccessfullyUpdateUsersEmail(string newEmail)
    // {
    //     // Arrange
    //     var email = "Email@tests.com";
    //     var user = new User(Guid.NewGuid(), email, "password", "userTests");
        
    //     // Act
    //     var updatedUser = user.UpdateEmail(newEmail);

    //     // Asserts
    //     Assert.Equal(updatedUser, user);
    //     Assert.Equal(updatedUser.Id, user.Id);
    //     Assert.Equal(updatedUser.Email, user.Email);
    //     Assert.NotEqual(updatedUser.Email, email);
    // }

    // [Fact]
    // public void UpdateUsersEmail_SameEmailAsInput_WhenSameUserEmailIsProvided()
    // {
    //     // Arrange
    //     var email = "Email@tests.com";
    //     var user = new User(Guid.NewGuid(), email, "password", "userTests");
        
    //     // Act
    //     var updatedUser = user.UpdateEmail(email);

    //     // Asserts
    //     Assert.Equal(email, updatedUser.Email);
    // }

    // [Fact]
    // public void UpdateUsersEmail_InvalidInput_ThrowArgumentExceptionFailedUpdateUser()
    // {
    //     // Arrange
    //     string newemailEmpty = "";
    //     string newEmailNull = null!;

    //     var user = new User(Guid.NewGuid(), "Email@tests.com", "password", "userTests");

    //     // Act & Asserts
    //     Assert.Throws<ArgumentException>(() => user.UpdateEmail(newemailEmpty));
    //     Assert.Throws<ArgumentNullException>(() => user.UpdateEmail(newEmailNull));
    // }


    // [Theory]
    // [InlineData("123456")]
    // public void UpdateUsersHashedPassword_ValidInput_SuccessfullyUpdateUsersHashedPassword(string newHashedPassword)
    // {
    //     // Arrange
    //     var hashedPassword = "654321";
    //     var user = new User(Guid.NewGuid(), "Email@tests.com", hashedPassword, "userTests");
        
    //     // Act
    //     var updatedUser = user.UpdateHashedPassword(newHashedPassword);

    //     // Asserts
    //     Assert.Equal(newHashedPassword, updatedUser.HashedPassword);
    //     Assert.Equal(updatedUser, user);
    //     Assert.Equal(updatedUser.Id, user.Id);
    //     Assert.Equal(updatedUser.HashedPassword, user.HashedPassword);
    //     Assert.NotEqual(updatedUser.HashedPassword, hashedPassword);
    // }

    // [Fact]
    // public void UpdateUsersHashedPassword_SameHashedPasswordAsInput_WhenSameUserHashedPasswordIsProvided()
    // {
    //     // Arrange
    //     var hashedPassword = "654321";
    //     var user = new User(Guid.NewGuid(), "Email@tests.com", hashedPassword, "userTests");
        
    //     // Act
    //     var updatedUser = user.UpdateHashedPassword(hashedPassword);

    //     // Asserts
    //     Assert.Equal(hashedPassword, updatedUser.HashedPassword);
    // }

    // [Fact]
    // public void UpdateUsersHashedPassword_InvalidInput_ThrowArgumentExceptionFailedUpdateUser()
    // {
    //     // Arrange
    //     string newHashedPasswordEmpty = "";
    //     string newHashedPasswordNull = null!;

    //     var user = new User(Guid.NewGuid(), "Email@tests.com", "password", "userTests");

    //     // Act & Asserts
    //     Assert.Throws<ArgumentException>(() => user.UpdateEmail(newHashedPasswordEmpty));
    //     Assert.Throws<ArgumentNullException>(() => user.UpdateEmail(newHashedPasswordNull));
    // }

    // [Theory]
    // [InlineData("newUserTests")]
    // public void UpdateUsersName_ValidInput_SuccessfullyUpdateUsersName(string newName)
    // {
    //     // Arrange
    //     var name = "userTests";
    //     var user = new User(Guid.NewGuid(), "Email@tests.com", "123456", name);
        
    //     // Act
    //     var updatedUser = user.UpdateName(newName);

    //     // Asserts
    //     Assert.Equal(updatedUser, user);
    //     Assert.Equal(updatedUser.Id, user.Id);
    //     Assert.Equal(updatedUser.Name, user.Name);
    //     Assert.NotEqual(updatedUser.Name, name);
    // }

    // [Fact]
    // public void UpdateUsersName_SameNameAsInput_WhenSameUserNameIsProvided()
    // {
    //     // Arrange
    //     var name = "userTests";
    //     var user = new User(Guid.NewGuid(), "123456", "password", name);
        
    //     // Act
    //     var updatedUser = user.UpdateName(name);

    //     // Asserts
    //     Assert.Equal(name, updatedUser.Name);
    // }

    // [Fact]
    // public void UpdateUsersName_InvalidInput_ThrowArgumentExceptionFailedUpdateUser()
    // {
    //     // Arrange
    //     string newNameEmpty = "";
    //     string newNameNull = null!;

    //     var user = new User(Guid.NewGuid(), "Email@tests.com", "password", "userTests");

    //     // Act & Asserts
    //     Assert.Throws<ArgumentException>(() => user.UpdateEmail(newNameEmpty));
    //     Assert.Throws<ArgumentNullException>(() => user.UpdateEmail(newNameNull));
    // }

    // [Theory]
    // [InlineData("(00)99900-0000")]
    // public void UpdateContact_ValidInput_SuccessfullyUpdateUsersContact(string newContact)
    // {
    //     // Arrange
    //     var contact = "userTests";
    //     var user = new User(Guid.NewGuid(), "Email@tests.com", "123456", "userTests", contact: contact);
        
    //     // Act
    //     var updatedUser = user.UpdateContact(newContact);

    //     // Asserts
    //     Assert.Equal(updatedUser, user);
    //     Assert.Equal(updatedUser.Id, user.Id);
    //     Assert.Equal(updatedUser.Contact, user.Contact);
    //     Assert.NotEqual(updatedUser.Contact, contact);
    // }

    // [Fact]
    // public void UpdateUsersContact_SameContactAsInput_WhenSameUserContactIsProvided()
    // {
    //     // Arrange
    //     var contact = "(99)99988-7766";
    //     var user = new User(Guid.NewGuid(), "Email@tests.com", "123456", "userTests", contact: contact);
        
    //     // Act
    //     var updatedUser = user.UpdateContact(contact);

    //     // Asserts
    //     Assert.Equal(contact, updatedUser.Contact);
    // }

    // [Fact]
    // public void UpdateUsersContact_InvalidInput_ThrowArgumentExceptionFailedUpdateUser()
    // {
    //     // Arrange
    //     string newContactEmpty = "";
    //     string newContactNull = null!;

    //     var user = new User(Guid.NewGuid(), "Email@tests.com", "password", "userTests", "(99)99988-7766");

    //     // Act & Asserts
    //     Assert.Throws<ArgumentException>(() => user.UpdateEmail(newContactEmpty));
    //     Assert.Throws<ArgumentNullException>(() => user.UpdateEmail(newContactNull));
    // }


    // [Fact]
    // public void UpdateUser_ValidInput_SuccessfullyUpdateUser()
    // {
    //     // Arrange
    //     var oldUser = new User(Guid.NewGuid(), "Email@tests.com", "123456", "userTests");
    //     var newUser = new User("NewEmail@tests.com", "654321", "newUserTests");
        
    //     // Act
    //     var updatedUser = oldUser.Update(newUser);

    //     // Asserts
    //     Assert.Equal(updatedUser.Id, oldUser.Id);
    //     Assert.Equal(updatedUser, oldUser);
    // }

}
