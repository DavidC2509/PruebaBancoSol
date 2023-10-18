using TrackFinance.Core.TransactionAgregate.Enum;
using TrackFinance.Core.TransactionAgregate;

namespace TrackFinance.Web.Endpoints.Balance;

public class BalaceRequestForLineCharts
{
  public const string Route = "/Balance/LineCharts/{DateType}/{UserId}/{TransactionType}";
  public DateType DateType { get; set; }
  public int UserId { get; set; }
  public TransactionType TransactionType { get; set; }
}
