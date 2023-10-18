using FluentValidation;

namespace TrackFinance.Web.Endpoints.Expenses;

public class ExpensesListValidator : AbstractValidator<ExpenseListRequest>
{
  public ExpensesListValidator()
  {
    RuleFor(expense => expense.UserId).GreaterThan(0);
  }
}
