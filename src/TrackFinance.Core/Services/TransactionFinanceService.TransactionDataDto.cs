using TrackFinance.Core.TransactionAgregate.Enum;

namespace TrackFinance.Core.Services;

public class TransactionDataDto
{
  public DateTime Date { get;  set; }
  public DayOfWeek? DayOfWeek { get;  set; } = null;
  public int Day { get;  set; }
  public decimal TotalAmount { get;  set; }
  public TransactionDescriptionType TransactionDescriptionType { get;  set; }
  public int Week { get;  set; }
  public int Year { get;  set; }
  public int Month { get;  set; }
  public TransactionType TransactionType { get;  set; }
}
