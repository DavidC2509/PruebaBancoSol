namespace TrackFinance.Web.Endpoints.Historical;

public class GetHistoricalRecordByUserResponse
{
  public List<HistoricalRecord> HistoricalRecord { get; set; } = new();
}