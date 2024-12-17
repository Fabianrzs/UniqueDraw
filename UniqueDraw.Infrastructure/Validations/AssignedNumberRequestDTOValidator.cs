namespace UniqueDraw.Infrastructure.Validations;

public class AssignedNumberRequestDTOValidator : AbstractValidator<AssignedNumberRequestDTO>
{
    public AssignedNumberRequestDTOValidator()
    {
        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("El ClientId no puede estar vacío.");

        RuleFor(x => x.RaffleId)
            .NotEmpty().WithMessage("El RaffleId no puede estar vacío.");
    }
}
