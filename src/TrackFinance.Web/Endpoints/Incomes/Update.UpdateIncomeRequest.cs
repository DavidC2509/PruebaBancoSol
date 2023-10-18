using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.Web.Endpoints.Models;

namespace TrackFinance.Web.Endpoints.Incomes;

public class UpdateIncomeRequest : UpdateTransaction
{
  public const string Route = "/Incomes";
}
