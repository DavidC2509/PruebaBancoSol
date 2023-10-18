using Newtonsoft.Json;
using TrackFinance.Web;
using TrackFinance.Web.Endpoints.Historical;
using Xunit;

namespace TrackFinance.FunctionalTests.ApiEndpoints.Historical;
public class TrackFinanceGetHistoricalRecordByUser : IClassFixture<CustomWebApplicationFactory<WebMarker>>
{
  private readonly HttpClient _client;

  public TrackFinanceGetHistoricalRecordByUser(CustomWebApplicationFactory<WebMarker> factory)
  {
    _client = factory.CreateClient();
  }

  [Theory]
  [InlineData("2023-08-01", "2023-08-31", 1)]
  [InlineData("1900-01-01", "1900-01-01", 0)]
  public async Task ReturnsHistoricalValuesSuccess(string startDate, string endDate, int expectedValue)
  {
    var response = await _client.GetStringAsync(GetHistoricalRecordByUserRequest.BuildRoute(1, startDate, endDate));
    var result = JsonConvert.DeserializeObject<GetHistoricalRecordByUserResponse>(response);
    Assert.True(result?.HistoricalRecord.Count >= expectedValue);
  }

  [Fact]
  public async Task SearchHistoricalValues()
  {
    var response = await _client.GetStringAsync(GetHistoricalRecordByUserRequest.BuildRoute(1, "2023-08-01", "2023-08-31"));
    Assert.Contains("computadoras", response);
  }
}
