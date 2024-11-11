namespace Classificador.Api.Presentation.Core.Extensions;

public static class ViewModelExtensions
{
    public static CreateUserCommand ToCommand(this CreateUserForm form)
        => new(form.Email!, form.Password!, form.ConfirmPassword!, form.Name!, form.Contact, form.SpecialtyId);

    public static LoginUserCommand ToCommand(this LoginViewModel viewModel) 
        => new(viewModel.Email, viewModel.Password);

    public static CreatePrescribingInformationTxtCommand ToCommand(this CreatePrescribingInformationViewModel viewModel)
        => new(viewModel.File, viewModel.Description);
    
    public static CreateClassificationCommand ToCommand(this CreateClassificationForm form)
        => new(form.IdUser, form.IdNamedEntity, form.IdCategory, form.Comment ?? string.Empty);


    public static DeletePendingClassificationCommand ToCommand(this DeletePendingClassificationForm form)
        => new(form.IdClassification);
    
    public static UpdateClassificationToCompletedCommand ToCommand(this PatchClassificationToCompletedForm form)
        => new(form.IdUser, form.IdPrescribingInformation);
}
