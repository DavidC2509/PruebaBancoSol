namespace TrackFinance.Web.Endpoints.Incomes;

public class CreateIncomesResponse
{
  public CreateIncomesResponse(int statusResult, int expensesId)
  {
    StatusResult = statusResult;
    ExpensesId = expensesId;
  }

  public int StatusResult { get; set; }
  public int ExpensesId { get; set; }
}
