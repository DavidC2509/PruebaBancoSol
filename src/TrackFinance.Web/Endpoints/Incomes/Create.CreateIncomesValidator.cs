using FluentValidation;
using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Expenses;

public class CreateIncomesValidator : AbstractValidator<CreateIncomesRequest>
{
  public CreateIncomesValidator() 
  { 
    RuleFor(expense => expense.Description).NotEmpty().NotNull();
    RuleFor(expense => expense.Amount).GreaterThan(0);
  }
}
