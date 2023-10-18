using FluentValidation;
using TrackFinance.Web.Endpoints.Historical;

namespace TrackFinance.Web.Endpoints.Expenses;

public class GetHistoricalRecordByUserValidator : AbstractValidator<GetHistoricalRecordByUserRequest>
{
  public GetHistoricalRecordByUserValidator() 
  { 
    RuleFor(expense => expense.UserId).GreaterThan(0);
    RuleFor(expense => expense.EndDate).GreaterThanOrEqualTo(expense => expense.StartDate);
    RuleFor(expense => expense.StartDate).LessThanOrEqualTo(expense => expense.EndDate);

  }
}
