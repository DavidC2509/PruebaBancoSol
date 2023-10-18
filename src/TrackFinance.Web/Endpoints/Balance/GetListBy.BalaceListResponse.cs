namespace TrackFinance.Web.Endpoints.Balance;

public class BalanceListResponse
{
  public List<TransactionRecord>? ExpensesTransaction { get; set; }
  public List<TransactionRecord>? IncomesTransaction { get; set; }
}
