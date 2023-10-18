using FluentValidation;

namespace TrackFinance.Web.Endpoints.Expenses;

public class DeleteExpenseValidator : AbstractValidator<DeleteExpenseRequest>
{
  public DeleteExpenseValidator()
  {
    RuleFor(expense => expense.ExpenseId).GreaterThan(0);
  }
}
