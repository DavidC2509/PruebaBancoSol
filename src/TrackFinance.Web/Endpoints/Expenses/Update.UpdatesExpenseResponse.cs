using TrackFinance.Web.Endpoints.Models;

namespace TrackFinance.Web.Endpoints.Expenses;

public class UpdateExpenseResponse
{
  public TransactionIncomeExpenseRecord _expenseRecord;

  public UpdateExpenseResponse(TransactionIncomeExpenseRecord expenseRecord)
  {
    _expenseRecord = expenseRecord;
  }
}
