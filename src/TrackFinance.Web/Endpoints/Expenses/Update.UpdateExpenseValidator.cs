using FluentValidation;

namespace TrackFinance.Web.Endpoints.Expenses;

public class UpdateExpensesValidator : AbstractValidator<UpdateExpenseRequest>
{
  public UpdateExpensesValidator() 
  { 
    RuleFor(expense => expense.Amount).GreaterThan(0);
    RuleFor(expense => expense.Description).NotEmpty().NotNull();
  }
}
