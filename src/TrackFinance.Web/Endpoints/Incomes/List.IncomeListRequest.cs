namespace TrackFinance.Web.Endpoints.Incomes;

public class IncomeListRequest
{
  public const string Route = "/Income/{UserId:int}/user";
  public static string BuildRoute(int userId) => Route.Replace("{UserId:int}", userId.ToString());
  public int UserId { get; set; }
}
