using TrackFinance.Core.TransactionAgregate;
using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Web.Endpoints.Balance;

public class BalaceRequest
{
  public const string Route = "/Balance/{DateType}/{UserId}/{TransactionType}";
  public DateType DateType { get; set; }
  public int UserId { get; set; }
  public TransactionType TransactionType { get; set; }
}
