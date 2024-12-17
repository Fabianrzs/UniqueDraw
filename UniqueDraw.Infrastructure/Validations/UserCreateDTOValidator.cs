namespace UniqueDraw.Infrastructure.Validations;

public class UserCreateDTOValidator : AbstractValidator<UserCreateDTO>
{
    public UserCreateDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del usuario es obligatorio.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El correo electrónico es obligatorio.")
            .EmailAddress().WithMessage("Debe ser un correo electrónico válido.");

        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("El ClientId no puede estar vacío.");
    }
}
