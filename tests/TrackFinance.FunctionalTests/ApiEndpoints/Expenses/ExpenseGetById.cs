using Newtonsoft.Json;
using TrackFinance.Web;
using TrackFinance.Web.Endpoints.Expenses;
using Xunit;

namespace TrackFinance.FunctionalTests.ApiEndpoints.Expenses;
public class ExpenseGetById : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
 private readonly HttpClient _client;

 public ExpenseGetById(CustomWebApplicationFactory<WebMarker> factory)
 {
   _client = factory.CreateClient();
 }

 [Fact]
 public async Task ReturnExpenseValueSuccess()
 {
   var response = await _client.GetStringAsync(GetExpenseByIdRequest.BuildRoute(1));
   var result = JsonConvert.DeserializeObject<GetExpenseByIdResponse>(response);
   Assert.Equal("compra de equipos", result!.Description);
   Assert.True(result.Amount > 0);
 }
}
