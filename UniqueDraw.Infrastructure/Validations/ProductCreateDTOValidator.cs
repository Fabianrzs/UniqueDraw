namespace UniqueDraw.Infrastructure.Validations;

public class ProductCreateDTOValidator : AbstractValidator<ProductCreateDTO>
{
    public ProductCreateDTOValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("El nombre del producto es obligatorio.");

        RuleFor(x => x.Description)
            .MaximumLength(500).WithMessage("La descripción no puede tener más de 500 caracteres.");

        RuleFor(x => x.Price)
            .Must(x => x > 0).WithMessage("El precio debe ser mayor a 0.");
    }
}
