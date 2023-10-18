using TrackFinance.Web.Endpoints.Models;

namespace TrackFinance.Web.Endpoints.Expenses;

public class ExpenseListResponse
{
  public List<TransactionIncomeExpenseRecord> Expenses { get; set; } = new();
}
