using FluentValidation;
using ProductsMockApi.Application.Requests;

namespace ProductsMockApi.Application.Validators;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
  public CreateProductRequestValidator()
  {
    RuleFor(r => r.Name)
      .NotEmpty()
      .WithMessage("Product Name is required")
      .MinimumLength(3)
      .WithMessage("Product name is too short.");

    RuleFor(r => r.Price)
      .GreaterThan(0)
      .WithMessage("Product price should be greater than 0");

    RuleFor(r => r.Capacity)
      .NotEmpty()
      .WithMessage("Capacity is required");

    RuleFor(r => r.Color)
      .NotEmpty()
      .WithMessage("Color is required")
      .MinimumLength(3)
      .WithMessage("Color is too short");
  }
}