using FluentValidation;
using TrackFinance.Web.Endpoints.Incomes;

namespace TrackFinance.Web.Endpoints.Incomes;

public class IncomesListValidator : AbstractValidator<IncomeListRequest>
{
  public IncomesListValidator()
  {
    RuleFor(expense => expense.UserId).GreaterThan(0);
  }
}
