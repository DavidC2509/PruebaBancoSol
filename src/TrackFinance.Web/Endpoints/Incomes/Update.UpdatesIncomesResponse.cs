namespace TrackFinance.Web.Endpoints.Incomes;

public class UpdateIncomesResponse
{
  public IncomeRecord _expenseRecord;

  public UpdateIncomesResponse(IncomeRecord expenseRecord)
  {
    _expenseRecord = expenseRecord;
  }
}
