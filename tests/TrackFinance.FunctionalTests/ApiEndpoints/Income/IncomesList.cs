using Newtonsoft.Json;
using TrackFinance.Web;
using TrackFinance.Web.Endpoints.Incomes;
using Xunit;

namespace TrackFinance.FunctionalTests.ApiEndpoints.Income;
public class IncomesList : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _client;

  public IncomesList(CustomWebApplicationFactory<WebMarker> factory)
  {
    _client = factory.CreateClient();
  }

  [Fact]
  public async Task ReturnIncomesListValuesSuccess()
  {
    var response = await _client.GetStringAsync(IncomeListRequest.BuildRoute(1));
    var result = JsonConvert.DeserializeObject<IncomeListResponse>(response);
    Assert.True(result!.Incomes.Count > 0);
  }

  [Fact]
  public async Task ReturnIncomesListEmptySuccess()
  {
    var response = await _client.GetStringAsync(IncomeListRequest.BuildRoute(100));
    var result = JsonConvert.DeserializeObject<IncomeListResponse>(response);
    Assert.True(result!.Incomes.Count == 0);
  }

  [Fact]
  public async Task SearchIncomeValueSuccess()
  {
    var response = await _client.GetStringAsync(IncomeListRequest.BuildRoute(1));
    Assert.Contains("venta comida", response);
  }
}
