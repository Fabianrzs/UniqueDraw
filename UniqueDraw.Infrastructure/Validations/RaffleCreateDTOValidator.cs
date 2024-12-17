namespace UniqueDraw.Infrastructure.Validations;

public class RaffleCreateDTOValidator : AbstractValidator<RaffleCreateDTO>
{
    public RaffleCreateDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del sorteo es obligatorio.");

        RuleFor(x => x.StartDate)
            .LessThan(x => x.EndDate).WithMessage("La fecha de inicio debe ser anterior a la fecha de finalización.");

        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("El ClientId no puede estar vacío.");
    }
}
