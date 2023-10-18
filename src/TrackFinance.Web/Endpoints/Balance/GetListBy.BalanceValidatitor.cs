using FluentValidation;
using TrackFinance.Web.Endpoints.Balance;

namespace TrackFinance.Web.Endpoints.Expenses;

public class BalanceValidatitor : AbstractValidator<BalaceRequest>
{
  public BalanceValidatitor() 
  { 
    RuleFor(expense => expense.UserId).GreaterThan(0);
  }
}
