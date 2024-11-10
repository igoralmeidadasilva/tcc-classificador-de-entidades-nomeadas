namespace Classificador.Api.Presentation.Core.Extensions;

public static class ViewModelExtensions
{
    public static CreateUserCommand ToCommand(this CreateUserForm form)
        => new(form.Email!, form.Password!, form.ConfirmPassword!, form.Name!, form.Contact, form.SpecialtyId);
}
