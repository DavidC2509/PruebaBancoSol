using Newtonsoft.Json;
using TrackFinance.Web;
using TrackFinance.Web.Endpoints.Incomes;
using Xunit;

namespace TrackFinance.FunctionalTests.ApiEndpoints.Income;
public class IncomeGetById : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _client;

  public IncomeGetById(CustomWebApplicationFactory<WebMarker> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnIncomeValueSuccess()
  {
    var response = await _client.GetStringAsync(GetIncomeByIdRequest.BuildRoute(5));
    var result = JsonConvert.DeserializeObject<GetIncomeByIdResponse>(response);
    Assert.Equal("venta comida", result!.Description);
    Assert.True(result.Amount > 0);
  }
}
