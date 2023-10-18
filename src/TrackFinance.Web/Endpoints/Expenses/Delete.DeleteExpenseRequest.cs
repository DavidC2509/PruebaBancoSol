namespace TrackFinance.Web.Endpoints.Expenses;

public class DeleteExpenseRequest
{
  public const string Route = "/Expenses/{ExpenseId:int}";
  public static string BuildRoute(int expenseId) => Route.Replace("{Expense:int}", expenseId.ToString());
  public int ExpenseId { get; set; }
}
