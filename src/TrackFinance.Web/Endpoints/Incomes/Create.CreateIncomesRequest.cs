using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.Web.Endpoints.Models;

namespace TrackFinance.Web.Endpoints.Incomes;

public class CreateIncomesRequest : CreateTransaction
{
  public const string Route = "/Incomes";
}
