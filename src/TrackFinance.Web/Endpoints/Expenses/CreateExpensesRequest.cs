using TrackFinance.Web.Endpoints.Models;

namespace TrackFinance.Web.Endpoints.Expenses;

public class CreateExpensesRequest : CreateTransaction
{
  public const string Route = "/Expenses";
}
