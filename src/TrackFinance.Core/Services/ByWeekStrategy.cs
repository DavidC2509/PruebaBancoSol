using System.Globalization;
using Ardalis.Result;
using TrackFinance.Core.Interfaces;
using TrackFinance.Core.TransactionAgregate;

namespace TrackFinance.Core.Services;


public class ByWeekStrategy : ITransactionStrategy
{
    public DateType SupportedDateType => DateType.Week;

    public ByWeekStrategy()
    {
    }

    public Result<List<TransactionDataDto>> GetTransactionItemsAsync(List<Transaction> itemByWeek, CancellationToken cancellationToken = default)
    {

        var transactionsResult = new List<TransactionDataDto>();

        var transactions = itemByWeek.Select(h => new
        {
            h.ExpenseDate.Year,
            WeekNumber = GetWeekOfYear(h.ExpenseDate),
            h.Amount,
            h.TransactionDescriptionType,
            h.TransactionType
        }).GroupBy(h => new { h.TransactionType, h.Year, h.WeekNumber, h.TransactionDescriptionType })
              .Select(g => new
              {
                  g.Key.Year,
                  g.Key.WeekNumber,
                  TotalAmount = g.Sum(h => h.Amount),
                  g.Key.TransactionDescriptionType,
                  g.Key.TransactionType
              })
              .OrderBy(g => g.Year)
              .ThenBy(g => g.WeekNumber).ToList();

        foreach (var transaction in transactions)
        {
            var transactionItem = new TransactionDataDto
            {
                TotalAmount = transaction.TotalAmount,
                Week = transaction.WeekNumber,
                Year = transaction.Year,
                TransactionDescriptionType = transaction.TransactionDescriptionType,
                TransactionType = transaction.TransactionType
            };
            transactionsResult.Add(transactionItem);
        }

        if (transactionsResult.Count == 0) Result<List<TransactionDataDto>>.NotFound();
        return transactionsResult;
    }


    public Result<List<TransactionDataDto>> GetTransactionItemsForLineChartsAsync(List<Transaction> itemByWeek, CancellationToken cancellationToken = default)
    {
        var transactionsResult = new List<TransactionDataDto>();

        var transactions = itemByWeek.Select(h => new
        {
            h.ExpenseDate.Year,
            WeekNumber = GetWeekOfYear(h.ExpenseDate),
            h.Amount,
            h.TransactionDescriptionType,
            h.TransactionType
        }).GroupBy(h => new { h.TransactionType, h.Year, h.WeekNumber })
              .Select(g => new
              {
                  g.Key.Year,
                  g.Key.WeekNumber,
                  TotalAmount = g.Sum(h => h.Amount),
                  g.Key.TransactionType
              })
              .OrderBy(g => g.Year)
              .ThenBy(g => g.WeekNumber).ToList();

        foreach (var transaction in transactions)
        {
            var transactionItem = new TransactionDataDto
            {
                TotalAmount = transaction.TotalAmount,
                Week = transaction.WeekNumber,
                Year = transaction.Year,
                TransactionType = transaction.TransactionType
            };
            transactionsResult.Add(transactionItem);
        }

        if (transactionsResult.Count == 0) Result<List<TransactionDataDto>>.NotFound();
        return transactionsResult;
    }


    private static int GetWeekOfYear(DateTime time)
    {
        var cal = CultureInfo.InvariantCulture.Calendar;
        var day = (int)cal.GetDayOfWeek(time);

        if (day is >= (int)DayOfWeek.Monday and <= (int)DayOfWeek.Wednesday)
        {
            time = time.AddDays(3);
        }
        return cal.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
    }
}


