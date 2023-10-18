using FluentValidation;
using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Expenses;

public class DeleteIncomeValidator : AbstractValidator<DeleteIncomeRequest>
{
  public DeleteIncomeValidator()
  {
    RuleFor(expense => expense.IncomeId).GreaterThan(0);
  }
}
