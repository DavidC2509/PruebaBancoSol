using FluentValidation;

namespace TrackFinance.Web.Endpoints.Expenses;

public class GetExpenseByIdValidator : AbstractValidator<GetExpenseByIdRequest>
{
  public GetExpenseByIdValidator()
  {
    RuleFor(expense => expense.ExpenseId).GreaterThan(0);
  }
}
