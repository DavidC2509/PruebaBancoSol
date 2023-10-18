using Ardalis.Result;
using TrackFinance.Core.Interfaces;
using TrackFinance.Core.TransactionAgregate;


namespace TrackFinance.Core.Services;


public class ByDayStrategy : ITransactionStrategy
{
    public DateType SupportedDateType => DateType.Day;

    public ByDayStrategy()
    {
    }
    public Result<List<TransactionDataDto>> GetTransactionItemsAsync(List<Transaction> itemsByDay, CancellationToken cancellationToken = default)
    {
        var transactions = new List<TransactionDataDto>();
        var selectedValues = itemsByDay.Select(t => new
        {
            t.ExpenseDate.Date,
            t.Amount,
            t.ExpenseDate.DayOfWeek,
            t.TransactionDescriptionType,
            t.TransactionType
        })
              .GroupBy(t => new { t.TransactionType, t.Date, t.DayOfWeek, t.TransactionDescriptionType })
              .Select(y => new { Date = y.Key, TotalAmount = y.Sum(x => x.Amount), y.Key.TransactionType })
              .OrderBy(x => x.Date.Date)
              .ToList();

        foreach (var t in selectedValues)
        {
            var transactionItem = new TransactionDataDto
            {
                Date = t.Date.Date,
                DayOfWeek = t.Date.DayOfWeek,
                Day = t.Date.Date.Day,
                TotalAmount = t.TotalAmount,
                TransactionDescriptionType = t.Date.TransactionDescriptionType,
                TransactionType = t.TransactionType
            };

            transactions.Add(transactionItem);
        }
        if (transactions.Count == 0) Result<List<TransactionDataDto>>.NotFound();

        return transactions;
    }

    public Result<List<TransactionDataDto>> GetTransactionItemsForLineChartsAsync(List<Transaction> itemByDay, CancellationToken cancellationToken = default)
    {
        var transactions = new List<TransactionDataDto>();
        var selectedValues = itemByDay.Select(t => new
        {
            t.ExpenseDate.Date,
            t.Amount,
            t.ExpenseDate.DayOfWeek,
            t.TransactionDescriptionType,
            t.TransactionType
        })
              .GroupBy(t => new { t.TransactionType, t.Date, t.DayOfWeek })
              .Select(y => new { Date = y.Key, TotalAmount = y.Sum(x => x.Amount), y.Key.TransactionType })
              .OrderBy(x => x.Date.Date)
              .ToList();

        foreach (var t in selectedValues)
        {
            var transactionItem = new TransactionDataDto
            {
                Date = t.Date.Date,
                DayOfWeek = t.Date.DayOfWeek,
                Day = t.Date.Date.Day,
                TotalAmount = t.TotalAmount,
                TransactionType = t.TransactionType
            };

            transactions.Add(transactionItem);
        }
        if (transactions.Count == 0) Result<List<TransactionDataDto>>.NotFound();

        return transactions;
    }
}


